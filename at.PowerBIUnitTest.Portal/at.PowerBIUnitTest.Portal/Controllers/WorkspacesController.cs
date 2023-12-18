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

namespace at.PowerBIUnitTest.Portal.Controllers
{

    public class WorkspacesController : BaseController
    {
        private readonly IConfiguration configuration;

        private IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<WorkspacesController> logger;
        public WorkspacesController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<WorkspacesController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor)
        {
            this.configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        [EnableQuery(MaxExpansionDepth = 4)]
        public IQueryable<Workspace> FilterWorkspace([FromServices] PowerBiService powerBiService)
        {
            logger.LogDebug($"Begin: WorkspaceController FilterWorkspace()");
            try
            {
                logger.LogDebug("Filtering Workspace....");
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string authToken = powerBiService.GetTokenOnBehalfOf(base.msIdTenantCurrentUser, accessToken).Result;
            string jsonResponse;
            Workspace[] abcd = new Workspace[0];
            //Root TestValue;

            if (powerBiService.LoadWorkspace(authToken, out jsonResponse)){
                Root TestValue = new Root();
                TestValue = JsonConvert.DeserializeObject<Root>(jsonResponse);
                int Zähler;
                Zähler = TestValue.OdataCount;
                abcd = new Workspace[Zähler];

                for (int i = 0; i <= Zähler - 1; i++){

                    Workspace Füller;
                    TabularModel[] FüllerTB;

                    Füller = new Workspace();
                    FüllerTB = new TabularModel[1];
                    Root3 TestDataset = new Root3();

                    if(powerBiService.LoadDataset(TestValue.Values.ElementAt(i).id, authToken, out jsonResponse)){
                        TestDataset = JsonConvert.DeserializeObject<Root3>(jsonResponse);
                        int zähler2;
                        zähler2 = TestDataset.Values.Count();

                        FüllerTB = new TabularModel[zähler2];
                        TabularModel FüllerTB2 = new TabularModel();
                        for (int k = 0; k <= zähler2 - 1; k++){
                            FüllerTB2.DatasetPbId = TestDataset.Values.ElementAt(k).id;
                            //FüllerTB2.Id = this.dbContext.TabularModels.Where(p => p.DatasetPbId == FüllerTB2.DatasetPbId).FirstOrDefault().Id;

                            FüllerTB[k] = FüllerTB2;
                        }
                    }

                    Füller.WorkspacePbId = TestValue.Values.ElementAt(i).id;
                    Füller.Name = TestValue.Values.ElementAt(i).name;
                   // Füller.Id = TestValue.Values.ElementAt(i).id;
                    Füller.TabularModels = FüllerTB;
                    abcd[i] = Füller;
                }

                IQueryable<Workspace> qtest = abcd.AsQueryable();

                return qtest;
            }

            Workspace[] dings = new Workspace[1];
            
            
            
            var workspaces = dings.AsQueryable();
            logger.LogDebug($"End: WorkspaceController FilterWorkspace(Return: {workspaces})");
            return workspaces;
            
            
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occured while performing FilterWorkspace");
                throw;
            }
        }


        [EnableQuery(MaxExpansionDepth = 4)]
        public IQueryable<Workspace> Get([FromServices] PowerBiService powerBiService){
            /*var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
             string authToken = powerBiService.GetTokenOnBehalfOf(accessToken).Result;
             string jsonResponse;

             if(powerBiService.LoadWorkspace(authToken, out jsonResponse))
             {
             Root TestValue = new Root();
             TestValue = JsonConvert.DeserializeObject<Root>(jsonResponse);
             int Zähler2;
             Zähler2 = TestValue.OdataCount;


             Workspace[] Filter = new Workspace[Zähler2];
             //FilterWorkspace(powerBiService, out Filter);
             string[] TestFilter = new string[0];


             return base.dbContext.Workspaces;*/
            logger.LogDebug($"Begin & End: WorkspacesController Get()");

            return base.dbContext.Workspaces;

        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            try
            {
                logger.LogDebug($"Begin: UnitTestsController Delete()");
                var workspace = await dbContext.Workspaces.FindAsync(key);

                if (workspace == null)
                {
                    return NotFound();
                }

                base.dbContext.Remove(workspace);
                base.dbContext.SaveChanges();

                logger.LogDebug($"End: UnitTestsController Delete()");
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured while performing DELETE");
                throw;
            }
        }
    }
}