using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountryFriend.Services.Country
{
    public interface ICountryService
    {
        IEnumerable<Domain.Country.Country> GetAll();
        int DbCount();
        Task<IdentityResult> CreateCountryAsync(Domain.Country.Country country);
        Domain.Country.Country FindById(Guid countryId);
        Task<IdentityResult> UpdateCountryAsync(Domain.Country.Country newCountry);
        Task<IdentityResult> DeleteCountryAsync(Guid countryId);
    }
}
