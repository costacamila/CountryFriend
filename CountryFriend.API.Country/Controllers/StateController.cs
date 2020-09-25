using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountryFriend.Services.State;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace CountryFriend.API.Country.Controllers
{
    [Route("api/state")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private IStateService StateService { get; set; }
        public StateController(IStateService stateService)
        {
            this.StateService = stateService;
        }
        // GET: api/<StateController>
        [HttpGet]
        public IEnumerable<Domain.State.State> Get()
        {
            return this.StateService.GetAll();
        }
        // GET api/<StateController>/5
        [HttpGet("{id}")]
        public Domain.State.State Get([FromRoute] Guid id)
        {
            return this.StateService.GetAll().Where(x => x.Id == id).FirstOrDefault();
        }
        // POST api/<StateController>
        [HttpPost]
        public async Task<IdentityResult> Post([FromBody] Domain.State.State state)
        {
            return await this.StateService.CreateStateAsync(state);
        }
        // PUT api/<StateController>/5
        [HttpPut("edit")]
        public async Task<IdentityResult> Put([FromBody] Domain.State.State state)
        {
            return await this.StateService.UpdateStateAsync(state);
        }
        // DELETE api/<StateController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IdentityResult> Delete([FromRoute] Guid id)
        {
            return await this.StateService.DeleteStateAsync(id);
        }
        [HttpGet("getbyname/{name}")]
        public Domain.State.State GetByName([FromRoute] string name)
        {
            return this.StateService.GetAll().Where(x => x.Name.Trim().ToLower() == name.Trim().ToLower()).FirstOrDefault();
        }
        [HttpGet("dbcount")]
        public int DbCount()
        {
            return this.StateService.DbCount();
        }
    }
}
