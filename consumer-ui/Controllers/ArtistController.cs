using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using consumer_ui.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace consumer_ui.Controllers
{
    [Authorize] // Only logged-in user can access these pages. 
    public class ArtistController : Controller
    {
        private ArtistModel artistModel = new ArtistModel();

        // Loads the Index.cshtml-page of [URL]/Artist with
        // ArtistDetails fetched from Web API in the model-class.
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var artistList = await artistModel.Get(); 
            return View(artistList);
        }


        // Loads the Insert.cshtml-file. 
        // This does NOT Insert an entry. Only displays the editor page
        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] ArtistDetail formData)
        {
            if (String.IsNullOrEmpty(formData.Name))
            {
                ViewBag.incorrectName = "* Cannot be empty";
                return View(formData);
            }
            else
            {
                await artistModel.Insert(formData);
                return RedirectToAction("Index", "Artist");
            }
        }


        // Loads the Edit.cshtml-page for an artist
        // of a specific Id, for example provided by the @Actionlink 
        // in Index.cshtml. Id is provided in the URI (Artist/Edit/{id}?...)
        // This does NOT Edit the entry. Only displays the editor page
        [HttpGet]
        public IActionResult Edit(int id, string name)
        {
            return View(new ArtistDetail(){
                Id = id,
                Name = name
            });
        }

        // Called on Post, will use the ArtistModel given from the form to edit
        // the entry from the API with the given ID, then redirect to Artist start page. 
        // If the edited name is empty or null however, the input is invalid and 
        // a ViewBag will be sent back to the page stating the failed input. 
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] ArtistDetail formData)
        {
            if (String.IsNullOrEmpty(formData.Name))
            {
                ViewBag.incorrectName = "* Cannot be empty";
                return View(formData);
            }
            else
            {
                await artistModel.Update(formData);
                return RedirectToAction("Index", "Artist");
            }
        }


        // Loads the Details.cshtml-page for an artist
        // of a specific Id, for example provided by the @Actionlink 
        // in Index.cshtml. Id is provided in the URI (Artist/Details/{id}?...)
        [HttpGet]
        public IActionResult Details(int id, string name, List<string> songTitles)
        {
            return View(new ArtistDetail(){
                Id = id,
                Name = name,
                SongTitles = songTitles
            });
        }

        // Loads the Delete.cshtml-page for an artist
        // of a specific Id, for example provided by the @Actionlink 
        // in Index.cshtml. Id is provided in the URI (Artist/Delete/{id}?...)
        // This does NOT delete the entry. Only displays the confirmation page
        [HttpGet]
        public IActionResult Delete(int id, string name, List<string> songTitles)
        {
            return View(new ArtistDetail(){
                Id = id,
                Name = name,
                SongTitles = songTitles
            });
        }

        // Called on Post, will use the ArtistModel to delete the entry
        // from the API with the given ID, then redirect to Artist start page. 
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await artistModel.Delete(id);
            return RedirectToAction("Index", "Artist");
        }
    }
}