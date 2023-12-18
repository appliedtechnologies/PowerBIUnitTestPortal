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
        private readonly IConfiguration configuration;
        private readonly ILogger<TabularModelsController> logger;
        public TabularModelsController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<TabularModelsController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

       
        [EnableQuery(MaxExpansionDepth = 4)]
        public IQueryable<TabularModel> Get()
        {
            try
            {
                logger.LogDebug($"Begin & End: TabularModelsController Get()");
            return base.dbContext.TabularModels;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occured while performig GET(TabModel)");
                throw;
            }
        }   

    }


}