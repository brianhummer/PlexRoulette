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
using PlexRoulette.Services;
using PlexRoulette.Models.RouletteViewModels;

namespace PlexRoulette.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlexApi _plexApi;

        private static string AuthToken;
        private static List<Directory> Libraries;
        private static Dictionary<string, Metadata[]> LibraryMetadata;

        public HomeController(IPlexApi plexApi)
        {
            _plexApi = plexApi;
        }
        public async Task<IActionResult> Index()
        {
            if(Libraries == null || Libraries.Count == 0)
                await UpdatePlexData();

            return View(Libraries);
        }

        [HttpPost]
        public async Task<IActionResult> RandomThree([FromBody]SelectedLibrary selected)
        {
            if(LibraryMetadata == null || LibraryMetadata.Count == 0)
                await UpdatePlexData();

            var temp = LibraryMetadata[selected.LibraryId].Where(x => x.viewedLeafCount == 0).ToList();
            List<Metadata> randomThree = new List<Metadata>();
            var rand = new Random(DateTime.Now.Millisecond);

            for(int i = 0; i < 3; i++)
            {
                var selection = rand.Next(0, temp.Count - 1);
                randomThree.Add(temp[selection]);

                temp.Remove(temp[selection]);
            }

            var viewModel = new RouletteSelectionViewModel
            {
                LibraryId = selected.LibraryId,
                WatchOptions = randomThree
            };

            return PartialView("RandomThree", viewModel);
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

        private async Task UpdatePlexData()
        {
            if(AuthToken == null)
                AuthToken = (await _plexApi.SignIn()).user.authentication_token;

            Libraries = (await _plexApi.GetLibrarySections(AuthToken)).MediaContainer.Directory;
            LibraryMetadata = new Dictionary<string, Metadata[]>();

            for (int i = 0; i < Libraries.Count; i++)
            {
                LibraryMetadata.Add(Libraries[i].key, (await _plexApi.GetLibrary(AuthToken, Libraries[i].key)).MediaContainer.Metadata);
            }
        }
    }
}
