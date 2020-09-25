using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountryFriend.Domain.Friend.Repository
{
    public interface IFriendFriendRepository
    {
        IEnumerable<Domain.Friend.FriendFriend> GetAll();
        Task<IdentityResult> CreateFoFAsync(Domain.Friend.FriendFriend friend);
        Domain.Friend.FriendFriend FindById(Guid friendId);
        Task<IdentityResult> UpdateFoFAsync(Domain.Friend.FriendFriend newFriendFriend);
        Task<IdentityResult> DeleteFoFAsync(Guid friendId);
    }
}
