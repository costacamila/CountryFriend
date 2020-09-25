using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CountryFriend.CrossCutting.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace CountryFriend.Web.Controllers
{
    public class FriendFriendController : Controller
    {
        private AzureStorage AzureStorage { get; set; }

        public FriendFriendController(AzureStorage azureStorage)
        {
            this.AzureStorage = azureStorage;
        }

        public IActionResult Index()
        {
            var client = new RestClient();
            var requestFriendFriend = new RestRequest("https://localhost:5003/api/friendfriend");
            return View(client.Get<IEnumerable<Domain.Friend.FriendFriend>>(requestFriendFriend).Data);
        }

        public ActionResult Details(Guid id)
        {
            var client = new RestClient();
            var requestFriendFriend = new RestRequest("https://localhost:5003/api/friendfriend/" + id);
            return View(client.Get<Domain.Friend.FriendFriend>(requestFriendFriend).Data);
        }

        // GET: AuthorController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] IFormFile file, [FromForm] string name, [FromForm] string surname, [FromForm] string email,
                                [FromForm] string phone, [FromForm] DateTime birthday, [FromForm] string country, [FromForm] string state, [FromForm] string friendname)
        {
            try
            {
                var filename = $"{Guid.NewGuid().ToString().Replace("-", "")}.jpg";

                var ms = new MemoryStream();

                using (var fileUpload = file.OpenReadStream())
                {
                    fileUpload.CopyTo(ms);
                    fileUpload.Close();
                }
                var fileSaved = await this.AzureStorage.SaveToStorage(ms.ToArray(), filename);

                var requestFriendFriend = new RestRequest("https://localhost:5003/api/friendfriend");
                requestFriendFriend.AddJsonBody(JsonConvert.SerializeObject(new
                {
                    URL = fileSaved,
                    Filename = filename,
                    Name = name,
                    Surname = surname,
                    Email = email,
                    Phone = phone,
                    Birthday = birthday,
                    Country = country,
                    State = state,
                    FriendName = friendname
                }));

                var client = new RestClient();
                await client.PostAsync<Domain.Friend.FriendFriend>(requestFriendFriend);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                ModelState.AddModelError("APP_ERROR", "FriendFriend already exists.");
                return View();
            }
        }

        public ActionResult Edit(Guid id)
        {
            var client = new RestClient();
            var requestFriendFriend = new RestRequest("https://localhost:5003/api/friendfriend/" + id);

            var friendfriend = (client.Get<Domain.Friend.FriendFriend>(requestFriendFriend).Data);
            this.HttpContext.Session.SetString("FriendFriendId", JsonConvert.SerializeObject(client.Get<Domain.Friend.FriendFriend>(requestFriendFriend).Data.Id.ToString()));
            return View(friendfriend);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Domain.Friend.FriendFriend friendfriend)
        {
            try
            {
                var client = new RestClient();
                var id = JsonConvert.DeserializeObject(this.HttpContext.Session.GetString("FriendFriendId"));

                var requestFriendFriend = new RestRequest("https://localhost:5003/api/friendfriend/edit/");
                requestFriendFriend.AddJsonBody(JsonConvert.SerializeObject(new
                {
                    Id = id,
                    Name = friendfriend.Name
                }));

                await client.PutAsync<Domain.Friend.FriendFriend>(requestFriendFriend);

                this.HttpContext.Session.Remove("FriendFriendId");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                ModelState.AddModelError("APP_ERROR", "FriendFriend doesn't exist.");
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var client = new RestClient();
            var requestFriendFriend = new RestRequest("https://localhost:5003/api/friendfriend/" + id);

            var friendfriend = (client.Get<Domain.Friend.FriendFriend>(requestFriendFriend).Data);
            return View(friendfriend);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteFriendFriend([FromRoute] Guid id)
        {
            try
            {
                var client = new RestClient();
                var requestFriendFriend = new RestRequest("https://localhost:5003/api/friendfriend/delete/" + id);
                await client.DeleteAsync<Domain.Friend.FriendFriend>(requestFriendFriend);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                ModelState.AddModelError("APP_ERROR", "FriendFriend doesn't exist.");
                return View();
            }
        }
    }
}
