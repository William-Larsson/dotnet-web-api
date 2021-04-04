using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace consumer_ui.Models
{
    public class PublisherModel
    {
        // TODO: in this class, make operations for HTTP Get, put, post, delete
        // Example: see assignment 3 from previous course (for example Publishermodel/detail/controller/view)
        // connect (maybe) to PublisherDetail, PublisherModel and AuthHandler (?)
        private AuthHandler authHandler;
        private HttpClient httpClient;

        public PublisherModel()
        {
            authHandler = new AuthHandler();
            httpClient = authHandler.httpClient;
        }

        // For getting a single Publisher by its Id. 
        // Uses API GET. 
        public void GetById()
        {
            throw new NotImplementedException();
        }

        // For getting a all publishers from API. 
        // Uses API GET. 
        public async void Get()
        {
            var accessResult = authHandler.AcquireAccessToken().Result;

            if (!string.IsNullOrEmpty(accessResult.AccessToken))
            {
                // Call the API from its base address (set in the http client) + API-endpoint
                HttpResponseMessage response = await httpClient.GetAsync("/api/Publisher");

                if (response.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string json = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("HTTP response PublisherModel Get():");
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


        // Makes a HTTP Post to insert a new Publisher into the API. 
        // Only uses the PublisherDetail name for request. 
        public async void Insert(PublisherDetail PublisherDetail)
        {
            var accessResult = authHandler.AcquireAccessToken().Result;

            if (!string.IsNullOrEmpty(accessResult.AccessToken))
            {
                // Call the API from its base address (set in the http client) + API-endpoint
                // with the json content. 
                var jsonContent = new StringContent(JsonConvert.SerializeObject(
                    new {name = PublisherDetail.Name}),
                    Encoding.UTF8, 
                    "application/json"
                );

                HttpResponseMessage response = await httpClient
                    .PostAsync("/api/Publisher", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string json = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("HTTP response PublisherModel Insert():");
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

        // Makes a HTTP Put to update an existsing Publisher into the API. 
        // Only uses the PublisherDetail name for request, but uses Id 
        // to point to the correct API resource. 
        public async void Update(PublisherDetail PublisherDetail)
        {
            var accessResult = authHandler.AcquireAccessToken().Result;

            if (!string.IsNullOrEmpty(accessResult.AccessToken))
            {
                // Call the API from its base address (set in the http client) + API-endpoint
                // with the json content. 
                var jsonContent = new StringContent(JsonConvert.SerializeObject(
                    new {name = PublisherDetail.Name}),
                    Encoding.UTF8, 
                    "application/json"
                );

                HttpResponseMessage response = await httpClient
                    .PutAsync($"/api/Publisher/{PublisherDetail.Id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string json = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("HTTP response PublisherModel Update():");
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

        // Makes a HTTP Delete to remove an existsing Publisher from the API. 
        // Only uses the Id to point to the correct API resource. 
        public async void Delete(PublisherDetail PublisherDetail)
        {
            var accessResult = authHandler.AcquireAccessToken().Result;

            if (!string.IsNullOrEmpty(accessResult.AccessToken))
            {
                // Call the API from its base address (set in the http client) + API-endpoint
                HttpResponseMessage response = await httpClient
                    .DeleteAsync($"/api/Publisher/{PublisherDetail.Id}");

                if (response.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string json = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("HTTP response PublisherModel Delete():");
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