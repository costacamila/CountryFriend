using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountryFriend.Services.State
{
    public interface IStateService
    {
        IEnumerable<Domain.State.State> GetAll();
        int DbCount();
        Task<IdentityResult> CreateStateAsync(Domain.State.State state);
        Domain.State.State FindById(Guid stateId);
        Task<IdentityResult> UpdateStateAsync(Domain.State.State newState);
        Task<IdentityResult> DeleteStateAsync(Guid stateId);
    }
}
