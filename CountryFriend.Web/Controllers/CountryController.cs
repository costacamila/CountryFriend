using System;
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
    public class CountryController : Controller
    {
        private AzureStorage AzureStorage { get; set; }

        public CountryController(AzureStorage azureStorage)
        {
            this.AzureStorage = azureStorage;
        }

        public IActionResult Index()
        {
            var client = new RestClient();
            var requestCountry = new RestRequest("https://localhost:5005/api/country");
            return View(client.Get<IEnumerable<Domain.Country.Country>>(requestCountry).Data);
        }

        public ActionResult Details(Guid id)
        {
            var client = new RestClient();
            var requestCountry = new RestRequest("https://localhost:5005/api/country/" + id);
            return View(client.Get<Domain.Country.Country>(requestCountry).Data);
        }

        // GET: AuthorController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] IFormFile file, [FromForm] string name)
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


                var requestCountry = new RestRequest("https://localhost:5005/api/country");
                requestCountry.AddJsonBody(JsonConvert.SerializeObject(new
                {
                    URL = fileSaved,
                    Filename = filename,
                    Name = name
                }));


                var client = new RestClient();
                //var requestCountry = new RestRequest("https://localhost:5005/api/country");
                await client.PostAsync<Domain.Country.Country>(requestCountry);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("APP_ERROR", "Country already exists.");
                return View();
            }
        }

        public ActionResult Edit(Guid id)
        {
            var client = new RestClient();
            var requestCountry = new RestRequest("https://localhost:5005/api/country/" + id);

            var country = (client.Get<Domain.Country.Country>(requestCountry).Data);
            this.HttpContext.Session.SetString("CountryId", JsonConvert.SerializeObject(client.Get<Domain.Country.Country>(requestCountry).Data.Id.ToString()));
            return View(country);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Domain.Country.Country country)
        {
            try
            {
                var client = new RestClient();
                var id = JsonConvert.DeserializeObject(this.HttpContext.Session.GetString("CountryId"));

                var requestCountry = new RestRequest("https://localhost:5005/api/country/edit/");
                requestCountry.AddJsonBody(JsonConvert.SerializeObject(new
                {
                    Id = id,
                    Name = country.Name
                }));

                await client.PutAsync<Domain.Country.Country>(requestCountry);

                this.HttpContext.Session.Remove("CountryId");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                ModelState.AddModelError("APP_ERROR", "Country doesn't exist.");
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var client = new RestClient();
            var requestCountry = new RestRequest("https://localhost:5005/api/country/" + id);

            var country = (client.Get<Domain.Country.Country>(requestCountry).Data);
            return View(country);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAuthor([FromRoute] Guid id)
        {
            try
            {
                var client = new RestClient();
                var requestCountry = new RestRequest("https://localhost:5005/api/country/delete/" + id);
                await client.DeleteAsync<Domain.Country.Country>(requestCountry);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                ModelState.AddModelError("APP_ERROR", "Country doesn't exist.");
                return View();
            }
        }
    }
}
