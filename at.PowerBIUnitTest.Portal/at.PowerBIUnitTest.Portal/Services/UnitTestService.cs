using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using at.PowerBIUnitTest.Portal.Data.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace at.PowerBIUnitTest.Portal.Services
{
    public class UnitTestService
    {
        private readonly ILogger logger;
        private readonly PortalDbContext dbContext;
        private readonly PowerBiService powerBiService;

        public UnitTestService(ILogger<UnitTestService> logger, PortalDbContext dbContext, PowerBiService powerBiService)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.powerBiService = powerBiService;
        }

        public async Task ExecuteMultipe(Guid msIdTenantCurrentUser, string accessToken, IEnumerable<int> unitTestIds)
        {
            var powerBiToken = await powerBiService.GetTokenOnBehalfOf(msIdTenantCurrentUser, accessToken);

            var testRuns = new List<TestRun>();
            foreach (var unitTestId in unitTestIds)
            {
                var testRun = await Execute(powerBiToken, unitTestId);
                if (testRun != null)
                {
                    testRuns.Add(testRun);
                }
            }

            var testRunCollection = new TestRunCollection
            {
                TimeStamp = DateTime.Now,
                TestRuns = testRuns,
                WasPassed = testRuns.All(e => e.WasPassed)
            };

            dbContext.TestRunCollections.Add(testRunCollection);
            await dbContext.SaveChangesAsync();
        }

        private async Task<TestRun> Execute(string powerBiToken, int unitTestIds)
        {
            var unitTest = dbContext.UnitTests.FirstOrDefault(e => e.Id == unitTestIds);
            if (unitTest == null)
            {
                logger.LogError($"Unit test with id {unitTestIds} not found");
                return null;
            }

            var result = await this.powerBiService.QueryDataset(powerBiToken, unitTest.UserStoryNavigation.TabularModelNavigation.MsId, unitTest.DAX);
            string testResult = null;
            try
            {
                testResult = ((result.jObject["results"][0]["tables"][0]["rows"][0] as JObject).First as JProperty).Value.ToString();

                if (unitTest.ResultType == "Number")
                {
                    float floatResult = float.Parse(testResult);

                    if (unitTest.DecimalPlaces != null)
                        testResult = Math.Round(floatResult, int.Parse(unitTest.DecimalPlaces)).ToString();

                    if (unitTest.FloatSeparators == "Use Seperators")
                    {
                        double number = float.Parse(testResult);
                        testResult = number.ToString("N");
                    }
                }
                else if (unitTest.ResultType == "Date")
                {
                    if (unitTest.DateTimeFormat == "UTC")
                    {
                        testResult = DateTime.Parse(testResult).ToUniversalTime().ToString();
                    }

                    if (unitTest.DateTimeFormat == "CET")
                    {
                        testResult = DateTime.Parse(testResult).ToLocalTime().ToString();
                        testResult = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(testResult).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time")).ToString();
                    }
                }
                else if (unitTest.ResultType == "Percentage")
                {
                    double number = float.Parse(testResult);
                    testResult = number.ToString("#0.###%");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while parsing result for unit test {unitTest.Id}");
            }

            var testRun = new TestRun
            {
                UnitTest = unitTest.Id,
                JsonResponse = result.jsonResponse,
                Result = testResult ?? null,
                TimeStamp = DateTime.Now,
                ExecutedSuccessfully = testResult != null,
                WasPassed = testResult == unitTest.ExpectedResult,
                ExpectedResult = unitTest.ExpectedResult
            };

            return testRun;
        }
    }
}