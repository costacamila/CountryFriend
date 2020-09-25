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
    [Route("api/friendfriend")]
    [ApiController]
    public class FriendFriendController : ControllerBase
    {
        private IFriendFriendService FriendFriendService { get; set; }
        public FriendFriendController(IFriendFriendService friendfriendService)
        {
            this.FriendFriendService = friendfriendService;
        }
        // GET: api/<FriendFriendController>
        [HttpGet]
        public IEnumerable<Domain.Friend.FriendFriend> Get()
        {
            return this.FriendFriendService.GetAll();
        }

        // GET api/<FriendFriendController>/5
        [HttpGet("{id}")]
        public Domain.Friend.FriendFriend Get([FromRoute] Guid id)
        {
            return this.FriendFriendService.GetAll().Where(x => x.Id == id).FirstOrDefault();
        }
        // POST api/<FriendFriendController>
        [HttpPost]
        public async Task<IdentityResult> Post([FromBody] Domain.Friend.FriendFriend friendfriend)
        {
            return await this.FriendFriendService.CreateFriendFriendAsync(friendfriend);
        }
        // PUT api/<FriendFriendController>/5
        [HttpPut("edit")]
        public async Task<IdentityResult> Put([FromBody] Domain.Friend.FriendFriend friendfriend)
        {
            return await this.FriendFriendService.UpdateFriendFriendAsync(friendfriend);
        }
        // DELETE api/<FriendFriendController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IdentityResult> Delete([FromRoute] Guid id)
        {
            return await this.FriendFriendService.DeleteFriendFriendAsync(id);
        }
        [HttpGet("getbyname/{name}")]
        public Domain.Friend.FriendFriend GetByName([FromRoute] string name)
        {
            return this.FriendFriendService.GetAll().Where(x => x.Name.Replace(" ", "").ToLower() == name.Replace(" ", "").ToLower()).FirstOrDefault();
        }
    }
}
