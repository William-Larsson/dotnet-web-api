using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

namespace consumer_ui.Models
{
    public class SongModel
    {
        // TODO: in this class, make operations for HTTP Get, put, post, delete
        // Example: see assignment 3 from previous course and the relation between the classes of the same name 
        // connect (maybe) to SongDetail, SongController and AuthHandler (?)

        private AuthHandler authHandler;
        private HttpClient httpClient;

        public SongModel()
        {
            authHandler = new AuthHandler();
            httpClient = authHandler.httpClient;
        }

        // For getting a single Song by its Id. 
        // Uses API GET. 
        public void GetById()
        {
            throw new NotImplementedException();
        }

        // For getting a all Songs from API. 
        // Uses API GET. 
        public async Task<List<SongDetail>> Get()
        {
            var accessResult = authHandler.AcquireAccessToken().Result;
            List<SongDetail> songList = null;

            if (!string.IsNullOrEmpty(accessResult.AccessToken))
            {
                // Call the API from its base address (set in the http client) + API-endpoint
                HttpResponseMessage response = await httpClient.GetAsync("/api/Song");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    songList = JsonConvert
                        .DeserializeObject<IEnumerable<SongDetail>>(json).ToList();
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

            return songList;
        }


        // Makes a HTTP Post to insert a new Song into the API. 
        // Only uses the SongDetail name for request. 
        public async Task Insert(SongDetail SongDetail)
        {
            var accessResult = authHandler.AcquireAccessToken().Result;

            if (!string.IsNullOrEmpty(accessResult.AccessToken))
            {
                // Call the API from its base address (set in the http client) + API-endpoint
                // with the json content. 
                var tempJson = JsonConvert.SerializeObject(new 
                    {
                        title = SongDetail.Title,
                        releaseDate = SongDetail.ReleaseDate.ToString("d").Replace('/', '-'),
                        genre = SongDetail.Genre,
                        publisherId = SongDetail.PublisherId,
                        artistsIds = SongDetail.ArtistIds
                    });

                var jsonContent = new StringContent(tempJson,
                    Encoding.UTF8, 
                    "application/json"
                );

                HttpResponseMessage response = await httpClient
                    .PostAsync("/api/Song", jsonContent);

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

        // Makes a HTTP Put to update an existsing Song into the API. 
        // Not implemented as the HTTP Put-operation is currently,
        // not functioning for /api/Song/{id}
        public void Update(SongDetail SongDetail)
        {
            throw new NotImplementedException();
        }

        // Makes a HTTP Delete to remove an existsing Song from the API. 
        // Only uses the Id to point to the correct API resource. 
        public async Task Delete(int id)
        {
            var accessResult = authHandler.AcquireAccessToken().Result;

            if (!string.IsNullOrEmpty(accessResult.AccessToken))
            {
                // Call the API from its base address (set in the http client) + API-endpoint
                HttpResponseMessage response = await httpClient
                    .DeleteAsync($"/api/Song/{id}");

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