using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CountryFriend.Web.Models;
using RestSharp;

namespace CountryFriend.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult DbCounter()
        {
            var list = new List<int>();

            var client = new RestClient();
            var requestFriendCount = new RestRequest("https://localhost:5003/api/friend/dbcount");
            var requestCountryCount = new RestRequest("https://localhost:5005/api/country/dbcount");
            var requestStateCount = new RestRequest("https://localhost:5005/api/state/dbcount");

            var friendCount = client.Get<int>(requestFriendCount).Data;
            var countryCount = client.Get<int>(requestCountryCount).Data;
            var stateCount = client.Get<int>(requestStateCount).Data;

            list.Add(friendCount);
            list.Add(countryCount);
            list.Add(stateCount);

            return View(list);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
