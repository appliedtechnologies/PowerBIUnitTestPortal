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

    public class TabularModelsController : BaseController
    {
        public TabularModelsController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<TabularModelsController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor, logger)
        {
        }

        [EnableQuery]
        public IQueryable<TabularModel> Get([FromODataUri] int key)
        {
            logger.LogDebug($"Begin & End: TabularModelsController Get(key: {key})");
            return base.dbContext.TabularModels.Where(e => e.WorkspaceNavigation.TenantNavigation.MsId == this.msIdTenantCurrentUser && e.Id == key);
        }

        [EnableQuery]
        public IQueryable<TabularModel> Get()
        {
            logger.LogDebug($"Begin & End: TabularModelsController Get()");
            return base.dbContext.TabularModels.Where(e => e.WorkspaceNavigation.TenantNavigation.MsId == this.msIdTenantCurrentUser);
        }  
    }
}