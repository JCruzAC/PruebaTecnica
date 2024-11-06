using AppWeb.Models;
using CapaNegocios.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICargoService _cargoService;

        public HomeController(ILogger<HomeController> logger, ICargoService cargoService)
        {
            _logger = logger;
            _cargoService = cargoService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _cargoService.GetAllCargoByEstado(2);
            return View(lista);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
