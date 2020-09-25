using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountryFriend.Services.Friend
{
    public interface IFriendFriendService
    {
        IEnumerable<Domain.Friend.FriendFriend> GetAll();
        Task<IdentityResult> CreateFriendFriendAsync(Domain.Friend.FriendFriend friend);
        Domain.Friend.FriendFriend FindById(Guid friendId);
        Task<IdentityResult> UpdateFriendFriendAsync(Domain.Friend.FriendFriend newFriendFriend);
        Task<IdentityResult> DeleteFriendFriendAsync(Guid friendId);
    }
}
