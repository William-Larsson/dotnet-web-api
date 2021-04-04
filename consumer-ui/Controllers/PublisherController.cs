using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using consumer_ui.Models;
using Microsoft.AspNetCore.Mvc;

namespace consumer_ui.Controllers
{
    [Authorize] // Only logged-in user can access these pages. 
    public class PublisherController : Controller
    {
        AuthHandler authHandler = new AuthHandler();

        public async Task<IActionResult> Index()
        {
            // TODO: Continue here
            List<PublisherDetail> publisherList = new List<PublisherDetail>();
            PublisherModel publisherModel = new PublisherModel(); // TODO: Use this class to do HTTP Get, put, post, delete

            var publisherDetail = new PublisherDetail()
            {
                Id = 9,
                Name = "Testing Publisher Update"
            };

            publisherList.Add(publisherDetail);


            publisherModel.Get(); // OK
            //publisherModel.Insert(publisherDetail); // OK
            //publisherModel.Update(publisherDetail); // OK
            //publisherModel.Delete(publisherDetail); // OK


            return View(publisherList);
        }
    }
}