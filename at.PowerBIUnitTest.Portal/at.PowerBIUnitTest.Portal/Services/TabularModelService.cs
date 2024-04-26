using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using at.PowerBIUnitTest.Portal.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

public class TabularModelService
{
    private readonly ILogger logger;
    private readonly PortalDbContext dbContext;
    private readonly PowerBiService powerBiService;

    public TabularModelService(ILogger<TabularModelService> logger, PortalDbContext dbContext, PowerBiService powerBiService)
    {
        this.logger = logger;
        this.dbContext = dbContext;
        this.powerBiService = powerBiService;
    }

    public async Task PullDatasetsFromPowerBi(Guid msIdTenantCurrentUser, string accessToken)
    {
        logger.LogDebug($"Begin: TabularModelService PullDatasetsFromPowerBi(msIdTenantCurrentUser: {msIdTenantCurrentUser})");

        var powerBiToken = await powerBiService.GetTokenOnBehalfOf(msIdTenantCurrentUser, accessToken);

        foreach(var workspace  in dbContext.Workspaces.Where(e => e.TenantNavigation.MsId == msIdTenantCurrentUser).ToList())
        {
            var remoteDatasets = await powerBiService.LoadDataset(powerBiToken, workspace.MsId);
            List<TabularModel> tabularModels = new List<TabularModel>();

            foreach (var remoteDataset in remoteDatasets)
            {
                var tabularModel = new TabularModel
                {
                    MsId = remoteDataset["id"].ToObject<Guid>(),
                    Name = remoteDataset["name"].ToString(),
                    Workspace = workspace.Id
                };
                tabularModels.Add(tabularModel);
            }

            tabularModels.ForEach(e =>
            {
                var dbTabularModel = this.dbContext.TabularModels.FirstOrDefault(t => t.MsId == t.MsId);

                if (dbTabularModel == null)
                {
                    this.dbContext.TabularModels.Add(e);
                }
                else
                {
                    dbTabularModel.Name = e.Name;
                    dbTabularModel.Workspace = e.Workspace;
                    dbContext.Update(dbTabularModel);
                }
            });
        };

        await dbContext.SaveChangesAsync();

        logger.LogDebug($"End: TabularModelService PullDatasetsFromPowerBi(msIdTenantCurrentUser: {msIdTenantCurrentUser})");
    }
}