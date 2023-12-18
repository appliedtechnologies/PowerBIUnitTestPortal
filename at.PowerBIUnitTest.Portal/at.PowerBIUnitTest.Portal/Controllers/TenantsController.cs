using at.PowerBIUnitTest.Portal.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Identity.Web;
using System.Linq;
using Microsoft.Extensions.Logging;
using at.PowerBIUnitTest.Portal.Services;

namespace at.PowerBIUnitTest.Portal.Controllers
{
    [Authorize]
    public class TenantsController : BaseController
    {
        private readonly ILogger<TenantsController> logger;
        public TenantsController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, ILogger<TenantsController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor)
        {
            this.logger = logger;
        }

        // GET: odata/Tenants
        [EnableQuery]
        public IQueryable<Tenant> Get()
        {
            logger.LogDebug($"Begin & End: TenantsController Get()");
            return base.dbContext.Tenants.Where(e => e.MsId == this.msIdTenantCurrentUser);
        }
    }
}
