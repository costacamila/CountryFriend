using CountryFriend.Domain.Friend.Repository;
using CountryFriend.Repository.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryFriend.Repository.Friend
{
    public class FriendFriendRepository : IFriendFriendRepository
    {
        private CountryFriendContext Context { get; set; }
        public FriendFriendRepository(CountryFriendContext countryFriendContext)
        {
            this.Context = countryFriendContext;
        }
        public IEnumerable<Domain.Friend.FriendFriend> GetAll()
        {
            return Context.FriendFriends.AsEnumerable();
        }
        public async Task<IdentityResult> CreateFoFAsync(Domain.Friend.FriendFriend friend)
        {
            var account = this.Context.Friends.Where(x => (x.Name.Replace(" ", "").ToLower() + x.Surname.Replace(" ", "").ToLower()) == friend.FriendName.Replace(" ", "").ToLower()).FirstOrDefault();
            friend.Friend = account;
            this.Context.FriendFriends.Add(friend);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }
        public Domain.Friend.FriendFriend FindById(Guid friendId)
        {
            return this.Context.FriendFriends.FirstOrDefault(x => x.Id == friendId);
        }
        public async Task<IdentityResult> UpdateFoFAsync(Domain.Friend.FriendFriend newFriend)
        {
            var oldFriend = Context.FriendFriends.FirstOrDefault(x => x.Id == newFriend.Id);
            oldFriend.Name = newFriend.Name;
            Context.FriendFriends.Update(oldFriend);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> DeleteFoFAsync(Guid friendId)
        {
            var friend = Context.FriendFriends.FirstOrDefault(x => x.Id == friendId);
            using (var transaction = this.Context.Database.BeginTransaction())
            {
                try
                {
                    this.Context.FriendFriends.Remove(friend);
                    await this.Context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException);
                    Console.WriteLine(ex.StackTrace);
                    transaction.Rollback();
                }
            }
            return IdentityResult.Success;
        }
    }
}
