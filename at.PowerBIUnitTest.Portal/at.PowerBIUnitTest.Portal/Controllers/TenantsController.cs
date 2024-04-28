using at.PowerBIUnitTest.Portal.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Web;
using System.Linq;
using Microsoft.Extensions.Logging;
using at.PowerBIUnitTest.Portal.Services;
using Microsoft.AspNet.OData;

namespace at.PowerBIUnitTest.Portal.Controllers
{
    public class TenantsController : BaseController
    {
        public TenantsController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, ILogger<TenantsController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor, logger)
        {
        }

        [EnableQuery]
        public IQueryable<Tenant> Get([FromODataUri] int key)
        {
            logger.LogDebug($"Begin & End: TenantsController Get(key: {key})");
            return base.dbContext.Tenants.Where(e => e.MsId == this.msIdTenantCurrentUser && e.Id == key);
        }

        [EnableQuery]
        public IQueryable<Tenant> Get()
        {
            logger.LogDebug($"Begin & End: TenantsController Get()");
            return base.dbContext.Tenants.Where(e => e.MsId == this.msIdTenantCurrentUser);
        }
    }
}
