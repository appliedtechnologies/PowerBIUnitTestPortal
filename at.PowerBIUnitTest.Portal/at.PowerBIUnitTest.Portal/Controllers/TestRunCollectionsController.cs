using System.Linq;
using at.PowerBIUnitTest.Portal.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;

namespace at.PowerBIUnitTest.Portal.Controllers
{
    public class TestRunCollectionsController : BaseController
    {
        public TestRunCollectionsController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<TestRunCollectionsController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor, logger)
        {
        }

        [EnableQuery]
        public IQueryable<TestRunCollection> Get([FromODataUri] int key)
        {
            logger.LogDebug($"Begin & End: TestRunCollectionsController Get(key: {key})");
            return base.dbContext.TestRunCollections.Where(e => e.TestRuns.Count > 0 && e.TestRuns.First().UnitTestNavigation.UserStoryNavigation.TabularModelNavigation.WorkspaceNavigation.TenantNavigation.MsId == this.msIdTenantCurrentUser && e.Id == key);
        }

        [EnableQuery]
        public IQueryable<TestRunCollection> Get()
        {
            logger.LogDebug($"Begin & End: TestRunCollectionsController Get()");
            return base.dbContext.TestRunCollections.Where(e => e.TestRuns.Count > 0 && e.TestRuns.First().UnitTestNavigation.UserStoryNavigation.TabularModelNavigation.WorkspaceNavigation.TenantNavigation.MsId == this.msIdTenantCurrentUser);
        }
    }
}
