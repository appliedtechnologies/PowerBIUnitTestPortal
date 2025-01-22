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
    public class PowerBiController : BaseController
    {
        public PowerBiController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<PowerBiController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor, logger)
        {
        }

         public IQueryable<Report> Get()
        {
            logger.LogDebug($"Begin & End: ReportController Get()");
            return base.dbContext.Reports.Where(p => p.TenantNavigation.MsId == msIdTenantCurrentUser);
        }
        
        [HttpGet("odata/GetEmbedToken(workspaceId={workspaceId},reportId={reportId})")]
        public async Task<IActionResult> GetEmbedToken([FromODataUri] Guid workspaceId,  [FromODataUri] Guid reportId,  [FromServices] PowerBiService powerBiService)
        {
            try
            {

                if (reportId == Guid.Empty || workspaceId == Guid.Empty)
                {
                    return BadRequest("Report ID or Workspace ID cannot be empty.");
                }

                var workspaceExists = this.dbContext.Workspaces.Any(e =>
                e.TenantNavigation.MsId == msIdTenantCurrentUser && e.MsId == workspaceId);

                if (!workspaceExists)
                {
                    return NotFound("Workspace not found or access is denied.");
                }

                string token = await powerBiService.GetEmbedToken(downstreamWebApi, msIdTenantCurrentUser, workspaceId, reportId);

                if (string.IsNullOrEmpty(token))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to generate embed token.");
                }

                return Ok(token);
            }
            catch (Exception ex)
            {
                
                logger.LogError(ex, "An error occurred while generating the embed token.");

                
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
       
    }
}