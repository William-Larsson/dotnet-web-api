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
        public async Task<List<PublisherDetail>> Get()
        {
            var accessResult = authHandler.AcquireAccessToken().Result;
            List<PublisherDetail> publisherList = null;

            if (!string.IsNullOrEmpty(accessResult.AccessToken))
            {
                // Call the API from its base address (set in the http client) + API-endpoint
                HttpResponseMessage response = await httpClient.GetAsync("/api/Publisher");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    publisherList = JsonConvert
                        .DeserializeObject<IEnumerable<PublisherDetail>>(json).ToList();
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

            return publisherList;
        }


        // Makes a HTTP Post to insert a new Publisher into the API. 
        // Only uses the PublisherDetail name for request. 
        public async Task Insert(PublisherDetail PublisherDetail)
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
                    string json = await response.Content.ReadAsStringAsync();
                } 
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Failed to call the Web Api: {response.StatusCode}");
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Content: {content}");
                    Console.ResetColor();
                }
            }

            // TODO: Return the result (json or as an object) in some way?
        }

        // Makes a HTTP Put to update an existsing Publisher into the API. 
        // Only uses the PublisherDetail name for request, but uses Id 
        // to point to the correct API resource. 
        public async Task Update(PublisherDetail PublisherDetail)
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
                    string json = await response.Content.ReadAsStringAsync();
                } 
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Failed to call the Web Api: {response.StatusCode}");
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Content: {content}");
                    Console.ResetColor();
                }
            }

             // TODO: Return the result in some way?
        }

        // Makes a HTTP Delete to remove an existsing Publisher from the API. 
        // Only uses the Id to point to the correct API resource. 
        public async Task Delete(int id)
        {
            var accessResult = authHandler.AcquireAccessToken().Result;

            if (!string.IsNullOrEmpty(accessResult.AccessToken))
            {
                // Call the API from its base address (set in the http client) + API-endpoint
                HttpResponseMessage response = await httpClient
                    .DeleteAsync($"/api/Publisher/{id}");

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
                    Console.ResetColor();
                }
            }

             // TODO: Return the result in some way?
        }
    }
}