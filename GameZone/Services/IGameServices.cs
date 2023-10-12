using GameZone.ViewModels;

namespace GameZone.Services
{
    public interface IGameServices
    {
        Task Create(CreateGameVM gameVM);
    }
}
