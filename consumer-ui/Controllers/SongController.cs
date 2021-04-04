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
    public class SongController : Controller
    {
        AuthHandler authHandler = new AuthHandler();

        public IActionResult Index()
        {
            SongModel SongModel = new SongModel(); // TODO: Use this class to do HTTP Get, put, post, delete

            // TODO: Continue here
            List<SongDetail> SongList = new List<SongDetail>();

            var artistIds = new List<int>();
            artistIds.Add(6);
           
            var SongDetail = new SongDetail()
            {
                Id = 14,
                Title = "Testing Testing Testing",
                ReleaseDate = DateTime.Now,
                Genre = "Test Pop",
                PublisherName = "TSTNG Studios",
                PublisherId = 8,
                ArtistNames = new List<string>(),
                ArtistIds = artistIds
            };

            SongList.Add(SongDetail);

            SongModel.Get(); // OK
            // SongModel.Insert(SongDetail); // OK
            // SongModel.Delete(SongDetail); // OK

            return View(SongList);
        }
    }
}