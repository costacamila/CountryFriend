using CountryFriend.Domain.Country.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryFriend.Services.Country
{
    public class CountryService : ICountryService
    {
        private ICountryRepository CountryRepository { get; set; }
        public CountryService(ICountryRepository countryRepository)
        {
            this.CountryRepository = countryRepository;
        }
        public IEnumerable<Domain.Country.Country> GetAll()
        {
            return CountryRepository.GetAll();
        }
        public int DbCount()
        {
            return CountryRepository.DbCount();
        }
        public async Task<IdentityResult> CreateCountryAsync(Domain.Country.Country country)
        {
            return await CountryRepository.CreateCountryAsync(country);
        }
        public Domain.Country.Country FindById(Guid countryId)
        {
            return CountryRepository.FindById(countryId);
        }
        public async Task<IdentityResult> UpdateCountryAsync(Domain.Country.Country newCountry)
        {
            if (this.CountryRepository.GetAll().Where(x => x.Id == newCountry.Id).FirstOrDefault() == null)
            {
                throw new Exception("Country doesn't exists.");
            }
            return await CountryRepository.UpdateCountryAsync(newCountry);
        }
        public async Task<IdentityResult> DeleteCountryAsync(Guid countryId)
        {
            if (this.CountryRepository.GetAll().Where(x => x.Id == countryId).FirstOrDefault() == null)
            {
                throw new Exception("Country doesn't exists.");
            }
            return await CountryRepository.DeleteCountryAsync(countryId);
        }
    }
}
