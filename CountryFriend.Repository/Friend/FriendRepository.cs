using CountryFriend.Domain.Friend.Repository;
using CountryFriend.Repository.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryFriend.Repository.Friend
{
    public class FriendRepository : IFriendRepository
    {
        private CountryFriendContext Context { get; set; }
        public FriendRepository(CountryFriendContext countryFriendContext)
        {
            this.Context = countryFriendContext;
        }
        public IEnumerable<Domain.Friend.Friend> GetAll()
        {
            return Context.Friends.Include(x => x.Friends).AsEnumerable();
        }
        public int DbCount()
        {
            return this.Context.Friends.Count();
        }
        public async Task<IdentityResult> CreateFriendAsync(Domain.Friend.Friend friend)
        {
            this.Context.Friends.Add(friend);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }
        public Domain.Friend.Friend FindById(Guid friendId)
        {
            return this.Context.Friends.Include(x => x.Friends).FirstOrDefault(x => x.Id == friendId);
        }
        public async Task<IdentityResult> UpdateFriendAsync(Domain.Friend.Friend newFriend)
        {
            var oldFriend = Context.Friends.FirstOrDefault(x => x.Id == newFriend.Id);
            oldFriend.Name = newFriend.Name;
            using (var transaction = this.Context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in oldFriend.Friends)
                    {
                        item.Friend = oldFriend;
                        item.FriendName = oldFriend.Name;
                        this.Context.FriendFriends.Update(item);
                    }
                    await this.Context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    transaction.Rollback();
                }
            }
            Context.Friends.Update(oldFriend);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> DeleteFriendAsync(Guid friendId)
        {
            var friend = Context.Friends.Include(x => x.Friends).FirstOrDefault(x => x.Id == friendId);
            using (var transaction = this.Context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in friend.Friends)
                    {
                        this.Context.FriendFriends.Remove(item);
                    }
                    this.Context.Friends.Remove(friend);
                    await this.Context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    transaction.Rollback();
                }
            }
            return IdentityResult.Success;
        }
    }
}
