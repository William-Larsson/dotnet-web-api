using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using consumer_ui.Models;

namespace consumer_ui.Controllers
{
    public class ArtistController : Controller
    {
        AuthHandler authHandler = new AuthHandler();

        public async Task<IActionResult> Index()
        {
            ArtistModel artistModel = new ArtistModel(); // TODO: Use this class to do HTTP Get, put, post, delete
            artistModel.Get();


            // TODO: Continue here
            List<ArtistDetail> artistList = new List<ArtistDetail>();
           
            artistList.Add(new ArtistDetail()
            {
                Id = 1,
                Name = "Testing Artist",
                Songs = new List<string>()
            });

            return View(artistList);
        }
    }
}