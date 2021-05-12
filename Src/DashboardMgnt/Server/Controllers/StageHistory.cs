using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.sdk.Records;
using DashboardMgnt.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DashboardMgnt.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StageHistory : Controller
    {
        private readonly DashboardMgmtStore _dashboardMgmtStore;

        public StageHistory(DashboardMgmtStore dashboardMgmtStore)
        {
            _dashboardMgmtStore = dashboardMgmtStore;
        }

        [HttpGet("{provider}/{stage}")]
        public async Task<IActionResult> Get(string provider, string stage)
        {
            IReadOnlyList<StageHistoryRecord> records = await _dashboardMgmtStore.Client.StageHistory.List(provider, stage);

            return records.Count switch
            {
                0 => NotFound(),

                _ => Ok(records.First()),
            };
        }

        [HttpPost]
        public async Task Post([FromBody] StageRecord record)
        {
            await _dashboardMgmtStore.Client.Stage.Set(record.Stage, record.OrderNumber);
        }

        [HttpDelete("{provider}/{stage}")]
        public async Task Delete(string provider, string stage)
        {
            await _dashboardMgmtStore.Client.StageHistory.Delete(provider, stage);
        }
    }
}
