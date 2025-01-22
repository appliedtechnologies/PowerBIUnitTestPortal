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
using Microsoft.Identity.Web;
using Newtonsoft.Json.Linq;

namespace at.PowerBIUnitTest.Portal.Services
{
    public class PowerBiService
    {
        private readonly ILogger logger;
        private readonly string clientId;
        private readonly string clientSecret;

        public PowerBiService(IConfiguration config, ILogger<PowerBiService> logger)
        {
            this.logger = logger;
            this.clientId = config["AzureAd:ClientId"];
            this.clientSecret = config["AzureAd:ClientSecret"];
        }

        public async Task<string> GetTokenOnBehalfOf(Guid msIdTenant, string accessToken)
        {
            logger.LogDebug($"Begin: PowerBiService GetTokenOnBehalfOf()");
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
                logger.LogDebug($"End: PowerBiService GetTokenOnBehalf( Return: {Token})");
                return Token;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured while getting Token");
                throw;

            }
        }

        public async Task<string> GetEmbedToken(IDownstreamWebApi client, Guid tenantId, Guid workspaceId, Guid reportId)
        {
             
            string url = $"/groups/{workspaceId}/reports/{reportId}/GenerateToken";

             
            var payload = new
            {
                accessLevel = "View"
            };

            string jsonPayload = JsonSerializer.Serialize(payload);
            
            using var requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
             var response = await client.CallWebApiForAppAsync(
                "PowerBiApi",
                options =>
                {
                    options.Tenant = tenantId.ToString();
                    options.RelativePath = url;
                    options.HttpMethod = HttpMethod.Post;
                    
                },
                content : requestContent
                );

            
            response.EnsureSuccessStatusCode();

            
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseJson = JsonSerializer.Deserialize<JsonDocument>(responseBody);

            
            return responseJson?.RootElement.GetProperty("token").GetString();
            
        }

        public async Task<JToken> LoadWorkspace(string authToken)
        {
            logger.LogDebug($"Begin: PowerBiService LoadWorkspace");
            try
            {
                logger.LogDebug("Loading Workspace");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                string url = $"https://api.powerbi.com/v1.0/myorg/groups";

                var response = await client.GetAsync(url);
                var jsonResponse = (await response.Content.ReadAsAsync<JObject>())["value"];
                logger.LogDebug($"End: PowerBiService LoadWorkspace(return: {jsonResponse})");
                return jsonResponse;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while loading workspaces");
                throw;
            }
        }

        public async Task<JToken> LoadDataset(string authToken, Guid datasetId)
        {
            logger.LogDebug($"Begin: PowerBIService LoadDataset(datasetId: {datasetId})");
            try
            {
                logger.LogDebug("Loading Dataset");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                string url = $"https://api.powerbi.com/v1.0/myorg/groups/{datasetId}/datasets";

                var response = await client.GetAsync(url);
                var jsonResponse = (await response.Content.ReadAsAsync<JObject>())["value"];
                logger.LogDebug($"End: PowerBIService LoadDataset(return: {jsonResponse})");
                return jsonResponse;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while loading Dataset");
                throw;
            }
        }

        public async Task<(string jsonResponse, JObject jObject)> QueryDataset(string authToken, Guid datasetId, string daxQuery)
        {
            logger.LogDebug($"Begin: PowerBIService QueryDataset(datasetId: {datasetId}, daxQuery: {daxQuery})");
            try
            {
                logger.LogDebug("Querying dataset");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                string url = $"https://api.powerbi.com/v1.0/myorg/datasets/{datasetId}/executeQueries";

                var requestBody = JsonSerializer.Serialize(
                    new QueryRequest(daxQuery)
                    );


                var response = await client.PostAsync(url, new StringContent(requestBody, UnicodeEncoding.UTF8, "application/json"));
                var jObject = await response.Content.ReadAsAsync<JObject>();
                var jsonResponse = await response.Content.ReadAsStringAsync();

                logger.LogDebug($"End: PowerBIService QueryDataset(jsonResponse: {jsonResponse})");

                return (jsonResponse, jObject);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while querying the dataset."); 
                throw;
            }
        }
    }
}