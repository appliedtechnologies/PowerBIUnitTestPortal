using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using at.PowerBIUnitTest.Portal.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;


namespace at.PowerBIUnitTest.Portal.Services
{
    public class PowerBiService 
    {
        private readonly string clientId;
        private readonly string clientSecret;
        private readonly ILogger<PowerBiService> logger;
        
        public PowerBiService(IConfiguration config, ILogger<PowerBiService> logger){
            this.clientId = config["AzureAd:ClientId"];
            this.clientSecret = config["AzureAd:ClientSecret"];
            this.logger = logger;

        }
        public bool QueryDataset(Guid datasetId, string daxQuery, string authToken, out string jsonResponse)
        {
            logger.LogDebug($"Begin: PowerBIService QueryDataset(datasetId: {datasetId}, daxQuery: {daxQuery})");
            try
            {
                logger.LogDebug("Querying dataset...");
                HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", authToken);

            string url = $"https://api.powerbi.com/v1.0/myorg/datasets/{datasetId}/executeQueries";

            var requestBody = JsonSerializer.Serialize(
                new QueryRequest(daxQuery)
                );


            var response = client.PostAsync(url, new StringContent(requestBody, UnicodeEncoding.UTF8, "application/json")).Result;
            jsonResponse = response.Content.ReadAsStringAsync().Result;
            
            var IsSuccessStatusCode = response.IsSuccessStatusCode;
            logger.LogDebug($"End: PowerBIService QueryDataset(jsonResponse: {jsonResponse}, Return: {IsSuccessStatusCode})");
            return IsSuccessStatusCode;
            
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occurred while querying the dataset.");
                throw;
            }
        }

        public bool ShowValue(Guid datasetId, string daxQuery, string authToken, out string jsonResponse)
        {
            logger.LogDebug($"Begin: PowerBIService ShowValue(datasetId: {datasetId})");
            try
            {
                logger.LogDebug("Showing Value.....");
                HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", authToken);

            string url = $"https://api.powerbi.com/v1.0/myorg/datasets/{datasetId}/executeQueries?Monat=Januar";

            var requestBody = JsonSerializer.Serialize(
                new QueryRequest(daxQuery)
                );


            var response = client.PostAsync(url, new StringContent(requestBody, UnicodeEncoding.UTF8, "application/json")).Result;
            jsonResponse = response.Content.ReadAsStringAsync().Result;

            var IsSuccessStatusCode = response.IsSuccessStatusCode;
            logger.LogDebug($"End: PowerBIService ShowValue(jsonResponse: {jsonResponse}, Return: {IsSuccessStatusCode})");
            return IsSuccessStatusCode;
            
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occurred while querying the dataset");
                throw;
            }
        }

        public bool LoadWorkspace(string authToken, out string jsonResponse)
        {
            logger.LogDebug($"Begin: PowerBIService LoadWorkspace");
            try
            {
                logger.LogDebug("Loading Workspace....");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", authToken);

                string url = $"https://api.powerbi.com/v1.0/myorg/groups";

                


                var response =client.GetAsync(url).Result;
            jsonResponse = response.Content.ReadAsStringAsync().Result;
            var IsSuccessStatusCode = response.IsSuccessStatusCode;
            logger.LogDebug($"End: PowerBIService LoadWorkspace(jsonResponse: {jsonResponse}, Return: {IsSuccessStatusCode})");
            return IsSuccessStatusCode;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occurred while loading workspaces");
                throw;
            }

        }

         public bool LoadDataset(string datasetId, string authToken, out string jsonResponse)
        {
            logger.LogDebug($"Begin: PowerBIService LoadDataset(datasetId: {datasetId})");
            try
            {
                logger.LogDebug("Loading Dataset....");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authToken);

                string url = $"https://api.powerbi.com/v1.0/myorg/groups/{datasetId}/datasets";

                


                var response =client.GetAsync(url).Result;
            jsonResponse = response.Content.ReadAsStringAsync().Result;
            var IsSuccessStatusCode = response.IsSuccessStatusCode;
            logger.LogDebug($"End: PowerBIService LoadDataset(jsonResponse: {jsonResponse}, Return: {IsSuccessStatusCode})");
            return IsSuccessStatusCode;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occurred while loading Dataset");
                throw;
            }

        }
    

        public async Task<string> GetTokenOnBehalfOf(Guid msIdTenant, string accessToken)
        {
            logger.LogDebug($"Begin: PowerBIService GetTokenOnBehalfOf()");
             try
             {
                logger.LogDebug("Getting token");
            IConfidentialClientApplication PublicClientApp = ConfidentialClientApplicationBuilder.Create(clientId)
                                                            .WithClientSecret(clientSecret)
                                                            .WithAuthority(AzureCloudInstance.AzurePublic, msIdTenant)
                                                            .Build();
            AuthenticationResult authResult = await PublicClientApp.AcquireTokenOnBehalfOf(scopes: new[] { "https://analysis.windows.net/powerbi/api/.default " }, new UserAssertion(accessToken))
                                                                    .ExecuteAsync();

            var Token = authResult.AccessToken;
            logger.LogDebug($"End: PowerBIService GetTokenOnBehalf( Return: {Token})");
            return Token;
             }
             catch(Exception ex)
             {
                logger.LogError(ex, "An error occured while getting Token");
                throw;

             }
        }   

        //public async Task<string> GetOAuthToken(string OAuthToken){
          //  IConfidentialClientApplication PublicClientApp = ConfidentialClientApplicationBuilder.Create(clientId)
            //                                                   .WithClientSecret(clientSecret)
              //                                              .WithAuthority(AzureCloudInstance.AzurePublic, "fbfd2005-2cb0-4cea-9c07-d5ad0307112d")
                //                                            .Build();
                                                            
        //}

       
    }


}