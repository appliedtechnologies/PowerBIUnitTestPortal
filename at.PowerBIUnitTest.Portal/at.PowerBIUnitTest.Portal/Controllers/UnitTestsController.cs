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
            await unitTestService.ExecuteMultipe(msIdTenantCurrentUser, accessToken,unitTestIds);

            logger.LogDebug($"End: UnitTestsController Execute()");
            return Ok();
        }

        // [HttpPost]
        // public UnitTestExecutionResult Execute([FromBody] object unitTestObject, [FromServices] PowerBiService powerBiService)
        // {
        // logger.LogDebug($"Begin: UnitTestController Execute()");
        // try
        // {
        //     logger.LogDebug("Executing UnitTest.....");
        //     var unitTestExecutionResult = new UnitTestExecutionResult();

        //     var structur = JsonConvert.DeserializeObject<Structur>(unitTestObject.ToString());

        //     var unitTestUpdate = JsonConvert.DeserializeObject<UnitTest>(unitTestObject.ToString());

        //     Console.WriteLine($"Unit Test {structur.Name} wird ausgeführt");

        //     var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        //     string authToken = powerBiService.GetTokenOnBehalfOf(base.msIdTenantCurrentUser, accessToken).Result;
        //     Guid datasetId = new Guid(structur.DatasetPbId);//new Guid("1272907a-888e-446f-b89e-037cfaf3f8b5");
        //                                                     //DatasetId Variable                 
        //     TestRun HistoryAdd = new TestRun();

        //     string jsonResponse;
        //     if (powerBiService.QueryDataset(datasetId, structur.DAX, authToken, out jsonResponse))
        //     {


        //         unitTestExecutionResult.UnitTestExecuted = true;

        //         dynamic TestRersultArray = JsonConvert.DeserializeObject(jsonResponse);
        //         var TestResult = ((TestRersultArray.results[0].tables[0].rows[0] as JObject).First as JProperty).Value.ToString();

        //         logger.LogDebug($"TestResult: {TestResult}");


        //         if (double.TryParse(TestResult, out double testResultDouble))
        //         {
        //             TestResult = Math.Round(testResultDouble, 4).ToString();
        //             logger.LogDebug($"rounded TestResult: {TestResult}");
        //         }

        //         if (TestResult == structur.ExpectedResult)
        //         {
        //             unitTestExecutionResult.UnitTestSucceeded = true;
        //         }

        //         else
        //         {
        //             unitTestExecutionResult.UnitTestSucceeded = false;
        //         }

        //         logger.LogDebug($"unitTestExecutionResult: {unitTestExecutionResult}");

        //         //unitTestUpdate.LastResult = TestResult;
        //         UnitTest UnitTestNeu = base.dbContext.UnitTests.Where(p => p.Id == unitTestUpdate.Id).FirstOrDefault();
        //         UnitTestNeu.LastResult = TestResult;

        //         if(UnitTestNeu.ResultType == "Float")
        //         {
        //             float FloatResult;
        //             FloatResult = float.Parse(TestResult);
        //             //UnitTestNeu.LastResult = Math.Round(FloatResult, int.Parse(UnitTestNeu.DecimalPlaces)).ToString();


        //             float roundedValue = (float)Math.Round(FloatResult, int.Parse(UnitTestNeu.DecimalPlaces));
        //             float round = FloatResult - roundedValue;

        //                     if (FloatResult - roundedValue >= 0.05)
        //                         {   
        //                             roundedValue += (float)Math.Pow(0.1, int.Parse(UnitTestNeu.DecimalPlaces));
        //                         }   

        //                     UnitTestNeu.LastResult = roundedValue.ToString();
        //                     UnitTestNeu.LastResult = Math.Round(roundedValue, int.Parse(UnitTestNeu.DecimalPlaces)).ToString();


        //             if(UnitTestNeu.FloatSeparators == "Use Seperators")
        //             {
        //                 //UnitTestNeu.LastResult.ToString("N");

        //                 double number = float.Parse(UnitTestNeu.LastResult);
        //                 UnitTestNeu.LastResult = number.ToString("N");
        //             }
        //         }

        //         if(UnitTestNeu.ResultType == "Date")
        //         {
        //             DateTime DateResult;
        //             if(UnitTestNeu.DateTimeFormat == "UTC")
        //             {
        //                 DateResult = DateTime.Parse(TestResult).ToUniversalTime();
        //                 UnitTestNeu.LastResult = DateResult.ToString();



        //             }

        //             if(UnitTestNeu.DateTimeFormat == "CET")
        //             {
        //                 DateResult = DateTime.Parse(TestResult).ToLocalTime();
        //                 UnitTestNeu.LastResult = DateResult.ToString();
        //             }

        //         }

        //         if(UnitTestNeu.ResultType == "Percentage")
        //         {
        //             double number = float.Parse(TestResult);
        //             UnitTestNeu.LastResult = number.ToString("#0.###%");

        //         }


        //         UnitTestNeu.Timestamp = DateTime.Now.ToString();

        //         HistoryAdd.Result = UnitTestNeu.LastResult;
        //         HistoryAdd.ExpectedResult = UnitTestNeu.ExpectedResult;
        //         HistoryAdd.UnitTest = UnitTestNeu.Id;
        //         HistoryAdd.WasPassed = unitTestExecutionResult.UnitTestSucceeded.ToString();
        //         HistoryAdd.TimeStamp = UnitTestNeu.Timestamp;
        //         HistoriesForTestRun.Add(HistoryAdd);


        //         base.dbContext.Add(HistoryAdd);
        //         base.dbContext.SaveChanges();

        //         base.dbContext.Update(UnitTestNeu);
        //         base.dbContext.SaveChanges();
        //     }

        //     //TODO
        //     /* else if (powerBiService.QueryDataset(datasetId, structur.DAX, authToken, out jsonResponse)){
        //          unitTestExecutionResult.UnitTestExecuted = false;
        //          Root2 TestValue;
        //          TestValue = JsonConvert.DeserializeObject<Root2>(jsonResponse);

        //          unitTestUpdate.LastResult = TestValue.results[0].tables[0].rows[0].ActualValue.ToString();
        //          UnitTest UnitTestNeu = new UnitTest();
        //          UnitTestNeu = base.dbContext.UnitTests.Where(p => p.Name == unitTestUpdate.Name).FirstOrDefault();
        //          UnitTestNeu.LastResult = unitTestUpdate.LastResult;

        //          DateTime Timestamp = new DateTime();
        //          Timestamp = DateTime.Now;
        //          UnitTestNeu.Timestamp = Timestamp.ToString();

        //          HistoryAdd.LastRun = UnitTestNeu.LastResult;
        //          HistoryAdd.ExpectedRun = UnitTestNeu.ExpectedResult;
        //          HistoryAdd.UnitTest = UnitTestNeu.Id;
        //          HistoryAdd.TimeStamp = UnitTestNeu.Timestamp;
        //          HistoryAdd.Result = "False";

        //          base.dbContext.Add(HistoryAdd);
        //          base.dbContext.SaveChanges();


        //          base.dbContext.Update(UnitTestNeu);
        //          base.dbContext.SaveChanges();
        //      }*/



        //     logger.LogDebug($"End: UnitTestController Execute(Return: {unitTestExecutionResult})");
        //     return unitTestExecutionResult;
        // }
        // catch (Exception ex)
        // {
        //     logger.LogDebug(ex, "An error occured while Executing the UnitTest(Controller)");
        //     throw;
        // }
        // }
    }
}
