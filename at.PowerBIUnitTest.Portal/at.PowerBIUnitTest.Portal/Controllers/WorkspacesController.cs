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
using Microsoft.EntityFrameworkCore;

namespace at.PowerBIUnitTest.Portal.Controllers
{

    public class WorkspacesController : BaseController
    {
        public WorkspacesController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<WorkspacesController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor, logger)
        {
        }

        [EnableQuery]
        public IQueryable<Workspace> Get([FromODataUri] int key)
        {
            logger.LogDebug($"Begin & End: WorkspacesController Get(key: {key})");
            return base.dbContext.Workspaces.Where(e => e.TenantNavigation.MsId == this.msIdTenantCurrentUser && e.Id == key);
        }

        [EnableQuery]
        public IQueryable<Workspace> Get()
        {
            logger.LogDebug($"Begin & End: WorkspacesController Get()");
            return base.dbContext.Workspaces.Where(e => e.TenantNavigation.MsId == this.msIdTenantCurrentUser);
        }
    }
}