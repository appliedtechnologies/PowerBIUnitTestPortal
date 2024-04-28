using at.PowerBIUnitTest.Portal.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using at.PowerBIUnitTest.Portal.Services;

namespace at.PowerBIUnitTest.Portal.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<UsersController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor, logger)
        {
        }

        [EnableQuery]
        public IQueryable<User> Get([FromODataUri] int key)
        {
            logger.LogDebug($"Begin & End: UsersController Get(key: {key})");
            return base.dbContext.Users.Where(e => e.TenantNavigation.MsId == this.msIdTenantCurrentUser && e.Id == key);
        }

        // GET: odata/Users
        [EnableQuery]
        public IQueryable<User> Get()
        {
            logger.LogDebug($"Begin & End: UsersController Get()");
            return base.dbContext.Users.Where(e => e.TenantNavigation.MsId == this.msIdTenantCurrentUser);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromServices] IConfiguration configuration)
        {
            logger.LogDebug($"Begin: UsersController Login()");

            if (!ModelState.IsValid)
                return BadRequest();

            //get information of current logged in user
            User currentUser = new User
            {
                MsId = Guid.Parse(this.HttpContext.User.FindFirst(ClaimConstants.ObjectId)?.Value),
                Firstname = this.HttpContext.User.FindFirst(ClaimTypes.GivenName)?.Value,
                Lastname = this.HttpContext.User.FindFirst(ClaimTypes.Surname)?.Value,
                Email = this.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value
            };

            if (string.IsNullOrEmpty(currentUser.Firstname))
                currentUser.Firstname = currentUser.Email;

            if (string.IsNullOrEmpty(currentUser.Lastname))
                currentUser.Lastname = string.Empty;

            Guid msIdTenantCurrentUser = Guid.Parse(this.HttpContext.User.FindFirst(ClaimConstants.TenantId).Value);

            //check if current user exists in database
            if (this.dbContext.Users.Any(e => e.MsId == currentUser.MsId))
            { //current user does exist in database
                currentUser = this.UpdateUserIfNeeded(currentUser);
            }
            else
            { //current user does NOT exist in database
                this.dbContext.Users.Add(currentUser);

                if (this.dbContext.Tenants.Any(e => e.MsId == msIdTenantCurrentUser))
                    currentUser.Tenant = this.dbContext.Tenants.First(e => e.MsId == msIdTenantCurrentUser).Id;
                else
                {
                    currentUser.TenantNavigation = await this.AddTenant(msIdTenantCurrentUser);
                }
            }

            await this.dbContext.SaveChangesAsync();
            logger.LogDebug($"End: UsersController Login()");
            return Ok();
        }

        private User UpdateUserIfNeeded(User currentUser)
        {
            logger.LogDebug($"Begin: UsersController UpdateUserIfNeeded()");

            User currentDbUser = this.dbContext.Users.First(e => e.MsId == currentUser.MsId);

            if (currentDbUser.Firstname != currentUser.Firstname)
                currentDbUser.Firstname = currentUser.Firstname;

            if (currentDbUser.Lastname != currentUser.Lastname)
                currentDbUser.Lastname = currentUser.Lastname;

            if (currentDbUser.Email != currentUser.Email)
                currentDbUser.Email = currentUser.Email;

            logger.LogDebug($"End: UsersController UpdateUserIfNeeded()");

            return currentDbUser;
        }

        private async Task<Tenant> AddTenant(Guid msId)
        {
            logger.LogDebug($"Begin: UsersController AddTenant(msId = {msId.ToString()})");
            string tenantName = await this.GetTenantName(msId);

            Tenant newTenant = new Tenant
            {
                Name = tenantName,
                MsId = msId
            };

            this.dbContext.Tenants.Add(newTenant);
            logger.LogDebug($"End: UsersController AddTenant()");
            return newTenant;
        }

        private async Task<string> GetTenantName(Guid msId)
        {
            logger.LogDebug($"Begin: UsersController GetTenantName(msId = {msId.ToString()})");
            var tenantResponse = await this.downstreamWebApi.CallWebApiForUserAsync(
                "AzureManagementApi",
                options =>
                {
                    options.RelativePath = "tenants?api-version=2020-01-01";
                });

            JToken tenants = (await tenantResponse.Content.ReadAsAsync<JObject>())["value"];

            for (int i = 0; i < tenants.Count(); i++)
            {
                if (Guid.Parse((string)tenants[i]["tenantId"]) == msId){
                    var tenantName = (string)tenants[i]["displayName"];
                    logger.LogDebug($"End: UsersController GetTenantName(return = {tenantName})");

                    return tenantName;
                }        
            }

            //no tenant with msId was found
            throw new System.Exception($"can not find display name of tenant with id '{msId}'");
        }
    }
}
