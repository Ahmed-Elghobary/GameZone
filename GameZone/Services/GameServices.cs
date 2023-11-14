

using GameZone.Models;
using Microsoft.EntityFrameworkCore;

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
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
        }

        public IEnumerable<Game> GetAll()
        {
           return dbContext.Games.Include(g=>g.Category).Include(g=>g.Devices).ThenInclude(d=>d.Device)
                .AsNoTracking().ToList();
        }

        public Game? GetById(int id)
        {
            return dbContext.Games.Include(g => g.Category).Include(g => g.Devices).ThenInclude(d => d.Device)
               .AsNoTracking().FirstOrDefault(g=>g.Id==id);
        }
        public async Task Create(CreateGameVM gameVM)
        {
          
          var CoverName= await SaveCover(gameVM.Cover);


            Game game = new()
            {
                Name = gameVM.Name,
                Description = gameVM.Description,
                CategoryId = gameVM.CategoryId,
                Cover = CoverName,
                Devices = gameVM.SelectedDevices.Select(d => new GameDevice { DeviceId = d }) .ToList()
            };
            dbContext.Games.Add(game);
            dbContext.SaveChanges();


        }

        #region UpdateHased
        //public async Task<Game?> Update(UpdateGameVM gameVM)
        //{
        //    var game = dbContext.Games.Find(gameVM.GameId);


        //    var hasNewCover= gameVM.Cover is not null;
        //    var oldCover = game.Cover;
        //    game.Name = gameVM.Name;
        //    game.Description = gameVM.Description;
        //    game.CategoryId = gameVM.CategoryId;
        //    game.Devices = gameVM.SelectedDevices.Select(d => new GameDevice {DeviceId=d}).ToList();

        //    if (hasNewCover)
        //    {
        //        game.Cover = await SaveCover(gameVM.Cover!);
        //    }

        //    var EffectedRow = dbContext.SaveChanges();
        //    if (EffectedRow > 0)
        //    {
        //        if (hasNewCover)
        //        {
        //            var cover = Path.Combine(_imagesPath, oldCover);
        //            File.Delete(cover);
        //        }
        //        return game;
        //    }
        //    else
        //    {
        //        var cover = Path.Combine(_imagesPath, game.Cover);
        //        File.Delete(cover);
        //        return null;
        //    }
        //} 
        #endregion

        public async Task<Game?> Update(UpdateGameVM model)
        {
            var game = dbContext.Games
                .Include(g => g.Devices)
                .SingleOrDefault(g => g.Id == model.Id);

            if (game is null)
                return null;

            var hasNewCover = model.Cover is not null;
            var oldCover = game.Cover;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

            if (hasNewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }

            var effectedRows = dbContext.SaveChanges();

            if (effectedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imagesPath, oldCover);
                    File.Delete(cover);
                }

                return game;
            }
            else
            {
                var cover = Path.Combine(_imagesPath, game.Cover);
                File.Delete(cover);

                return null;
            }
        }
        public bool Delete(int id)
        {
            var isDeleted = false;
            var game = dbContext.Games.Find(id);
            if (game is null)
                return isDeleted;

            dbContext.Games.Remove(game);
            var effectedRow=dbContext.SaveChanges();
            if (effectedRow > 0)
            {
                isDeleted = true;
                var cover= Path.Combine(_imagesPath, game.Cover);
                File.Delete(cover);
            }
            return isDeleted;
        }
        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_imagesPath, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);
            return coverName;

        }

        
    }
}
