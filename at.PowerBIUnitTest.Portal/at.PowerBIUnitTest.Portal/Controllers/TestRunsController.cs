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
    public class TestRunsController : BaseController
    {
        private readonly ILogger<TenantsController> logger;
        public TestRunsController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, ILogger<TenantsController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor)
        {
            this.logger = logger;
        }

        // GET: odata/TestRuns
        [EnableQuery]
        [AllowAnonymous]
        public IQueryable<TestRunCollection> Get()
        {
            logger.LogDebug($"Begin & End: TestRunssController Get()");
            return base.dbContext.TestRunCollections;
        }
    }
}
