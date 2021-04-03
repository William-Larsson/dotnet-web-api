using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace consumer_ui.Controllers
{
    public class SongController : Controller
    {
        AuthHandler authHandler = new AuthHandler();

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}