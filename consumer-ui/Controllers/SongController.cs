using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using consumer_ui.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace consumer_ui.Controllers
{
    [Authorize] // Only logged-in user can access these pages. 
    public class SongController : Controller
    {
        SongModel songModel = new SongModel();

        // Loads the Index.cshtml-page of [URL]/Song with
        // SongDetails fetched from Web API in the model-class.
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var songList = await songModel.Get(); 
            songList = songList.OrderBy(song => song.ReleaseDate).ToList();
            return View(songList);
        }

        // Loads the Insert.cshtml-file. 
        // This does NOT Insert an entry. Only displays the editor page
        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        // Called when submitting the form on the Insert.cshtml page
        // If the user input is not empty or null, insert the new 
        // Song with HTTP Put thorugh the model. 
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] SongDetail formData, string artistIdsString)
        {
            var artistIds = this.ValidateArtistIds(artistIdsString).Result;
            var publisherIsValid = this.ValidatePublisherId(formData.PublisherId).Result;

            if (ModelState.IsValid && artistIds.isValid && publisherIsValid)
            {
                formData.ArtistIds = artistIds.artistIds;
                await songModel.Insert(formData);
                return RedirectToAction("Index", "Song");
            }

            if (!artistIds.isValid)
            {
                ViewBag.incorrectIds = "* Required. IDs must be space separated integer(s).";
            }

            if (!publisherIsValid)
            {
                ViewBag.incorrectPublisherId = "* No publisher of that ID could be found.";
            }

            ViewBag.artistIdsString = artistIdsString;
            return View(formData);

        }

        // Loads the Details.cshtml-page for a Song
        // of a specific Id, for example provided by the @Actionlink 
        // in Index.cshtml. Id is provided in the URI (Song/Details/{id}?...)
        [HttpGet]
        public IActionResult Details(int id, string title, DateTime relDate, string genre, string pubName, List<string> artistNames)
        {
            return View(new SongDetail()
            {
                Id = id,
                Title = title,
                ReleaseDate = relDate,
                Genre = genre,
                PublisherName = pubName,
                ArtistNames = artistNames
            });
        }

        // Loads the Delete.cshtml-page for a song
        // of a specific Id, for example provided by the @Actionlink 
        // in Index.cshtml. Id is provided in the URI (Song/Delete/{id}?...)
        // This does NOT delete the entry. Only displays the confirmation page
        [HttpGet]
        public IActionResult Delete(int id, string title, DateTime relDate, string genre, string pubName, List<string> artistNames)
        {
            return View(new SongDetail()
            {
                Id = id,
                Title = title,
                ReleaseDate = relDate,
                Genre = genre,
                PublisherName = pubName,
                ArtistNames = artistNames
            });
        }

        // Called on Post, will use the SongModel to delete the entry
        // from the API with the given ID, then redirect to Song start page. 
        [HttpPost]
        public async Task<IActionResult> Delete (int id)
        {
            await songModel.Delete(id);
            return RedirectToAction("Index", "Song");
        }

        // Returns a tuple of (bool, List<int>).
        // Fetches Artists from the API to compare input to. 
        // If valid, the tuple with be (true, List<>)
        // indicating that the IDs are valid, as well as
        // returning a list of all the Artists IDs as int. 
        private async Task<(bool isValid, List<int> artistIds)> ValidateArtistIds(string artistIdsString)
        {
            if (!String.IsNullOrEmpty(artistIdsString))
            {
                char[] delimiterChars = { ' ', ',', '-', ':', '\t', '\n' };
                var idStrings = artistIdsString.Split(delimiterChars).ToList();

                try
                {   // Parsing can throw exception
                    List<int> ids = idStrings.Select(id => Int32.Parse(id)).ToList();
                    var artistModel = new ArtistModel();
                    var artistList = await artistModel.Get(); 

                    foreach (var id in ids)
                    {
                        if (!artistList.Any(artist => artist.Id == id))
                            return (false, null);
                    }
                    return (true, ids.ToList());
                }
                catch (FormatException)
                {
                    return (false, null);
                }
            }
            return (false, null);
        }

        // Fetches Publishers from the API to compare input to.
        // If the selected publisher ID is valid, return true. 
        private async Task<bool> ValidatePublisherId(int id)
        {
            var publisherModel = new PublisherModel();
            var publishers = await publisherModel.Get();

            if (publishers.Any(pub => pub.Id == id))
                return true;
            else
                return false; 
        }
    }
}