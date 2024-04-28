using at.PowerBIUnitTest.Portal.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Identity.Web;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Net.Http.Headers;

namespace at.PowerBIUnitTest.Portal.Controllers
{
    [Authorize]
    public abstract class BaseController : ODataController
    {
        protected ILogger logger;
        protected PortalDbContext dbContext;
        protected IDownstreamWebApi downstreamWebApi;

        protected Guid msIdTenantCurrentUser;
        protected Guid msIdCurrentUser;
        protected IHttpContextAccessor httpContextAccessor;


        public BaseController(PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, ILogger logger)
        {
            dbContext = portalDbContext;
            this.downstreamWebApi = downstreamWebApi;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
            this.msIdTenantCurrentUser = Guid.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimConstants.TenantId).Value);
            this.msIdCurrentUser = Guid.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimConstants.ObjectId).Value);
            this.dbContext.MsIdCurrentUser = this.msIdCurrentUser;
        }
    }
}
