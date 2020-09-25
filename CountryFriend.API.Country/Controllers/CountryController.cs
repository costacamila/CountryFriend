using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountryFriend.Services.Country;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace CountryFriend.API.Country.Controllers
{
    [Route("api/country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private ICountryService CountryService { get; set; }
        public CountryController(ICountryService countryService)
        {
            this.CountryService = countryService;
        }
        // GET: api/<CountryController>
        [HttpGet]
        public IEnumerable<Domain.Country.Country> Get()
        {
            return this.CountryService.GetAll();
        }
        // GET api/<CountryController>/5
        [HttpGet("{id}")]
        public Domain.Country.Country Get([FromRoute] Guid id)
        {
            return this.CountryService.GetAll().Where(x => x.Id == id).FirstOrDefault();
        }
        // POST api/<CountryController>
        [HttpPost]
        public async Task<IdentityResult> Post([FromBody] Domain.Country.Country country)
        {
            return await this.CountryService.CreateCountryAsync(country);
        }
        // PUT api/<CountryController>/5
        [HttpPut("edit")]
        public async Task<IdentityResult> Put([FromBody] Domain.Country.Country country)
        {
            return await this.CountryService.UpdateCountryAsync(country);
        }
        // DELETE api/<CountryController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IdentityResult> Delete([FromRoute] Guid id)
        {
            return await this.CountryService.DeleteCountryAsync(id);
        }
        [HttpGet("getbyname/{name}")]
        public Domain.Country.Country GetByName([FromRoute] string name)
        {
            return this.CountryService.GetAll().Where(x => x.Name.ToLower().Replace(" ", "") == name.ToLower().Replace(" ", "")).FirstOrDefault();
        }
        [HttpGet("dbcount")]
        public int DbCount()
        {
            return this.CountryService.DbCount();
        }
    }
}
