using GameZone.Models;
using GameZone.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameServices _gameServies;

        public HomeController(IGameServices gameServies)
        {
            _gameServies = gameServies;
        }

        public IActionResult Index()
        {
           var games= _gameServies.GetAll();
            return View(games);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}