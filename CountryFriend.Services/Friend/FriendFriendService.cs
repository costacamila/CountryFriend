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
        public async Task<IdentityResult> CreateFoFAsync(Domain.Friend.FriendFriend friend)
        {
            if (this.FriendFriendRepository.GetAll().Where(x => x.Name.ToLower().Replace(" ", "")  == friend.Name.ToLower().Replace(" ", "")).FirstOrDefault() != null)
            {
                throw new Exception("Friends Friend already exists.");
            }
            return await FriendFriendRepository.CreateFoFAsync(friend);
        }

        public Domain.Friend.FriendFriend FindById(Guid friendId)
        {
            return FriendFriendRepository.FindById(friendId);
        }

        public async Task<IdentityResult> UpdateFoFAsync(Domain.Friend.FriendFriend newFoF)
        {
            if (this.FriendFriendRepository.GetAll().Where(x => x.Id  == newFoF.Id).FirstOrDefault() == null)
            {
                throw new Exception("Friends Friend doesn't exists.");
            }
            return await FriendFriendRepository.UpdateFoFAsync(newFoF);
        }

        public async Task<IdentityResult> DeleteFoFAsync(Guid friendId)
        {
            if (this.FriendFriendRepository.GetAll().Where(x => x.Id == friendId).FirstOrDefault() == null)
            {
                throw new Exception("Friends Friend doesn't exists.");
            }
            return await FriendFriendRepository.DeleteFoFAsync(friendId);
        }
    }
}
