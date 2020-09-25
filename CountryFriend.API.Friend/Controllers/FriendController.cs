using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountryFriend.Services.Friend;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace CountryFriend.API.Friend.Controllers
{
    [Route("api/friend")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private IFriendService FriendService { get; set; }
        private IFriendFriendService FriendFriendService { get; set; }
        public FriendController(IFriendService friendService, IFriendFriendService friendFriendService)
        {
            this.FriendService = friendService;
            this.FriendFriendService = friendFriendService;
        }
        // GET: api/<FriendController>
        [HttpGet]
        public IEnumerable<Domain.Friend.Friend> Get()
        {
            return this.FriendService.GetAll();
        }
        // GET api/<FriendController>/5
        [HttpGet("{id}")]
        public Domain.Friend.Friend Get([FromRoute] Guid id)
        {
            return this.FriendService.GetAll().Where(x => x.Id == id).FirstOrDefault();
        }
        // POST api/<FriendController>
        [HttpPost]
        public async Task<IdentityResult> Post([FromBody] Domain.Friend.Friend friend)
        {
            return await this.FriendService.CreateFriendAsync(friend);
        }
        // PUT api/<FriendController>/5
        [HttpPut("edit")]
        public async Task<IdentityResult> Put([FromBody] Domain.Friend.Friend friend)
        {
            return await this.FriendService.UpdateFriendAsync(friend);
        }
        // DELETE api/<FriendController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IdentityResult> Delete([FromRoute] Guid id)
        {
            return await this.FriendService.DeleteFriendAsync(id);
        }
        [HttpGet("getbyname/{name}")]
        public Domain.Friend.Friend GetByName([FromRoute] string name)
        {
            return this.FriendService.GetAll().Where(x => x.Name.Replace(" ", "").ToLower() == name.Replace(" ", "").ToLower()).FirstOrDefault();
        }
        [HttpGet("dbcount")]
        public int DbCount()
        {
            return this.FriendService.DbCount();
        }
    }
}
