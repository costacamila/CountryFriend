﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountryFriend.Domain.Friend.Repository
{
    public interface IFriendRepository
    {
        IEnumerable<Domain.Friend.Friend> GetAll();
        int DbCount();
        Task<IdentityResult> CreateFriendAsync(Domain.Friend.Friend friend);
        Domain.Friend.Friend FindById(Guid friendId);
        Task<IdentityResult> UpdateFriendAsync(Domain.Friend.Friend newFriend);
        Task<IdentityResult> DeleteFriendAsync(Guid friendId);
    }
}
