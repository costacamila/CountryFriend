using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CountryFriend.Web.Controllers
{
    public class FriendController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
