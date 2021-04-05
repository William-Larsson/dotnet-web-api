using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using consumer_ui.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace consumer_ui.Controllers
{
    [Authorize] // Only logged-in user can access these pages. 
    public class PublisherController : Controller
    {
        private PublisherModel publisherModel = new PublisherModel();

        // Loads the Index.cshtml-page of [URL]/Publisher with
        // PublisherDetails fetched from Web API in the model-class.
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var publisherList = await publisherModel.Get(); 
            return View(publisherList);
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
        // Artist with HTTP Put thorugh the model. 
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] PublisherDetail formData)
        {
            if (String.IsNullOrEmpty(formData.Name))
            {
                ViewBag.incorrectName = "* Cannot be empty";
                return View(formData);
            }
            else
            {
                await publisherModel.Insert(formData);
                return RedirectToAction("Index", "Publisher");
            }
        }

        // Loads the Edit.cshtml-page for a Publisher
        // of a specific Id, for example provided by the @Actionlink 
        // in Index.cshtml. Id is provided in the URI (Publisher/Edit/{id}?...)
        // This does NOT Edit the entry. Only displays the editor page
        [HttpGet]
        public IActionResult Edit(int id, string name)
        {
            return View(new PublisherDetail(){
                Id = id,
                Name = name
            });
        }

        // Called on Post, will use the PublisherModel given from the form to edit
        // the entry from the API with the given ID, then redirect to Publisher start page. 
        // If the edited name is empty or null however, the input is invalid and 
        // a ViewBag will be sent back to the page stating the failed input. 
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] PublisherDetail formData)
        {
            if (String.IsNullOrEmpty(formData.Name))
            {
                ViewBag.incorrectName = "* Cannot be empty";
                return View(formData);
            }
            else
            {
                await publisherModel.Update(formData);
                return RedirectToAction("Index", "Publisher");
            }
        }


        // Loads the Details.cshtml-page for an Publisher
        // of a specific Id, for example provided by the @Actionlink 
        // in Index.cshtml. Id is provided in the URI (Publisher/Details/{id}?...)
        [HttpGet]
        public IActionResult Details(int id, string name)
        {
            return View(new PublisherDetail(){
                Id = id,
                Name = name
            });
        }

        // Loads the Delete.cshtml-page for an Publisher
        // of a specific Id, for example provided by the @Actionlink 
        // in Index.cshtml. Id is provided in the URI (Publisher/Delete/{id}?...)
        // This does NOT delete the entry. Only displays the confirmation page
        [HttpGet]
        public IActionResult Delete(int id, string name)
        {
            return View(new PublisherDetail(){
                Id = id,
                Name = name
            });
        }

        // Called on Post, will use the PublisherModel to delete the entry
        // from the API with the given ID, then redirect to Publisher start page. 
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await publisherModel.Delete(id);
            return RedirectToAction("Index", "Publisher");
        }
    }
}