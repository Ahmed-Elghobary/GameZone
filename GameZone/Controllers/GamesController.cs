

using GameZone.Services;
using GameZone.ViewModels;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {

        private readonly ICategoriesServices _categoriesServices;
        private readonly IDevicesServices _devicesServices;
        private readonly IGameServices _gameServices;

        public GamesController(ICategoriesServices categoriesServices, IDevicesServices devicesServices, IGameServices gameServices)
        {

            _categoriesServices = categoriesServices;
            _devicesServices = devicesServices;
            _gameServices = gameServices;
        }

        public IActionResult Index()
        {
            var game = _gameServices.GetAll();
            return View(game);
        }
        public IActionResult Details(int id)
        {
            var game = _gameServices.GetById(id);
            if (game is null)
            {
                return NotFound();
            }

            return View(game);
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
        public async Task<IActionResult> Create(CreateGameVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesServices.GetSelectLists();

                model.Devices = _devicesServices.GetSelectLists();

                return View(model);
            }
            await _gameServices.Create(model);
            return RedirectToAction(nameof(Index));

        }


      

        [HttpGet]
        public IActionResult Update(int id)
        {
            var game = _gameServices.GetById(id);

            if (game is null)
                return NotFound();

            UpdateGameVM viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _categoriesServices.GetSelectLists(),
                Devices = _devicesServices.GetSelectLists(),
                CurrentCover = game.Cover
            };

            return View(viewModel);
        }

     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateGameVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesServices.GetSelectLists();
                model.Devices = _devicesServices.GetSelectLists();
                return View(model);
            }

            var game = await _gameServices.Update(model);

            if (game is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var Isdeleted=_gameServices.Delete(id);
            return Isdeleted? Ok():BadRequest();
        }
    }
}
