using at.PowerBIUnitTest.Portal.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.Identity.Web;
using System.Linq;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.OData.Routing;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNet.OData.Routing;
using Newtonsoft.Json;
using at.PowerBIUnitTest.Portal.Services;

namespace at.PowerBIUnitTest.Portal.Controllers
{
    public class PowerBiController : BaseController
    {
        public PowerBiController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<PowerBiController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor, logger)
        {
        }

        [HttpGet("odata/GetEmbedToken")]
        public async Task<string> GetEmbedToken([FromServices] PowerBiService powerBiService)
        {
            
            string token = await powerBiService.GetEmbedToken(downstreamWebApi, msIdTenantCurrentUser);
            return token;
        }
    }
}