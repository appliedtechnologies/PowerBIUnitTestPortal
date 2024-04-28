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
                if(testRun != null)
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
            if(unitTest == null)
            {
                logger.LogError($"Unit test with id {unitTestIds} not found");
                return null;
            }

            var testResult = await this.powerBiService.QueryDataset(powerBiToken, unitTest.UserStoryNavigation.TabularModelNavigation.MsId, unitTest.DAX);

            var testRun = new TestRun
            {
                UnitTest = unitTest.Id,
                Result = testResult,
                TimeStamp = DateTime.Now,
                WasPassed = testResult == unitTest.ExpectedResult,
                ExpectedResult = unitTest.ExpectedResult
            };

            return testRun;
        }
    }
}