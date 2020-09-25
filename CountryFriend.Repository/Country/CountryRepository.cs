using CountryFriend.Domain.Country.Repository;
using CountryFriend.Repository.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryFriend.Repository.Country
{
    public class CountryRepository : ICountryRepository
    {
        private CountryFriendContext Context { get; set; }
        public CountryRepository(CountryFriendContext countryFriendContext)
        {
            this.Context = countryFriendContext;
        }
        public IEnumerable<Domain.Country.Country> GetAll()
        {
            return Context.Countries.Include(x => x.States).AsEnumerable();
        }
        public int DbCount()
        {
            return this.Context.Countries.Count();
        }
        public async Task<IdentityResult> CreateCountryAsync(Domain.Country.Country country)
        {
            this.Context.Countries.Add(country);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }
        public Domain.Country.Country FindById(Guid countryId)
        {
            return this.Context.Countries.Include(x => x.States).FirstOrDefault(x => x.Id == countryId);
        }
        public async Task<IdentityResult> UpdateCountryAsync(Domain.Country.Country newCountry)
        {
            var oldCountry = Context.Countries.FirstOrDefault(x => x.Id == newCountry.Id);
            oldCountry.Name = newCountry.Name;
            using (var transaction = this.Context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in oldCountry.States)
                    {
                        item.Country = oldCountry;
                        item.CountryName = oldCountry.Name;
                        this.Context.States.Update(item);
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
            Context.Countries.Update(oldCountry);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> DeleteCountryAsync(Guid countryId)
        {
            var country = Context.Countries.Include(x => x.States).FirstOrDefault(x => x.Id == countryId);
            using (var transaction = this.Context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in country.States)
                    {
                        this.Context.States.Remove(item);
                    }
                    this.Context.Countries.Remove(country);
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
