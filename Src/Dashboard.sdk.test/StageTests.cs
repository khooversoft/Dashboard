using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.sdk.Records;
using FluentAssertions;
using Toolbox.Extensions;
using Xunit;

namespace Dashboard.sdk.test
{
    public class StageTests
    {
        [Fact]
        public async Task Stage_WhenRoundTripped_ShouldPass()
        {
            DashboardMgmtClient client = Application.GetClient();

            IReadOnlyList<StageRecord> providers = await client.Stage.List();
            await providers.ForEachAsync(async x => await client.Stage.Delete(x.Stage));

            const string stageName = "Stage_1";

            int id = await client.Stage.Set(stageName, 10);

            providers = await client.Stage.List();
            providers.Count.Should().Be(1);
            providers.First().Action(x =>
            {
                x.StageId.Should().Be(id);
                x.Stage.Should().Be(stageName);
                x.OrderNumber.Should().Be(10);
            });

            await client.Stage.Set(stageName, 20);

            providers = await client.Stage.List();
            providers.Count.Should().Be(1);
            providers.First().Action(x =>
            {
                x.StageId.Should().Be(id);
                x.Stage.Should().Be(stageName);
                x.OrderNumber.Should().Be(20);
            });

            await client.Stage.Delete(stageName);

            providers = await client.Stage.List();
            providers.Count.Should().Be(0);
        }

        [Fact]
        public async Task MultipleStage_WhenRoundTripped_ShouldPass()
        {
            DashboardMgmtClient client = Application.GetClient();

            IReadOnlyList<StageRecord> providers = await client.Stage.List();
            await providers.ForEachAsync(async x => await client.Stage.Delete(x.Stage));

            const int count = 10;
            var list = new List<string>(Enumerable.Range(0, count).Select(x => $"Stage_{x}"));

            await list.ForEachAsync(async x => await client.Stage.Set(x, 5));

            providers = await client.Stage.List();
            providers.Count.Should().Be(count);

            providers.OrderBy(x => x.Stage)
                .Zip(list.OrderBy(x => x), (o, i) => (o, i))
                .All(x => x.o.Stage == x.i)
                .Should().BeTrue();

            await providers.ForEachAsync(async x => await client.Stage.Delete(x.Stage));

            providers = await client.Stage.List();
            providers.Count.Should().Be(0);
        }
    }
}
