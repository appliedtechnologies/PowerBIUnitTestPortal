using at.PowerBIUnitTest.Portal.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Identity.Web;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.OData.Formatter;
using at.PowerBIUnitTest.Portal.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData.Deltas;

namespace at.PowerBIUnitTest.Portal.Controllers
{

    public class WorkspacesController : BaseController
    {
        public WorkspacesController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<WorkspacesController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor, logger)
        {
        }

        [EnableQuery(MaxExpansionDepth = 4)]
        public IQueryable<Workspace> Get([FromODataUri] int key)
        {
            logger.LogDebug($"Begin & End: WorkspacesController Get(key: {key})");
            return base.dbContext.Workspaces.Where(e => e.TenantNavigation.MsId == this.msIdTenantCurrentUser && e.Id == key);
        }

        [EnableQuery(MaxExpansionDepth = 4)]
        public IQueryable<Workspace> Get()
        {
            logger.LogDebug($"Begin & End: WorkspacesController Get()");
            return base.dbContext.Workspaces.Where(e => e.TenantNavigation.MsId == this.msIdTenantCurrentUser);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            logger.LogDebug($"Begin: WorkspacesController Delete(key: {key}");

            var workspace = await this.dbContext.Workspaces.FindAsync(key);

            if (workspace == null)
                return NotFound();

            if (workspace.TenantNavigation.MsId != this.msIdTenantCurrentUser)
                return Forbid();

            this.dbContext.Workspaces.Remove(workspace);
            await base.dbContext.SaveChangesAsync();

            logger.LogDebug($"End: WorkspacesController Delete(key: {key}");
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<Workspace> workspace)
        {
            logger.LogDebug($"Begin: WorkspacesController Patch(key: {key}, workspace: {workspace.GetChangedPropertyNames()}");

            if ((await this.dbContext.Workspaces.FirstOrDefaultAsync(e => e.Id == key && e.TenantNavigation.MsId == this.msIdTenantCurrentUser)) == null)
                return Forbid();

            string[] propertyNamesAllowedToChange = { "IsVisible" };
            if (workspace.GetChangedPropertyNames().Except(propertyNamesAllowedToChange).Count() == 0)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = await base.dbContext.Workspaces.FindAsync(key);
                if (entity == null)
                {
                    return NotFound();
                }

                workspace.Patch(entity);
                await base.dbContext.SaveChangesAsync();

                logger.LogDebug($"End: WorkspacesController Patch(key: {key}, workspace: {workspace.GetChangedPropertyNames()}");

                return Updated(entity);
            }
            else
            {
                logger.LogDebug($"End: WorkspacesController Patch(key: {key}, workspace: {workspace.GetChangedPropertyNames()}");
                return BadRequest();
            }
        }


        [HttpPost]
        public async Task<IActionResult> Pull([FromServices] WorkspaceService workspaceService, [FromServices] TabularModelService tabularModelService)
        {
            logger.LogDebug($"Begin: WorkspacesController Pull()");

            var accessToken = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            await workspaceService.PullWorkspacesFromPowerBi(msIdTenantCurrentUser, accessToken);
            await tabularModelService.PullDatasetsFromPowerBi(msIdTenantCurrentUser, accessToken);

            logger.LogDebug($"End: WorkspacesController Pull()");
            return Ok();
        }
    }
}