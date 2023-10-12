

using GameZone.Services;
using GameZone.ViewModels;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
       
        private readonly ICategoriesServices _categoriesServices;
        private readonly IDevicesServices _devicesServices;
        private readonly IGameServices _gameServices;

        public GamesController(GameDbContext dbContext, ICategoriesServices categoriesServices, IDevicesServices devicesServices, IGameServices gameServices)
        {

            _categoriesServices = categoriesServices;
            _devicesServices = devicesServices;
            _gameServices = gameServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {

            CreateGameVM gameVM = new()
            {
                Categories = _categoriesServices.GetSelectLists(),
                Devices = _devicesServices.GetSelectLists(),
            };
            return View(gameVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(CreateGameVM model)
        {
            if(!ModelState.IsValid)
            {
                model.Categories = _categoriesServices.GetSelectLists();

                model.Devices = _devicesServices.GetSelectLists();

                return View(model);
            }
            await _gameServices.Create(model);
           return RedirectToAction(nameof(Index));

        }

    }
}
