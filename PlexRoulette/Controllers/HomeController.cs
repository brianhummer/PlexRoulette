using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlexRoulette.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using PlexRoulette.Plex;

namespace PlexRoulette.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var plexApi = new PlexApi();
            var login = await plexApi.SignIn(new UserRequest { login = "brianhummer", password = "pwn89noobs" });
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
