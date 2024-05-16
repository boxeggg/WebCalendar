using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kalendarzyk.Data;
using Kalendarzyk.Models;
using Kalendarzyk.Data.Repository;

namespace Kalendarzyk.Controllers
{
    public class LocationController : Controller
    {

        private readonly ICalendarRepository _repo;

        public LocationController(ICalendarRepository repo)
        {
            _repo = repo;
        }


        // GET: Location
        public IActionResult Index()
        {
            if (TempData["Alert"] != null)
            {
                ViewData["Alert"] = TempData["Alert"];
            }
            return View(_repo.GetLocations());
        }

        // GET: Location/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationModel = _repo.GetLocation((int)id);
            if (locationModel == null)
            {
                return NotFound();
            }

            return View(locationModel);
        }

        // GET: Location/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] LocationModel locationModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.CreateLocation(locationModel);
                    TempData["Alert"] = "Udało ci się dodać lokacje";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ViewData["Alert"] = "Wystąpił bład: " + ex.Message;
                    return View(locationModel);
                }
            }
            return View(locationModel);
        }
    }
}

