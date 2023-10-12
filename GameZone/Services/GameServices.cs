using GameZone.Models;
using GameZone.ViewModels;
using Microsoft.Extensions.Primitives;

namespace GameZone.Services
{
    public class GameServices:IGameServices
    {
        private readonly GameDbContext dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;

        public GameServices(GameDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}/assets/images/games";
        }

        public async Task Create(CreateGameVM gameVM)
        {
           var coverName=$"{Guid.NewGuid()}{Path.GetExtension(gameVM.Cover.FileName)}";
            var path = Path.Combine(_imagesPath,coverName);

            using var stream=File.Create(path);
            await gameVM.Cover.CopyToAsync(stream);
            stream.Dispose();

            Game game = new()
            {
                Name=gameVM.Name,
                Description=gameVM.Description,
                CategoryId=gameVM.CategoryId,
                 Cover=coverName,
                 GameDevices = gameVM.SelectedDevices.Select(d=>new GameDevice { DeviceId=d}).ToList()
            };
            dbContext.Games.Add(game);
            dbContext.SaveChanges();


        }
    }
}
