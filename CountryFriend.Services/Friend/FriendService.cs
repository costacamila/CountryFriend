using CountryFriend.Domain.Friend.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryFriend.Services.Friend
{
    public class FriendService : IFriendService
    {
        private IFriendRepository FriendRepository { get; set; }
        public FriendService(IFriendRepository friendRepository)
        {
            this.FriendRepository = friendRepository;
        }
        public IEnumerable<Domain.Friend.Friend> GetAll()
        {
            return FriendRepository.GetAll();
        }
        public int DbCount()
        {
            return FriendRepository.DbCount();
        }
        public async Task<IdentityResult> CreateFriendAsync(Domain.Friend.Friend friend)
        {
            if (this.FriendRepository.GetAll().Where(x => x.Name.ToLower().Replace(" ", "") == friend.Name.ToLower().Replace(" ", "")).FirstOrDefault() != null)
            {
                throw new Exception("Friend already exists.");
            }
            return await FriendRepository.CreateFriendAsync(friend);
        }
        public Domain.Friend.Friend FindById(Guid friendId)
        {
            return FriendRepository.FindById(friendId);
        }
        public async Task<IdentityResult> UpdateFriendAsync(Domain.Friend.Friend newFriend)
        {
            if (this.FriendRepository.GetAll().Where(x => x.Id == newFriend.Id).FirstOrDefault() == null)
            {
                throw new Exception("Friend doesn't exists.");
            }
            return await FriendRepository.UpdateFriendAsync(newFriend);
        }
        public async Task<IdentityResult> DeleteFriendAsync(Guid friendId)
        {
            if (this.FriendRepository.GetAll().Where(x => x.Id == friendId).FirstOrDefault() == null)
            {
                throw new Exception("Friend doesn't exists.");
            }
            return await FriendRepository.DeleteFriendAsync(friendId);
        }
    }
}
