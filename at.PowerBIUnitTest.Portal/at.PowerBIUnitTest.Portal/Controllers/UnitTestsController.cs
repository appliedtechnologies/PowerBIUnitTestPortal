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
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData;
using at.PowerBIUnitTest.Portal.Services;

namespace at.PowerBIUnitTest.Portal.Controllers
{

    [Authorize]
    public class UnitTestsController : BaseController
    {
        public UnitTestsController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<UsersController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor, logger)
        {

        }

        [EnableQuery(MaxExpansionDepth = 4)]
        public IQueryable<UnitTest> Get([FromODataUri] int key)
        {
            logger.LogDebug($"Begin & End: UnitTestsController Get(key: {key})");
            return base.dbContext.UnitTests.Where(e => e.UserStoryNavigation.TabularModelNavigation.WorkspaceNavigation.TenantNavigation.MsId == this.msIdTenantCurrentUser && e.Id == key);
        }

        [EnableQuery(MaxExpansionDepth = 4)]
        public IQueryable<UnitTest> Get()
        {
            logger.LogDebug($"Begin & End: UnitTestsController Get()");
            return base.dbContext.UnitTests.Where(e => e.UserStoryNavigation.TabularModelNavigation.WorkspaceNavigation.TenantNavigation.MsId == this.msIdTenantCurrentUser);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UnitTest unitTest)
        {
            logger.LogDebug($"Begin: UnitTestsController Post(unitTest Name: {unitTest.Name})");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if ((await this.dbContext.UserStories.FirstOrDefaultAsync(e => e.Id == unitTest.UserStory && e.TabularModelNavigation.WorkspaceNavigation.TenantNavigation.MsId == this.msIdTenantCurrentUser)) == null)
                return Forbid();

            if (base.dbContext.UnitTests.Any(e => e.Name == unitTest.Name && e.UserStory == unitTest.UserStory))
                return BadRequest(new ODataError { ErrorCode = "400", Message = "Unit test with the same name already exists in the user story." });

            this.dbContext.UnitTests.Add(unitTest);
            await this.dbContext.SaveChangesAsync();

            logger.LogDebug($"End: UnitTestsController Post(unitTest Name: {unitTest.Name})");

            return Created(unitTest);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            logger.LogDebug($"Begin: UnitTestsController Delete(key: {key}");

            var unitTest = await this.dbContext.UnitTests.FindAsync(key);

            if (unitTest == null)
                return NotFound();

            if (unitTest.UserStoryNavigation.TabularModelNavigation.WorkspaceNavigation.TenantNavigation.MsId != this.msIdTenantCurrentUser)
                return Forbid();

            this.dbContext.UnitTests.Remove(unitTest);
            await base.dbContext.SaveChangesAsync();

            logger.LogDebug($"End: UnitTestsController Delete(key: {key}");
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<UnitTest> unitTest)
        {
            logger.LogDebug($"Begin: UnitTestsController Patch(key: {key}, unitTest: {unitTest.GetChangedPropertyNames()}");

            if ((await this.dbContext.UnitTests.FirstOrDefaultAsync(e => e.Id == key && e.UserStoryNavigation.TabularModelNavigation.WorkspaceNavigation.TenantNavigation.MsId == this.msIdTenantCurrentUser)) == null)
                return Forbid();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await base.dbContext.UnitTests.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            unitTest.Patch(entity);

            if (base.dbContext.UnitTests.Any(e => e.Name == entity.Name && e.UserStory == entity.UserStory && e.Id != entity.Id))
                return BadRequest(new ODataError { ErrorCode = "400", Message = "Unit test with the same name already exists in the user story." });

            await base.dbContext.SaveChangesAsync();

            logger.LogDebug($"End: UnitTestsController Patch(key: {key}, unitTest: {unitTest.GetChangedPropertyNames()}");

            return Updated(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Execute([FromBody] ODataActionParameters parmeters, [FromServices] UnitTestService unitTestService)
        {
            logger.LogDebug($"Begin: UnitTestsController Execute()");

            var unitTestIds = parmeters["unitTestIds"] as IEnumerable<int>;

            var accessToken = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            await unitTestService.ExecuteMultipe(msIdTenantCurrentUser, accessToken, unitTestIds);

            logger.LogDebug($"End: UnitTestsController Execute()");
            return Ok();
        }
    }
}
