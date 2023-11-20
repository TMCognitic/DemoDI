using DemoDI.Models;
using DemoDI.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DemoDI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Singleton _singleton;
        private readonly Scoped _scoped;
        private readonly Transient _transient;

        public HomeController(ILogger<HomeController> logger, Singleton singleton, Scoped scoped, Transient transient)
        {
            _logger = logger;
            _singleton = singleton;
            _scoped = scoped;
            _transient = transient;
        }

        public IActionResult Index()
        {
            ViewBag.SingetonByInjection = _singleton.GetHashCode();
            ViewBag.ScopedByInjection = _scoped.GetHashCode();
            ViewBag.TransientByInjection = _transient.GetHashCode();

            Singleton singleton = HttpContext.RequestServices.GetService<Singleton>()!;
            Scoped scoped = HttpContext.RequestServices.GetService<Scoped>()!;
            Transient transient = HttpContext.RequestServices.GetService<Transient>()!;

            ViewBag.SingetonByCall = singleton.GetHashCode();
            ViewBag.ScopedByCall = scoped.GetHashCode();
            ViewBag.TransientByCall = transient.GetHashCode();

            return View();
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
