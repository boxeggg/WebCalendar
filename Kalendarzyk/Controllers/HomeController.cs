using Kalendarzyk.Data.Repository;
using Kalendarzyk.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Kalendarzyk.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICalendarRepository _repo;

        public HomeController(ILogger<HomeController> logger, ICalendarRepository repo)
        {
            _repo = repo;
            _logger = logger;

        }

        public IActionResult Index()
        {
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
