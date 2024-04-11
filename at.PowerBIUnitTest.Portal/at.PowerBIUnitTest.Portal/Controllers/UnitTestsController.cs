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
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace at.PowerBIUnitTest.Portal.Controllers
{

    [Authorize]
    public class UnitTestsController : BaseController
    {
        private readonly IConfiguration configuration;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UnitTestsController> logger;
        private static List<TestRun> HistoriesForTestRun = new List<TestRun>();
        public UnitTestsController(Data.Models.PortalDbContext portalDbContext, IDownstreamWebApi downstreamWebApi, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<UnitTestsController> logger) : base(portalDbContext, downstreamWebApi, httpContextAccessor)
        {
            this.configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }
        
    //     // GET: odata/UnitTests
    //     [EnableQuery]
    //     public IQueryable<UnitTest> Get()
    //     {
    //         try
    //         {
    //             logger.LogDebug($"Begin & End: UnitTestsController Get()");
    //             return base.dbContext.UnitTests;
    //         }
    //         catch (Exception ex)
    //         {
    //             logger.LogError(ex, "An error occured while performing GET");
    //             throw;
    //         }
    //     }

    //     // Add odata/UnitTests
    //     [HttpPost]
    //     public UnitTest Post([FromBody] UnitTest unitTest)
    //     {
    //         try
    //         {
    //             logger.LogDebug($"Begin & End: UnitTestsController Post()");
    //             var newUnitTest = base.dbContext.Add(unitTest);
    //             base.dbContext.SaveChanges();
                
    //             return newUnitTest.Entity;
    //         }
    //         catch (Exception ex)
    //         {
    //             logger.LogError(ex, "An error occured while performing POST");
    //             throw;

    //         }
    //     }

    //     [AllowAnonymous]
    //     [HttpPost]
    //    public async Task<IActionResult> SaveTestRun(ODataActionParameters parmeters)
    //     {
    //        var newTestRun = new TestRunCollection();
    //         newTestRun.Count = Convert.ToInt32(parmeters["Count"]);
    //         newTestRun.WasPassed = parmeters["Result"].ToString();
    //         newTestRun.TimeStamp = DateTime.Now.ToString();


    //         if(parmeters["Type"].ToString() == "Workspace")
    //             {
    //                 newTestRun.Type = parmeters["Type"].ToString();
    //                 newTestRun.Workspace = parmeters["Name"].ToString();
    //             }
    //         if(parmeters["Type"].ToString() == "TabularModel")
    //             {
    //                 newTestRun.Type = parmeters["Type"].ToString();
    //                 newTestRun.TabularModel = parmeters["Name"].ToString();
    //             }
    //         if(parmeters["Type"].ToString() == "UserStory")
    //             {
    //                 newTestRun.Type = parmeters["Type"].ToString();
    //                 newTestRun.UserStory = parmeters["Name"].ToString();
    //             }

    //         base.dbContext.Add(newTestRun);
    //         base.dbContext.SaveChanges();
    //         //var Zähler = base.dbContext.Histories.Where(p => p.TimeStamp == newTestRun.TimeStamp).Count();
    //         /*for(int i = 0; i <= Zähler; i++)
    //         {
    //             var newHistory = new History();
    //             newHistory = base.dbContext.Histories.Where(p => p.TimeStamp == newTestRun.TimeStamp).Where(p => p.TestRun == null).FirstOrDefault();
    //             newHistory.TestRun = newTestRun.Id;
    //             base.dbContext.Update(newHistory);
    //             base.dbContext.SaveChanges();
    //         }*/

    //         foreach(var History in HistoriesForTestRun)
    //         {
    //             var newHistory = new TestRun();
    //             newHistory = base.dbContext.TestRuns.Where(p => p.Id == History.Id).FirstOrDefault();
    //             newHistory.TestRun = newTestRun.Id;
    //             base.dbContext.Update(newHistory);
    //             base.dbContext.SaveChanges();
                
    //         }
    //         HistoriesForTestRun.Clear();
    //         if(base.dbContext.TestRunCollections.Where(P => P.Id == newTestRun.Id).FirstOrDefault().TestRuns.All(p => p.WasPassed == "True"))
    //         {
    //             newTestRun.WasPassed = "True";
    //         }
    //         else
    //         newTestRun.WasPassed = "False";

    //         base.dbContext.Update(newTestRun);
    //         base.dbContext.SaveChanges();
    //         return Ok();
           
    //     }

    //     // DELETE: odata/UnitTestsbase
    //     [HttpDelete]
    //     public async Task<IActionResult> Delete([FromODataUri] int key)
    //     {
    //         try
    //         {
    //             logger.LogDebug($"Begin: UnitTestsController Delete()");
    //             var unitTest = await dbContext.UnitTests.FindAsync(key);

    //             if (unitTest == null)
    //             {
    //                 return NotFound();
    //             }

    //             base.dbContext.Remove(unitTest);
    //             base.dbContext.SaveChanges();

    //             logger.LogDebug($"End: UnitTestsController Delete()");
    //             return Ok();
    //         }
    //         catch (Exception ex)
    //         {
    //             logger.LogError(ex, "An error occured while performing DELETE");
    //             throw;
    //         }
    //     }

    //     //????
    //     public async Task<IActionResult> Patch([FromODataUri] int key, Delta<UnitTest> unitTest)
    //     {
    //         try
    //         {
    //             logger.LogDebug($"Begin: UnitTestsController Patch()");
    //             var unitTestToChange = await base.dbContext.UnitTests.FindAsync(key);

    //             if (unitTestToChange == null)
    //             {
    //                 return NotFound();
    //             }

    //             unitTest.Patch(unitTestToChange);
    //             base.dbContext.SaveChanges();

    //             logger.LogDebug($"End: UnitTestsController Patch()");
    //             return Updated(unitTestToChange);
    //         }
    //         catch (Exception ex)
    //         {
    //             logger.LogError(ex, "An error occured while performing PATCH");
    //             throw;
    //         }
    //     }


    //     //[ODataRoute("Execute")]
    //     // public bool bxecute([FromBody] UnitTest unitTestToExecute)
    //     // {
    //     //     if (!ModelState.IsValid)
    //     //     {
    //     //         var test = "model ungültig";
    //     //     }
    //     //     var testResult = false;
    //     //     dbContext.Add(unitTestToExecute);
    //     //     //await dbContext.SaveChangesAsync();
    //     //     //return Created(unitTestToExecute);


    //     //     Console.WriteLine($"Unittest {unitTestToExecute.Name} wird hier demnöchst ausgeführt.");

    //     //     return testResult;
    //     // }

    //     [AllowAnonymous]
    //     [HttpPost]
    //     public UnitTestExecutionResult Execute([FromBody] object unitTestObject, [FromServices] PowerBiService powerBiService)
    //     {
    //         logger.LogDebug($"Begin: UnitTestController Execute()");
    //         try
    //         {
    //             logger.LogDebug("Executing UnitTest.....");
    //             var unitTestExecutionResult = new UnitTestExecutionResult();

    //             var structur = JsonConvert.DeserializeObject<Structur>(unitTestObject.ToString());

    //             var unitTestUpdate = JsonConvert.DeserializeObject<UnitTest>(unitTestObject.ToString());

    //             Console.WriteLine($"Unit Test {structur.Name} wird ausgeführt");

    //             var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    //             string authToken = powerBiService.GetTokenOnBehalfOf(base.msIdTenantCurrentUser, accessToken).Result;
    //             Guid datasetId = new Guid(structur.DatasetPbId);//new Guid("1272907a-888e-446f-b89e-037cfaf3f8b5");
    //                                                             //DatasetId Variable                 
    //             TestRun HistoryAdd = new TestRun();

    //             string jsonResponse;
    //             if (powerBiService.QueryDataset(datasetId, structur.DAX, authToken, out jsonResponse))
    //             {


    //                 unitTestExecutionResult.UnitTestExecuted = true;

    //                 dynamic TestRersultArray = JsonConvert.DeserializeObject(jsonResponse);
    //                 var TestResult = ((TestRersultArray.results[0].tables[0].rows[0] as JObject).First as JProperty).Value.ToString();

    //                 logger.LogDebug($"TestResult: {TestResult}");


    //                 if (double.TryParse(TestResult, out double testResultDouble))
    //                 {
    //                     TestResult = Math.Round(testResultDouble, 4).ToString();
    //                     logger.LogDebug($"rounded TestResult: {TestResult}");
    //                 }

    //                 if (TestResult == structur.ExpectedResult)
    //                 {
    //                     unitTestExecutionResult.UnitTestSucceeded = true;
    //                 }

    //                 else
    //                 {
    //                     unitTestExecutionResult.UnitTestSucceeded = false;
    //                 }

    //                 logger.LogDebug($"unitTestExecutionResult: {unitTestExecutionResult}");

    //                 //unitTestUpdate.LastResult = TestResult;
    //                 UnitTest UnitTestNeu = base.dbContext.UnitTests.Where(p => p.Id == unitTestUpdate.Id).FirstOrDefault();
    //                 UnitTestNeu.LastResult = TestResult;

    //                 if(UnitTestNeu.ResultType == "Float")
    //                 {
    //                     float FloatResult;
    //                     FloatResult = float.Parse(TestResult);
    //                     //UnitTestNeu.LastResult = Math.Round(FloatResult, int.Parse(UnitTestNeu.DecimalPlaces)).ToString();


    //                     float roundedValue = (float)Math.Round(FloatResult, int.Parse(UnitTestNeu.DecimalPlaces));
    //                     float round = FloatResult - roundedValue;

    //                             if (FloatResult - roundedValue >= 0.05)
    //                                 {   
    //                                     roundedValue += (float)Math.Pow(0.1, int.Parse(UnitTestNeu.DecimalPlaces));
    //                                 }   

    //                             UnitTestNeu.LastResult = roundedValue.ToString();
    //                             UnitTestNeu.LastResult = Math.Round(roundedValue, int.Parse(UnitTestNeu.DecimalPlaces)).ToString();


    //                     if(UnitTestNeu.FloatSeparators == "Use Seperators")
    //                     {
    //                         //UnitTestNeu.LastResult.ToString("N");
                            
    //                         double number = float.Parse(UnitTestNeu.LastResult);
    //                         UnitTestNeu.LastResult = number.ToString("N");
    //                     }
    //                 }

    //                 if(UnitTestNeu.ResultType == "Date")
    //                 {
    //                     DateTime DateResult;
    //                     if(UnitTestNeu.DateTimeFormat == "UTC")
    //                     {
    //                         DateResult = DateTime.Parse(TestResult).ToUniversalTime();
    //                         UnitTestNeu.LastResult = DateResult.ToString();

                            

    //                     }

    //                     if(UnitTestNeu.DateTimeFormat == "CET")
    //                     {
    //                         DateResult = DateTime.Parse(TestResult).ToLocalTime();
    //                         UnitTestNeu.LastResult = DateResult.ToString();
    //                     }

    //                 }

    //                 if(UnitTestNeu.ResultType == "Percentage")
    //                 {
    //                     double number = float.Parse(TestResult);
    //                     UnitTestNeu.LastResult = number.ToString("#0.###%");

    //                 }

                    
    //                 UnitTestNeu.Timestamp = DateTime.Now.ToString();

    //                 HistoryAdd.Result = UnitTestNeu.LastResult;
    //                 HistoryAdd.ExpectedResult = UnitTestNeu.ExpectedResult;
    //                 HistoryAdd.UnitTest = UnitTestNeu.Id;
    //                 HistoryAdd.WasPassed = unitTestExecutionResult.UnitTestSucceeded.ToString();
    //                 HistoryAdd.TimeStamp = UnitTestNeu.Timestamp;
    //                 HistoriesForTestRun.Add(HistoryAdd);
                    

    //                 base.dbContext.Add(HistoryAdd);
    //                 base.dbContext.SaveChanges();

    //                 base.dbContext.Update(UnitTestNeu);
    //                 base.dbContext.SaveChanges();
    //             }

    //             //TODO
    //             /* else if (powerBiService.QueryDataset(datasetId, structur.DAX, authToken, out jsonResponse)){
    //                  unitTestExecutionResult.UnitTestExecuted = false;
    //                  Root2 TestValue;
    //                  TestValue = JsonConvert.DeserializeObject<Root2>(jsonResponse);

    //                  unitTestUpdate.LastResult = TestValue.results[0].tables[0].rows[0].ActualValue.ToString();
    //                  UnitTest UnitTestNeu = new UnitTest();
    //                  UnitTestNeu = base.dbContext.UnitTests.Where(p => p.Name == unitTestUpdate.Name).FirstOrDefault();
    //                  UnitTestNeu.LastResult = unitTestUpdate.LastResult;

    //                  DateTime Timestamp = new DateTime();
    //                  Timestamp = DateTime.Now;
    //                  UnitTestNeu.Timestamp = Timestamp.ToString();

    //                  HistoryAdd.LastRun = UnitTestNeu.LastResult;
    //                  HistoryAdd.ExpectedRun = UnitTestNeu.ExpectedResult;
    //                  HistoryAdd.UnitTest = UnitTestNeu.Id;
    //                  HistoryAdd.TimeStamp = UnitTestNeu.Timestamp;
    //                  HistoryAdd.Result = "False";

    //                  base.dbContext.Add(HistoryAdd);
    //                  base.dbContext.SaveChanges();


    //                  base.dbContext.Update(UnitTestNeu);
    //                  base.dbContext.SaveChanges();
    //              }*/



    //             logger.LogDebug($"End: UnitTestController Execute(Return: {unitTestExecutionResult})");
    //             return unitTestExecutionResult;
    //         }
    //         catch (Exception ex)
    //         {
    //             logger.LogDebug(ex, "An error occured while Executing the UnitTest(Controller)");
    //             throw;
    //         }
    //     }

    //     [AllowAnonymous]
    //     [HttpPost]
    //     public async Task<IActionResult> LoadWorkspace([FromServices] PowerBiService powerBiService, Root TestValue, Root3 TestDataset)
    //     {
    //         try
    //         {
    //             logger.LogDebug($"Begin: UnitTestsController LoadWorkspaces()");
    //             logger.LogDebug("Sycing or Loading Workspaces");
    //             LW(powerBiService, out TestValue);

    //             int Zähler;
    //             Zähler = TestValue.OdataCount;
    //             for (int i = 0; i <= Zähler - 1; i++)
    //             {
    //                 Workspace AddWorkspace = new Workspace();
    //                 string Json2;


    //                 //using (var db = new PortalDbContext())


    //                 var Test = TestValue.Values.ElementAt(i);
    //                 var TestWorkspace = base.dbContext.Workspaces.Where(p => p.MsId == Test.id).FirstOrDefault();


    //                 if (TestWorkspace == null)
    //                 {
    //                     AddWorkspace.Name = TestValue.Values.ElementAt(i).name;
    //                     AddWorkspace.MsId = TestValue.Values.ElementAt(i).id;

    //                     base.dbContext.Add(AddWorkspace);
    //                     base.dbContext.SaveChanges();

    //                 }

    //                 else if (TestWorkspace != null)
    //                 {
    //                     if (Test.name != TestWorkspace.Name)
    //                     {
    //                         TestWorkspace.Name = Test.name;
    //                     }

    //                     base.dbContext.Update(TestWorkspace);
    //                 }

    //                 LoadDataset(TestValue.Values.ElementAt(i).id, powerBiService, out TestDataset);

    //                 int Zähler2;
    //                 Zähler2 = TestDataset.Values.Count - 1;

    //                 if (Zähler2 >= 0)
    //                 {
    //                     for (int k = 0; k <= Zähler2; k++)
    //                     {
    //                         TabularModel AddTabModel = new TabularModel();

    //                         var DatasetTestId = TestDataset.Values.ElementAt(k).id;
    //                         var TestTabModel = base.dbContext.TabularModels.Where(p => p.DatasetPbId == DatasetTestId).FirstOrDefault();

    //                         if (TestTabModel == null)
    //                         {
    //                             AddWorkspace.MsId = TestValue.Values.ElementAt(i).id;
    //                             AddTabModel.DatasetPbId = TestDataset.Values.ElementAt(k).id;
    //                             AddTabModel.Workspace = base.dbContext.Workspaces.Where(p => p.MsId == AddWorkspace.MsId).FirstOrDefault().Id;
    //                             AddTabModel.Name = TestDataset.Values.ElementAt(k).name;

    //                             base.dbContext.Add(AddTabModel);
    //                             var test = base.dbContext.Workspaces.Where(e => e.Name == null);
    //                             base.dbContext.SaveChanges();
    //                         }

    //                         else if (TestTabModel != null)
    //                         {
    //                             AddWorkspace.MsId = TestValue.Values.ElementAt(i).id;
    //                             if (TestDataset.Values.ElementAt(k).id != base.dbContext.TabularModels.Where(p => p.DatasetPbId == TestDataset.Values.ElementAt(k).id).FirstOrDefault().DatasetPbId)
    //                             {
    //                                 TestTabModel.DatasetPbId = TestDataset.Values.ElementAt(k).id;
    //                             }

    //                            //if(TestDataset.Values.ElementAt(k).name != base.dbContext.TabularModels.Where(p => p.Name == TestDataset.Values.ElementAt(k).name).FirstOrDefault().Name)
    //                             //TestTabModel.Name = TestDataset.Values.ElementAt(k).name;

    //                             var tabularModel = base.dbContext.TabularModels.FirstOrDefault(p => p.Name == TestDataset.Values.ElementAt(k).name);
    //                                 if (tabularModel != null && TestDataset.Values.ElementAt(k).name != null)
    //                                     {
    //                                         if (TestDataset.Values.ElementAt(k).name != tabularModel.Name)
    //                                                 {
    //                                                     TestTabModel.Name = TestDataset.Values.ElementAt(k).name;
    //                                                 }
    //                                     }
    //                                     else
    //                                         {

    //                                         }


    //                             base.dbContext.Update(TestTabModel);
    //                         }
    //                     }
    //                 }
    //             }
    //             base.dbContext.SaveChanges();

    //             logger.LogDebug($"End: UnitTestController LoadDataset()");

    //             return Ok();
    //         }
    //         catch (Exception ex)
    //         {
    //             logger.LogError(ex, "An error occured while Sycning or Loading Workspaces");
    //             throw;
    //         }
    //     }

    //     public Root3 LoadDataset(string DatasetPbId, [FromServices] PowerBiService powerBiService, out Root3 TestDataset)
    //     {
    //         logger.LogDebug($"Begin: UnitTestsController LoadDataset()");
    //         var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    //         string authToken = powerBiService.GetTokenOnBehalfOf(base.msIdTenantCurrentUser, accessToken).Result;
    //         string jsonResponse;

    //         if (powerBiService.LoadDataset(DatasetPbId, authToken, out jsonResponse))
    //         {
    //             TestDataset = JsonConvert.DeserializeObject<Root3>(jsonResponse);
    //         }

    //         else
    //         {
    //             TestDataset = new Root3();
    //         }

    //         logger.LogDebug($"End: UnitTestsController LoadDataset()");
    //         return TestDataset;
    //     }

    //     public Root LW([FromServices] PowerBiService powerBiService, out Root TestValue)
    //     {
    //         logger.LogDebug($"Start: UnitTestsController LW()");

    //         var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    //         string authToken = powerBiService.GetTokenOnBehalfOf(base.msIdTenantCurrentUser, accessToken).Result;
    //         string jsonResponse;
    //         //Root TestValue;

    //         if (powerBiService.LoadWorkspace(authToken, out jsonResponse))
    //         {

    //             TestValue = JsonConvert.DeserializeObject<Root>(jsonResponse);
    //         }

    //         else
    //         {
    //             TestValue = new Root();
    //         }

    //         logger.LogDebug($"End: UnitTestsController LW()");
    //         return TestValue;
    //     }
    }
}
