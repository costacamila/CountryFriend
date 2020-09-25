using CountryFriend.Domain.State.Repository;
using CountryFriend.Repository.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryFriend.Repository.State
{
    public class StateRepository : IStateRepository
    {
        private CountryFriendContext Context { get; set; }
        public StateRepository(CountryFriendContext countryFriendContext)
        {
            this.Context = countryFriendContext;
        }
        public IEnumerable<Domain.State.State> GetAll()
        {
            return Context.States.AsEnumerable();
        }
        public int DbCount()
        {
            return this.Context.States.Count();
        }
        public async Task<IdentityResult> CreateStateAsync(Domain.State.State state)
        {
            var country = this.Context.Countries.Where(x => x.Name.ToLower().Replace(" ", "") == state.CountryName.ToLower().Replace(" ", "")).FirstOrDefault();
            if (country == null) 
            { 
                throw new Exception("Country doesn't exist."); 
            }
            state.Country = country;
            this.Context.States.Add(state);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }
        public Domain.State.State FindById(Guid stateId)
        {
            return this.Context.States.FirstOrDefault(x => x.Id == stateId);
        }
        public async Task<IdentityResult> UpdateStateAsync(Domain.State.State newState)
        {
            var oldState = Context.States.FirstOrDefault(x => x.Id == newState.Id);
            oldState.Name = newState.Name;
            Context.States.Update(oldState);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> DeleteStateAsync(Guid stateId)
        {
            var state = Context.States.FirstOrDefault(x => x.Id == stateId);
            using (var transaction = this.Context.Database.BeginTransaction())
            {
                try
                {
                    this.Context.States.Remove(state);
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
