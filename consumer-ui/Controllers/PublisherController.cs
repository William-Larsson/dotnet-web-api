using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using consumer_ui.Models;
using Microsoft.AspNetCore.Mvc;

namespace consumer_ui.Controllers
{
    public class PublisherController : Controller
    {
        AuthHandler authHandler = new AuthHandler();

        public async Task<IActionResult> Index()
        {
             // TODO: Move to the ArtistModel.cs class
            HttpClient httpClient = authHandler.httpClient;
            HttpResponseMessage response = await httpClient.GetAsync("/api/Publisher");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Artist Get from Controller: \n" + result);
            }
            else
            {
                Console.WriteLine("HTTP Response is not a success");
            }


            // TODO: Continue here
            List<PublisherDetail> artistList = new List<PublisherDetail>();
            PublisherModel artist = new PublisherModel(); // TODO: Use this class to do HTTP Get, put, post, delete

            artistList.Add(new PublisherDetail()
            {
                Id = 1,
                Name = "Testing Publisher"
            });

            return View(artistList);
        }
    }
}