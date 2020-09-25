using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace CountryFriend.Web.Controllers
{
    public class StateController : Controller
    {
        public IActionResult Index()
        {
            var client = new RestClient();
            var requestState = new RestRequest("https://localhost:5005/api/state");
            return View(client.Get<IEnumerable<Domain.State.State>>(requestState).Data);
        }

        public ActionResult Details(Guid id)
        {
            var client = new RestClient();
            var requestState = new RestRequest("https://localhost:5005/api/state/" + id);
            return View(client.Get<Domain.State.State>(requestState).Data);
        }

        // GET: AuthorController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Domain.State.State state)
        {
            try
            {
                var client = new RestClient();
                var requestState = new RestRequest("https://localhost:5005/api/state");
                await client.PostAsync<Domain.State.State>(requestState);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("APP_ERROR", "State already exists.");
                return View();
            }
        }

        public ActionResult Edit(Guid id)
        {
            var client = new RestClient();
            var requestState = new RestRequest("https://localhost:5005/api/state/" + id);

            var state = (client.Get<Domain.State.State>(requestState).Data);
            this.HttpContext.Session.SetString("StateId", JsonConvert.SerializeObject(client.Get<Domain.State.State>(requestState).Data.Id.ToString()));
            return View(state);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Domain.State.State state)
        {
            try
            {
                var client = new RestClient();
                var id = JsonConvert.DeserializeObject(this.HttpContext.Session.GetString("StateId"));

                var requestState = new RestRequest("https://localhost:5005/api/state/edit/");
                requestState.AddJsonBody(JsonConvert.SerializeObject(new
                {
                    Id = id,
                    Name = state.Name
                }));

                await client.PutAsync<Domain.State.State>(requestState);

                this.HttpContext.Session.Remove("StateId");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                ModelState.AddModelError("APP_ERROR", "State doesn't exist.");
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var client = new RestClient();
            var requestState = new RestRequest("https://localhost:5005/api/state/" + id);

            var state = (client.Get<Domain.State.State>(requestState).Data);
            return View(state);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAuthor([FromRoute] Guid id)
        {
            try
            {
                var client = new RestClient();
                var requestState = new RestRequest("https://localhost:5005/api/state/delete/" + id);
                await client.DeleteAsync<Domain.State.State>(requestState);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                ModelState.AddModelError("APP_ERROR", "State doesn't exist.");
                return View();
            }
        }
    }
}
