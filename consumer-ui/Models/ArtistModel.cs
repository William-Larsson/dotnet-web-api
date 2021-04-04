using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace consumer_ui.Models
{
    // Model-class for the Artist controller.
    // Is used to make API calls related to the API-operations
    // for the endpoint in [API-URI]/api/Artist. 
    public class ArtistModel
    {
        private AuthHandler authHandler;
        private HttpClient httpClient;

        public ArtistModel()
        {
            authHandler = new AuthHandler();
            httpClient = authHandler.httpClient;
        }

        // For getting a single artist by its Id. 
        // Uses API GET. 
        public void GetById()
        {
            throw new NotImplementedException();
        }

        // For getting a all artists from API. 
        // Uses API GET
        // Returns list of ArtistDetails, parsed from json response. 
        public async Task<List<ArtistDetail>> Get()
        {
            var accessResult = authHandler.AcquireAccessToken().Result;
            List<ArtistDetail> artistList = null;

            if (!string.IsNullOrEmpty(accessResult.AccessToken))
            {
                // Call the API from its base address (set in the http client) + API-endpoint
                HttpResponseMessage response = await httpClient.GetAsync("/api/Artist");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    artistList = JsonConvert
                        .DeserializeObject<IEnumerable<ArtistDetail>>(json).ToList();;
                } 
                else // Prints failed respons for debugging
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Failed to call the Web Api: {response.StatusCode}");
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Content: {content}");
                    Console.ResetColor(); 
                }
            }

            return artistList; 
        }


        // Makes a HTTP Post to insert a new artist into the API. 
        // Only uses the artistDetail name for request. 
        public async Task Insert(ArtistDetail artistDetail)
        {
            var accessResult = authHandler.AcquireAccessToken().Result;

            if (!string.IsNullOrEmpty(accessResult.AccessToken))
            {
                // Call the API from its base address (set in the http client) + API-endpoint
                // with the json content. 
                var jsonContent = new StringContent(JsonConvert.SerializeObject(
                    new {name = artistDetail.Name}),
                    Encoding.UTF8, 
                    "application/json"
                );

                HttpResponseMessage response = await httpClient
                    .PostAsync("/api/Artist", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                } 
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Failed to call the Web Api: {response.StatusCode}");
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Content: {content}");
                }
                Console.ResetColor();
            }

            // TODO: Return the result (json or as an object) in some way?
        }

        // Makes a HTTP Put to update an existsing artist into the API. 
        // Only uses the artistDetail name for request, but uses Id 
        // to point to the correct API resource. 
        public async Task Update(ArtistDetail artistDetail)
        {
            var accessResult = authHandler.AcquireAccessToken().Result;

            if (!string.IsNullOrEmpty(accessResult.AccessToken))
            {
                // Call the API from its base address (set in the http client) + API-endpoint
                // with the json content. 
                var jsonContent = new StringContent(JsonConvert.SerializeObject(
                    new {name = artistDetail.Name}),
                    Encoding.UTF8, 
                    "application/json"
                );

                HttpResponseMessage response = await httpClient
                    .PutAsync($"/api/Artist/{artistDetail.Id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                } 
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Failed to call the Web Api: {response.StatusCode}");
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Content: {content}");
                }
                Console.ResetColor();
            }

             // TODO: Return the result in some way?
        }

        // Makes a HTTP Delete to remove an existsing artist from the API. 
        // Only uses the Id to point to the correct API resource. 
        public async Task Delete(int id)
        {
            var accessResult = authHandler.AcquireAccessToken().Result;

            if (!string.IsNullOrEmpty(accessResult.AccessToken))
            {
                // Call the API from its base address (set in the http client) + API-endpoint
                HttpResponseMessage response = await httpClient
                    .DeleteAsync($"/api/Artist/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                } 
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Failed to call the Web Api: {response.StatusCode}");
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Content: {content}");
                }
                Console.ResetColor();
            }

             // TODO: Return the result in some way?
        }

    }
}