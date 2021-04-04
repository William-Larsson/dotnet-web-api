using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using consumer_ui.Models;
using Microsoft.AspNetCore.Authorization;

namespace consumer_ui.Controllers
{
    [Authorize] // Only logged-in user can access these pages. 
    public class ArtistController : Controller
    {
        AuthHandler authHandler = new AuthHandler();

        public async Task<IActionResult> Index()
        {
            ArtistModel artistModel = new ArtistModel(); // TODO: Use this class to do HTTP Get, put, post, delete

            // TODO: Continue here
            List<ArtistDetail> artistList = new List<ArtistDetail>();
           
            var artistDetail = new ArtistDetail()
            {
                Id = 10,
                Name = "Testing Artist New",
                Songs = new List<string>()
            };

            artistList.Add(artistDetail);

            artistModel.Get(); // OK
            //artistModel.Insert(artistDetail); // OK
            //artistModel.Update(artistDetail); // OK
            //artistModel.Delete(artistDetail); // OK

            return View(artistList);
        }
    }
}