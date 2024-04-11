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

namespace at.PowerBIUnitTest.Portal.Controllers
{
    public class HistoriesController : BaseController
    {
         private readonly IConfiguration configuration;
         private readonly ILogger<HistoriesController> logger;

        public HistoriesController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<HistoriesController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor)
        {
            this.configuration = configuration;
            this.logger = logger;
        }
        [EnableQuery]
        public IQueryable<TestRun> Get()
        {
            logger.LogDebug($"Begin & End: HistoriesController Get()");
            return base.dbContext.TestRuns;
        }
    }
}