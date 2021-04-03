using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace consumer_ui
{
    public class AuthHandler
    {
        public HttpClient httpClient { get; }
        public AuthConfig authConfig { get; }

        private IConfidentialClientApplication clientApp;
        private string[] resourceIds; 

        // Constructor.
        // Will setup settings from authsettings.json, 
        // create a HttpClient with SSL-certificate and
        // configure client app for making a access token request. 
        public AuthHandler(){
            authConfig = AuthConfig.ReadFromJsonFile("authsettings.json");

            // Bypass SSL certificate to get a proper SSL connection.
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            httpClient = new HttpClient(clientHandler);
            httpClient.BaseAddress = new Uri(authConfig.BaseAddress);

            clientApp = ConfidentialClientApplicationBuilder.Create(authConfig.ClientId)
                .WithClientSecret(authConfig.ClientSecret)
                .WithAuthority(new Uri(authConfig.Authority))
                .Build();

            resourceIds = new string[] { authConfig.ResourceId };

        }

        // Makes a request to gain a token for accessing the web api. 
        public async Task<AuthenticationResult> AcquireAccessToken()
        {
            AuthenticationResult result = null;
            try
            {
                result = await clientApp.AcquireTokenForClient(resourceIds).ExecuteAsync();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("API access token acquired.");
                Console.ResetColor();

                // Setup the headers:
                HttpRequestHeaders defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                if (defaultRequestHeaders.Accept == null ||
                   !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                
                // The most important step!!
                // Sets authorization to "bearer" and pass the access token
                // to the request header
                defaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("bearer", result.AccessToken);

                return result;
            }
            catch (MsalClientException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                return result;
            }
        } 
    }
}