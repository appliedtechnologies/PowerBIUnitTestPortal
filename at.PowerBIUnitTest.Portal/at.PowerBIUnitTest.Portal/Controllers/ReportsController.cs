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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData.Deltas;

namespace at.PowerBIUnitTest.Portal.Controllers
{

    public class ReportsController : BaseController
    {
        public ReportsController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<ReportsController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor, logger)
        {
        }

        [EnableQuery(MaxExpansionDepth = 4)]
        [HttpGet]
        public IQueryable<Report> Get()
        {
            logger.LogDebug($"Begin & End: ReportController Get()");
            return base.dbContext.Reports.Where(p => p.TenantNavigation.MsId == msIdTenantCurrentUser);
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<Report> report)
        {
            logger.LogDebug($"Begin: reportController Patch(key: {key}, workspace: {report.GetChangedPropertyNames()}");

            if ((await this.dbContext.Reports.FirstOrDefaultAsync(e => e.Id == key && e.TenantNavigation.MsId == this.msIdTenantCurrentUser)) == null)
                return Forbid();

            string[] propertyNamesAllowedToChange = { "Name", "ReportId", "WorkspaceId" };
            if (report.GetChangedPropertyNames().Except(propertyNamesAllowedToChange).Count() == 0)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = await base.dbContext.Reports.FindAsync(key);
                if (entity == null)
                {
                    return NotFound();
                }

                report.Patch(entity);
                await base.dbContext.SaveChangesAsync();

                logger.LogDebug($"End: ReportController Patch(key: {key}, report: {report.GetChangedPropertyNames()}");

                return Updated(entity);
            }
            else
            {
                logger.LogDebug($"End: ReportController Patch(key: {key}, report: {report.GetChangedPropertyNames()}");
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Report report)
        {
            logger.LogDebug("Begin: ReportController Add(report)");
            report.Tenant = this.dbContext.Tenants.Where(p => p.MsId == this.msIdTenantCurrentUser).FirstOrDefault().Id;

            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for adding report.");
                return BadRequest(ModelState);
            }

            try
            {
                await this.dbContext.Reports.AddAsync(report);
                await this.dbContext.SaveChangesAsync();

                logger.LogDebug($"End: ReportController Add(report) - Successfully added report with ID {report.Id}");

                return Created(report);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error occurred while adding report: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            logger.LogDebug($"Begin: ReportController Delete(key: {key}");

            var reports = await this.dbContext.Reports.FindAsync(key);

            if (reports == null)
                return NotFound();

            if (reports.TenantNavigation.MsId != this.msIdTenantCurrentUser)
                return Forbid();

            this.dbContext.Reports.Remove(reports);
            await base.dbContext.SaveChangesAsync();

            logger.LogDebug($"End: ReportController Delete(key: {key}");
            return Ok();
        }

        [HttpGet("odata/GetEmbedToken(workspaceId={workspaceId},reportId={reportId})")]
        public async Task<IActionResult> GetEmbedToken([FromODataUri] Guid workspaceId, [FromODataUri] Guid reportId, [FromServices] PowerBiService powerBiService)
        {
            try
            {
                if (reportId == Guid.Empty || workspaceId == Guid.Empty)
                {
                    return BadRequest("Report ID or Workspace ID cannot be empty.");
                }

                var workspaceExists = this.dbContext.Workspaces.Any(e => e.TenantNavigation.MsId == msIdTenantCurrentUser && e.MsId == workspaceId);

                if (!workspaceExists)
                {
                    return NotFound("Workspace not found or access is denied.");
                }

                string token = await powerBiService.GetEmbedToken(downstreamWebApi, msIdTenantCurrentUser, workspaceId, reportId, msUserNameCurrentUser);

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