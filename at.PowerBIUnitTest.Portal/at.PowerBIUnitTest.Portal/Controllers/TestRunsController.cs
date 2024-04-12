using System.Linq;
using at.PowerBIUnitTest.Portal.Data.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;

namespace at.PowerBIUnitTest.Portal.Controllers
{
    public class TestRunsController : BaseController
    {
        public TestRunsController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<TestRunsController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor, logger)
        {
        }

        [EnableQuery]
        public IQueryable<TestRun> Get([FromODataUri] int key)
        {
            logger.LogDebug($"Begin & End: TestRunsController Get(key: {key})");
            return base.dbContext.TestRuns.Where(e => e.UnitTestNavigation.UserStoryNavigation.TabularModelNavigation.WorkspaceNavigation.TenantNavigation.MsId == this.msIdTenantCurrentUser && e.Id == key);
        }

        [EnableQuery]
        public IQueryable<TestRun> Get()
        {
            logger.LogDebug($"Begin & End: TestRunsController Get()");
            return base.dbContext.TestRuns.Where(e => e.UnitTestNavigation.UserStoryNavigation.TabularModelNavigation.WorkspaceNavigation.TenantNavigation.MsId == this.msIdTenantCurrentUser);
        }
    }
}