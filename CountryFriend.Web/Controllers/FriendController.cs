﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CountryFriend.CrossCutting.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace CountryFriend.Web.Controllers
{
    public class FriendController : Controller
    {
        private AzureStorage AzureStorage { get; set; }

        public FriendController(AzureStorage azureStorage)
        {
            this.AzureStorage = azureStorage;
        }

        public IActionResult Index()
        {
            var client = new RestClient();
            var requestFriend = new RestRequest("https://localhost:5003/api/friend");
            return View(client.Get<IEnumerable<Domain.Friend.Friend>>(requestFriend).Data);
        }

        public ActionResult Details(Guid id)
        {
            var client = new RestClient();
            var requestFriend = new RestRequest("https://localhost:5003/api/friend/" + id);
            return View(client.Get<Domain.Friend.Friend>(requestFriend).Data);
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
                                               [FromForm] string phone, [FromForm] DateTime birthday, [FromForm] string country, [FromForm] string state)
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

                var requestFriend = new RestRequest("https://localhost:5003/api/friend");
                requestFriend.AddJsonBody(JsonConvert.SerializeObject(new
                {
                    URL = fileSaved,
                    Filename = filename,
                    Name = name,
                    Surname = surname,
                    Email = email,
                    Phone = phone,
                    Birthday = birthday,
                    Country = country,
                    State = state
                }));

                var client = new RestClient();
                await client.PostAsync<Domain.Friend.Friend>(requestFriend);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                ModelState.AddModelError("APP_ERROR", "Friend already exists.");
                return View();
            }
        }

        public ActionResult Edit(Guid id)
        {
            var client = new RestClient();
            var requestFriend = new RestRequest("https://localhost:5003/api/friend/" + id);

            var friend = (client.Get<Domain.Friend.Friend>(requestFriend).Data);
            this.HttpContext.Session.SetString("FriendId", JsonConvert.SerializeObject(client.Get<Domain.Friend.Friend>(requestFriend).Data.Id.ToString()));
            return View(friend);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Domain.Friend.Friend friend)
        {
            try
            {
                var client = new RestClient();
                var id = JsonConvert.DeserializeObject(this.HttpContext.Session.GetString("FriendId"));

                var requestFriend = new RestRequest("https://localhost:5003/api/friend/edit/");
                requestFriend.AddJsonBody(JsonConvert.SerializeObject(new
                {
                    Id = id,
                    Name = friend.Name
                }));

                await client.PutAsync<Domain.Friend.Friend>(requestFriend);

                this.HttpContext.Session.Remove("FriendId");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                ModelState.AddModelError("APP_ERROR", "Friend doesn't exist.");
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var client = new RestClient();
            var requestFriend = new RestRequest("https://localhost:5003/api/friend/" + id);

            var friend = (client.Get<Domain.Friend.Friend>(requestFriend).Data);
            return View(friend);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteFriend([FromRoute] Guid id)
        {
            try
            {
                var client = new RestClient();
                var requestFriend = new RestRequest("https://localhost:5003/api/friend/delete/" + id);
                await client.DeleteAsync<Domain.Friend.Friend>(requestFriend);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                ModelState.AddModelError("APP_ERROR", "Friend doesn't exist.");
                return View();
            }
        }
    }
}
