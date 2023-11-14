using GameZone.ViewModels;

namespace GameZone.Services
{
    public interface IGameServices
    {
        IEnumerable<Game> GetAll();
        Game? GetById(int id);
        Task Create(CreateGameVM gameVM);
        Task<Game?> Update(UpdateGameVM gameVM);
        bool Delete(int id);
        
          
        
    }
}
