using AGDAR.Models;

namespace AGDAR.Services.Interfaces
{
    public interface IStateService
    {
        List<State> GetAll();
        State GetById(int id);
    }
}