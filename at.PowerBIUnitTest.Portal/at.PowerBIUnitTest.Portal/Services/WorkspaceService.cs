using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using at.PowerBIUnitTest.Portal.Data.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace at.PowerBIUnitTest.Portal.Services
{
    public class WorkspaceService
    {
        private readonly ILogger logger;
        private readonly PortalDbContext dbContext;
        private readonly PowerBiService powerBiService;

        public WorkspaceService(ILogger<WorkspaceService> logger, PortalDbContext dbContext, PowerBiService powerBiService)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.powerBiService = powerBiService;
        }

        public async Task PullWorkspacesFromPowerBi(Guid msIdTenantCurrentUser, string accessToken)
        {
            logger.LogDebug($"Begin: WorkspaceService PullWorkspacesFromPowerBi(msIdTenantCurrentUser: {msIdTenantCurrentUser})");

            var powerBiToken = await powerBiService.GetTokenOnBehalfOf(msIdTenantCurrentUser, accessToken);

            var remoteWorkspaces = await powerBiService.LoadWorkspace(powerBiToken);
            List<Workspace> workspaces = new List<Workspace>();
            Tenant currentUsersTenant = this.dbContext.Tenants.First(e => e.MsId == msIdTenantCurrentUser);

            foreach (var remoteWorkspace in remoteWorkspaces)
            {
                var workspace = new Workspace
                {
                    MsId = remoteWorkspace["id"].ToObject<Guid>(),
                    Name = remoteWorkspace["name"].ToString(),
                    Tenant = currentUsersTenant.Id
                };
                workspaces.Add(workspace);
            }

            workspaces.ForEach(e =>
            {
                var dbWorkspace = this.dbContext.Workspaces.FirstOrDefault(w => w.MsId == e.MsId);

                if (dbWorkspace == null)
                {
                    this.dbContext.Workspaces.Add(e);
                }
                else
                {
                    dbWorkspace.Name = e.Name;
                    dbWorkspace.Tenant = e.Tenant;
                    dbContext.Update(dbWorkspace);
                }
            });

            await dbContext.SaveChangesAsync();

            logger.LogDebug($"End: WorkspaceService PullWorkspacesFromPowerBi(msIdTenantCurrentUser: {msIdTenantCurrentUser})");
        }
    }
}