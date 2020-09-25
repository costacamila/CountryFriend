using CountryFriend.Domain.State.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryFriend.Services.State
{
    public class StateService : IStateService
    {
        private IStateRepository StateRepository { get; set; }
        public StateService(IStateRepository stateRepository)
        {
            this.StateRepository = stateRepository;
        }
        public IEnumerable<Domain.State.State> GetAll()
        {
            return StateRepository.GetAll();
        }
        public int DbCount()
        {
            return StateRepository.DbCount();
        }
        public async Task<IdentityResult> CreateStateAsync(Domain.State.State state)
        {
            if (this.StateRepository.GetAll().Where(x => x.Name.ToLower().Replace(" ", "") == state.Name.ToLower().Replace(" ", "")).FirstOrDefault() != null)
            {
                throw new Exception("State already exists.");
            }
            return await StateRepository.CreateStateAsync(state);
        }
        public Domain.State.State FindById(Guid stateId)
        {
            return StateRepository.FindById(stateId);
        }
        public async Task<IdentityResult> UpdateStateAsync(Domain.State.State newState)
        {
            if (this.StateRepository.GetAll().Where(x => x.Id  == newState.Id).FirstOrDefault() == null)
            {
                throw new Exception("State doesn't exists.");
            }
            return await StateRepository.UpdateStateAsync(newState);
        }
        public async Task<IdentityResult> DeleteStateAsync(Guid stateId)
        {
            if (this.StateRepository.GetAll().Where(x => x.Id == stateId).FirstOrDefault() == null)
            {
                throw new Exception("State doesn't exists.");
            }
            return await StateRepository.DeleteStateAsync(stateId);
        }
    }
}
