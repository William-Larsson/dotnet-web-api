using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace consumer_ui.Models
{
    public class ArtistModel
    {
        // TODO: in this class, make operations for HTTP Get, put, post, delete
        // Example: see assignment 3 from previous course and the relation between the classes of the same name 
        // connect (maybe) to ArtistDetail, ArtistController and AuthHandler (?)

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
        // Uses API GET. 
        // TODO: returntype?
        public async void Get()
        {
            var accessResult = authHandler.AcquireAccessToken().Result;

            if (!string.IsNullOrEmpty(accessResult.AccessToken))
            {
                // Call the API from its base address (set in the http client) + API-endpoint
                HttpResponseMessage response = await httpClient.GetAsync("/api/Artist");

                if (response.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string json = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("HTTP response ArtistModel Get():");
                    Console.WriteLine(json + "\n");
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

            // TODO: Return the result in some way!!
        }


        // Makes a HTTP Post to insert a new artist into the API. 
        // Only uses the artistDetail name for request. 
        public async void Insert(ArtistDetail artistDetail)
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
                    Console.ForegroundColor = ConsoleColor.Green;
                    string json = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("HTTP response ArtistModel Insert():");
                    Console.WriteLine(json + "\n");
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

        // Makes a HTTP Put to update an existsing artist into the API. 
        // Only uses the artistDetail name for request, but uses Id 
        // to point to the correct API resource. 
        public async void Update(ArtistDetail artistDetail)
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
                    Console.ForegroundColor = ConsoleColor.Green;
                    string json = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("HTTP response ArtistModel Update():");
                    Console.WriteLine(json + "\n");
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
        public async void Delete(ArtistDetail artistDetail)
        {
            var accessResult = authHandler.AcquireAccessToken().Result;

            if (!string.IsNullOrEmpty(accessResult.AccessToken))
            {
                // Call the API from its base address (set in the http client) + API-endpoint
                HttpResponseMessage response = await httpClient
                    .DeleteAsync($"/api/Artist/{artistDetail.Id}");

                if (response.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string json = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("HTTP response ArtistModel Delete():");
                    Console.WriteLine(json + "\n");
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