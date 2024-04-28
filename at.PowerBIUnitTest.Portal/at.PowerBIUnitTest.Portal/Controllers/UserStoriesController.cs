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
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.EntityFrameworkCore;
using at.PowerBIUnitTest.Portal.Services;
using System.Collections.Generic;

namespace at.PowerBIUnitTest.Portal.Controllers
{

    [Authorize]
    public class UserStoriesController : BaseController
    {
        public UserStoriesController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<UsersController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor, logger)
        {
        }

        [EnableQuery]
        public IQueryable<UserStory> Get([FromODataUri] int key)
        {
            logger.LogDebug($"Begin & End: UserStoriesController Get(key: {key})");
            return base.dbContext.UserStories.Where(e => e.TabularModelNavigation.WorkspaceNavigation.TenantNavigation.MsId == this.msIdTenantCurrentUser && e.Id == key);
        }

        [EnableQuery]
        public IQueryable<UserStory> Get()
        {
            logger.LogDebug($"Begin & End: UserStoriesController Get()");
            return base.dbContext.UserStories.Where(e => e.TabularModelNavigation.WorkspaceNavigation.TenantNavigation.MsId == this.msIdTenantCurrentUser);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserStory userStory)
        {
            logger.LogDebug($"Begin: UserStoriesController Post(unitTest Description: {userStory.Name})");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if ((await this.dbContext.TabularModels.FirstOrDefaultAsync(e => e.Id == userStory.TabularModel && e.WorkspaceNavigation.TenantNavigation.MsId == this.msIdTenantCurrentUser)) == null)
                return Forbid();

            this.dbContext.UserStories.Add(userStory);
            await this.dbContext.SaveChangesAsync();

            logger.LogDebug($"End: UserStoriesController Post(unitTest Description: {userStory.Name})");

            return Created(userStory);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            logger.LogDebug($"Begin: UserStoriesController Delete(key: {key}");

            var userStory = await this.dbContext.UserStories.FindAsync(key);

            if (userStory == null)
                return NotFound();

            if (userStory.TabularModelNavigation.WorkspaceNavigation.TenantNavigation.MsId != this.msIdTenantCurrentUser)
                return Forbid();

            this.dbContext.UserStories.Remove(userStory);
            await base.dbContext.SaveChangesAsync();

            logger.LogDebug($"End: UserStoriesController Delete(key: {key}");
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<UserStory> userStory)
        {
            logger.LogDebug($"Begin: UserStoriesController Patch(key: {key}, userStory: {userStory.GetChangedPropertyNames()}");

            if ((await this.dbContext.UserStories.FirstOrDefaultAsync(e => e.Id == key && e.TabularModelNavigation.WorkspaceNavigation.TenantNavigation.MsId == this.msIdTenantCurrentUser)) == null)
                return Forbid();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await base.dbContext.UserStories.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            userStory.Patch(entity);
            await base.dbContext.SaveChangesAsync();

            logger.LogDebug($"End: UserStoriesController Patch(key: {key}, userStory: {userStory.GetChangedPropertyNames()}");

            return Updated(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Copy([FromODataUri] int key, ODataActionParameters parameters)
        {
            logger.LogDebug($"Begin: UserStoriesController Copy(key: {key})");

            int targetTabularModelId = (int)parameters["targetTabularModelId"];

            var originalUserStory = await base.dbContext.UserStories.FirstOrDefaultAsync(e => e.Id == key && e.TabularModelNavigation.WorkspaceNavigation.TenantNavigation.MsId == this.msIdTenantCurrentUser);
            var tabularModel = await base.dbContext.TabularModels.FirstOrDefaultAsync(e => e.Id == targetTabularModelId && e.WorkspaceNavigation.TenantNavigation.MsId == this.msIdTenantCurrentUser);

            if (originalUserStory == null || tabularModel == null)
            {
                return NotFound();
            }

            var copiedUserStory = new UserStory
            {
                Name = originalUserStory.Name,
                TabularModel = targetTabularModelId,
            };

            var copiedUnitTests = new List<UnitTest>();

            foreach (var originalUnitTest in originalUserStory.UnitTests)
            {
                var copiedUnitTest = new UnitTest
                {
                    Name = originalUnitTest.Name,
                    DAX = originalUnitTest.DAX,
                    ExpectedResult = originalUnitTest.ExpectedResult,
                    ResultType = originalUnitTest.ResultType,
                    DateTimeFormat = originalUnitTest.DateTimeFormat,
                    DecimalPlaces = originalUnitTest.DecimalPlaces,
                    FloatSeparators = originalUnitTest.FloatSeparators,                    
                };

                copiedUnitTests.Add(copiedUnitTest);
            }

            copiedUserStory.UnitTests = copiedUnitTests;
            base.dbContext.UserStories.Add(copiedUserStory);
            await dbContext.SaveChangesAsync();

            logger.LogDebug($"End: UserStoriesController Copy()");
            return Ok();
        }
    }
}
