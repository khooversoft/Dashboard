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
    public class ProviderController : ControllerBase
    {
        private readonly DashboardMgmtStore _dashboardMgmtStore;

        public ProviderController(DashboardMgmtStore dashboardMgmtStore)
        {
            _dashboardMgmtStore = dashboardMgmtStore;
        }

        [HttpGet("{provider}")]
        public async Task<IActionResult> Get(string provider)
        {
            IReadOnlyList<ProviderRecord> records = await _dashboardMgmtStore.Client.Provider.List(provider);

            return records.Count switch
            {
                0 => NotFound(),

                _ => Ok(records.First()),
            };
        }

        [HttpPost]
        public async Task Post([FromBody] ProviderRecord record)
        {
            await _dashboardMgmtStore.Client.Provider.Set(record.Provider, record.Show);
        }

        [HttpDelete("{provider}")]
        public async Task Delete(string provider)
        {
            await _dashboardMgmtStore.Client.Provider.Delete(provider);
        }
    }
}
