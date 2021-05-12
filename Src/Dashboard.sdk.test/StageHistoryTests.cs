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
    [Collection("Database_Tests")]
    public class StageHistoryTests
    {
        [Fact]
        public async Task Provider_WhenRoundTripped_ShouldPass()
        {
            const string providerName = "Provider_1";
            const string stageName = "Stage_1";
            DateTime startDate = DateTime.Now.Date;
            DateTime completedDate = DateTime.Now.AddDays(1).Date;

            DashboardMgmtClient client = Application.GetClient();
            await Application.ClearDatabase();

            int providerId = await client.Provider.Set(providerName, true);
            int stageId = await client.Stage.Set(stageName, 0);


            IReadOnlyList<StageHistoryRecord> stageHistories = await client.StageHistory.List();

            await client.StageHistory.Set(providerName, stageName, null, null);

            stageHistories = await client.StageHistory.List();
            stageHistories.Count.Should().Be(1);
            stageHistories.First().Action(x =>
            {
                x.ProviderId.Should().Be(providerId);
                x.Provider.Should().Be(providerName);
                x.StageId.Should().Be(stageId);
                x.Stage.Should().Be(stageName);
                x.StartDate.Should().BeNull();
                x.CompletedDate.Should().BeNull();
            });

            await client.StageHistory.Set(providerName, stageName, startDate, null);

            stageHistories = await client.StageHistory.List(providerName, stageName);
            stageHistories.Count.Should().Be(1);
            stageHistories.First().Action(x =>
            {
                x.ProviderId.Should().Be(providerId);
                x.Provider.Should().Be(providerName);
                x.StageId.Should().Be(stageId);
                x.Stage.Should().Be(stageName);
                x.StartDate.Should().Be(startDate);
                x.CompletedDate.Should().BeNull();
            });

            await client.StageHistory.Set(providerName, stageName, startDate, completedDate);

            stageHistories = await client.StageHistory.List(providerName);
            stageHistories.Count.Should().Be(1);
            stageHistories.First().Action(x =>
            {
                x.ProviderId.Should().Be(providerId);
                x.Provider.Should().Be(providerName);
                x.StageId.Should().Be(stageId);
                x.Stage.Should().Be(stageName);
                x.StartDate.Should().Be(startDate);
                x.CompletedDate.Should().Be(completedDate);
            });

            await client.StageHistory.Delete(providerName, stageName);
            stageHistories = await client.StageHistory.List();
            stageHistories.Count.Should().Be(0);
        }
    }
}
