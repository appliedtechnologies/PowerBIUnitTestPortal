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

namespace at.PowerBIUnitTest.Portal.Controllers
{

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
            catch(Exception ex)
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
    }


}
