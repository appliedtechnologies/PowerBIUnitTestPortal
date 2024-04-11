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

namespace at.PowerBIUnitTest.Portal.Controllers
{

    [Authorize]
    public class UserStoriesController : BaseController
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<UsersController> logger;
        public UserStoriesController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<UsersController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor)
        {
            this.configuration = configuration;
            this.logger = logger;
        }


        [EnableQuery]
        public IQueryable<UserStory> Get()
        {
            logger.LogDebug($"Begin & End: UserStoriesController Get()");
            return base.dbContext.UserStories;
        }

        // Add odata/UserStory
        [HttpPost]
        public UserStory Post([FromBody] UserStory userStory)
        {
            try
            {
                logger.LogDebug($"Begin: UserStoriesController Post()");
                var newUserStory = base.dbContext.Add(userStory);
                base.dbContext.SaveChanges();
                logger.LogDebug($"End: UserStoriesController Post()");
                return newUserStory.Entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured while adding a User Story");
                throw;
            }
        }

        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<UserStory> userStory)
        {
            try
            {
                logger.LogDebug($"Begin: UnitTestsController Patch()");
                var userStoryToChange = await base.dbContext.UserStories.FindAsync(key);

                if (userStoryToChange == null)
                {
                    return NotFound();
                }

                userStory.Patch(userStoryToChange);
                base.dbContext.SaveChanges();

                logger.LogDebug($"End: UnitTestsController Patch()");
                return Updated(userStoryToChange);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured while performing PATCH");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Copy([FromODataUri] int key, ODataActionParameters parameters)
        {
            logger.LogDebug($"Begin: UserStoriesController Copy(key: {key})");
            /*
            var solution = await this.dbContext.UserStories.FirstOrDefaultAsync(e => e.Id == key && e.TabularModelNavigation.WorkspaceNavigation..DevelopmentEnvironmentNavigation.TenantNavigation.MsId == this.msIdTenantCurrentUser);
            if (solution == null)
                return Forbid();

            if (solution.IsPatch())
                return BadRequest("Can't apply upgrade for patch solution");

            int targetEnvironmentId = (int)parameters["targetEnvironmentId"];
            if (ImportExistsOnEnvironment(key, targetEnvironmentId) == false)
                return BadRequest("Can't skip import before applying an upgrade");

            Data.Models.Action createdAction;

            try
            {
                createdAction = await solutionService.AddApplyUpgradeAction(key, targetEnvironmentId, this.msIdCurrentUser);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            logger.LogDebug($"End: SolutionsController ApplyUpgrade()");
            */


            return Ok();
        }

    //     [HttpPost]
    //     public async Task<IActionResult> Copy2([FromODataUri] int key, ODataActionParameters parameters)
    //     {
    //         try
    //         {
    //             int targetTabularModelId = (int)parameters["targetTabularModelId1"];
    //             int targetWorkspaceId = (int)parameters["targetWorkspaceId1"];
    //             int originalUserStoryId = (int)parameters["userStoryId1"];

    //             // 1. UserStory kopieren
    //             var originalUserStory = await base.dbContext.UserStories
    //                 .Include(us => us.UnitTests)  // Include, um die verknüpften UnitTests abzurufen
    //                 .FirstOrDefaultAsync(us => us.Id == originalUserStoryId);

    //             if (originalUserStory == null)
    //             {
    //                 return NotFound();
    //             }

    //             var copiedUserStory = new UserStory
    //             {
    //                 Beschreibung = originalUserStory.Beschreibung,
    //                 TabularModel = targetTabularModelId,
    //             };

    //             base.dbContext.UserStories.Add(copiedUserStory);
    //             base.dbContext.SaveChanges();

    //             // 2. UnitTests kopieren
    //             foreach (var originalUnitTest in originalUserStory.UnitTests)
    //             {
    //                 var copiedUnitTest = new UnitTest
    //                 {
    //                     // Kopieren Sie alle erforderlichen Eigenschaften des UnitTests
    //                     // ... 

    //                     // Aktualisieren Sie die Beziehung zur kopierten UserStory
    //                     Name = originalUnitTest.Name,
    //                     DAX = originalUnitTest.DAX,
    //                     ExpectedResult = originalUnitTest.ExpectedResult,
    //                     ResultType = originalUnitTest.ResultType,
    //                     DateTimeFormat = originalUnitTest.DateTimeFormat,
    //                     DecimalPlaces = originalUnitTest.DecimalPlaces,
    //                     FloatSeparators = originalUnitTest.FloatSeparators,
    //                     Timestamp = originalUnitTest.Timestamp,
    //                     UserStory = copiedUserStory.Id,
    //                 };

    //                 dbContext.UnitTests.Add(copiedUnitTest);
    //             }

    //             dbContext.SaveChanges();

    //             logger.LogDebug($"End: UserStoriesController Copy()");
    //             return Ok();
    //         }
    //         catch (Exception ex)
    //         {
    //             logger.LogError(ex, "An error occurred while copying UserStory");
    //             throw;
    //         }
    //     }

    }


}
