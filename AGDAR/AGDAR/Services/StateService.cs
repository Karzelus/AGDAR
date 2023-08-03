using AGDAR.Models;
using AGDAR.Repositories;
using AGDAR.Services.Interfaces;
using AutoMapper;

namespace AGDAR.Services
{
    public class StateService : IStateService
    {
        private readonly StateRepository _stateRepository;
        public StateService(StateRepository stateRepository) //Constructor
        {
            _stateRepository = stateRepository;
        }
        public State GetById(int id)   //GetById
        {
            var state = _stateRepository.GetById(id);
            if (state is null)
            {
                return null;
            }
            return state;
        }

        public List<State> GetAll() //GetAll
        {
            List<State> states = _stateRepository.GetAll().ToList();
            return states;
        }
    }
}
