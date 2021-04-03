using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

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

        // TODO: returntype?
        // TODO: Do I even need this method?
        public void GetById()
        {
            
        }

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

        // TODO: returntype?
        public async void Insert()
        {
            var accessResult = authHandler.AcquireAccessToken().Result;

            if (!string.IsNullOrEmpty(accessResult.AccessToken))
            {
                // Call the API from its base address (set in the http client) + API-endpoint
                // TODO: Continue here!! 
                // Check saved Firefox-links for how to use PostAsync
                HttpResponseMessage response = await httpClient.PostAsync("/api/Artist", new StringContent("TODO!!!"));

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

            // TODO: Return the result in some way?
        }

        // TODO: returntype?
        public void Update()
        {
            
        }

        // TODO: returntype?
        public void Delete()
        {
            
        }

    }
}