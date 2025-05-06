using Kalendarzyk.Data;
using Kalendarzyk.Data.Repository;
using Kalendarzyk.Models;
using Kalendarzyk.Models.ViewModels;
using Kalendarzyk.Serializer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;



namespace Kalendarzyk.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICalendarRepository _repo;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ICalendarRepository repo,ApplicationDbContext context)
        {
            _repo = repo;
            _logger = logger;
            _context = context;

        }

        public IActionResult Index()
        {
            var user = _context.UserModel.FirstOrDefault(n => n.UserName == User.Identity.Name);
            var userId = user.Id;
            ViewData["res"] = JSONhelper.GetResourceJson(_repo.UserLocations(userId));
            ViewData["eve"] = JSONhelper.GetEventsJson(_repo.GetEvents());

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
