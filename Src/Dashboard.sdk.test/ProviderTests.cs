using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.sdk.Records;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Toolbox.Extensions;
using Xunit;

namespace Dashboard.sdk.test
{
    [Collection("Database_Tests")]
    public class ProviderTests
    {
        [Fact]
        public async Task Provider_WhenRoundTripped_ShouldPass()
        {
            DashboardMgmtClient client = Application.GetClient();

            IReadOnlyList<ProviderRecord> providers = await client.Provider.List();
            await providers.ForEachAsync(async x => await client.Provider.Delete(x.Provider));

            const string providerName = "Provider_1";

            int id = await client.Provider.Set(providerName, true);

            providers = await client.Provider.List();
            providers.Count.Should().Be(1);
            providers.First().Action(x =>
            {
                x.ProviderId.Should().Be(id);
                x.Provider.Should().Be(providerName);
                x.Show.Should().Be(true);
            });

            providers = await client.Provider.List(provider: providerName);
            providers.Count.Should().Be(1);
            providers.First().Action(x =>
            {
                x.ProviderId.Should().Be(id);
                x.Provider.Should().Be(providerName);
                x.Show.Should().Be(true);
            });

            await client.Provider.Set(providerName, false);

            providers = await client.Provider.List();
            providers.Count.Should().Be(1);
            providers.First().Action(x =>
            {
                x.ProviderId.Should().Be(id);
                x.Provider.Should().Be(providerName);
                x.Show.Should().Be(false);
            });

            await client.Provider.Delete(providerName);

            providers = await client.Provider.List();
            providers.Count.Should().Be(0);
        }


        [Fact]
        public async Task MultipleProvider_WhenRoundTripped_ShouldPass()
        {
            DashboardMgmtClient client = Application.GetClient();

            IReadOnlyList<ProviderRecord> providers = await client.Provider.List();
            await providers.ForEachAsync(async x => await client.Provider.Delete(x.Provider));

            const int count = 10;
            var list = new List<string>(Enumerable.Range(0, count).Select(x => $"Provider_{x}"));

            await list.ForEachAsync(async x => await client.Provider.Set(x, true));

            providers = await client.Provider.List();
            providers.Count.Should().Be(count);

            providers.OrderBy(x => x.Provider)
                .Zip(list.OrderBy(x => x), (o, i) => (o, i))
                .All(x => x.o.Provider == x.i)
                .Should().BeTrue();

            await providers.ForEachAsync(async x => await client.Provider.Delete(x.Provider));

            providers = await client.Provider.List();
            providers.Count.Should().Be(0);
        }
    }
}