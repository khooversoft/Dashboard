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
    public class StageController : Controller
    {
        private readonly DashboardMgmtStore _dashboardMgmtStore;

        public StageController(DashboardMgmtStore dashboardMgmtStore)
        {
            _dashboardMgmtStore = dashboardMgmtStore;
        }

        [HttpGet("{stage}")]
        public async Task<IActionResult> Get(string stage)
        {
            IReadOnlyList<StageRecord> records = await _dashboardMgmtStore.Client.Stage.List(stage);

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

        [HttpDelete("{stage}")]
        public async Task Delete(string stage)
        {
            await _dashboardMgmtStore.Client.Stage.Delete(stage);
        }
    }
}
