using CountryFriend.Domain.Friend.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryFriend.Services.Friend
{
    public class FriendFriendService : IFriendFriendService
    {
        private IFriendFriendRepository FriendFriendRepository { get; set; }
        public FriendFriendService(IFriendFriendRepository friendFriendRepository)
        {
            this.FriendFriendRepository = friendFriendRepository;
        }
        public IEnumerable<Domain.Friend.FriendFriend> GetAll()
        {
            return FriendFriendRepository.GetAll();
        }
        public async Task<IdentityResult> CreateFriendFriendAsync(Domain.Friend.FriendFriend friend)
        {
            return await FriendFriendRepository.CreateFriendFriendAsync(friend);
        }

        public Domain.Friend.FriendFriend FindById(Guid friendId)
        {
            return FriendFriendRepository.FindById(friendId);
        }

        public async Task<IdentityResult> UpdateFriendFriendAsync(Domain.Friend.FriendFriend newFriendFriend)
        {
            if (this.FriendFriendRepository.GetAll().Where(x => x.Id == newFriendFriend.Id).FirstOrDefault() == null)
            {
                throw new Exception("Friends Friend doesn't exists.");
            }
            return await FriendFriendRepository.UpdateFriendFriendAsync(newFriendFriend);
        }

        public async Task<IdentityResult> DeleteFriendFriendAsync(Guid friendId)
        {
            if (this.FriendFriendRepository.GetAll().Where(x => x.Id == friendId).FirstOrDefault() == null)
            {
                throw new Exception("Friends Friend doesn't exists.");
            }
            return await FriendFriendRepository.DeleteFriendFriendAsync(friendId);
        }
    }
}
