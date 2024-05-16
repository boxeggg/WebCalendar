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
using Kalendarzyk.Models.ViewModels;

namespace Kalendarzyk.Controllers
{
    public class EventsController : Controller
    {

        private readonly ICalendarRepository _repo;
        public EventsController(ICalendarRepository repo)
        {
            _repo = repo;
        }


        public IActionResult Index()
        {
            if (TempData["Alert"] != null)
            {
                ViewData["Alert"] = TempData["Alert"];
            }
            return View(_repo.GetEvents());
        }

        // GET: Events/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var @event = _repo.GetEvent((int)id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {

            return View(new EventViewModel(_repo.GetLocations()));
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EventViewModel vm, IFormCollection form)
        {

            try
            {
                _repo.CreateEvent(form);
                TempData["Alert"] = "Pomyślnie utworzono nowe wydarzenie dla: " + form["EventModel.Name"];
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["Alert"] = "Napotkano problem: " + ex.Message;
                return View(vm);

            }
        }

        // GET: Events/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventModel = _repo.GetEvent((int)id);
            if (eventModel == null)
            {
                return NotFound();
            }
            var vm = new EventViewModel(eventModel, _repo.GetLocations());
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection form)
        {


            try
            {
                _repo.UpdateEvent(form);
                TempData["Alert"] = "Udało ci się zmodyfikować nazwę dla: " + form["EventModel.name"];
                RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewData["Alert"] = "Napotakno error" + ex.Message;
                var vm = new EventViewModel(_repo.GetEvent(id), _repo.GetLocations());
                return View(vm);
            }
            return RedirectToAction(nameof(Index));
        }

    

        // GET: Events/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventModel = _repo.GetEvent((int)id);
            if (eventModel == null)
            {
                return NotFound();
            }

            return View(eventModel);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.DeleteEvent(id);
            TempData["Alert"] = "Usunąles wydarzenie:";
            return RedirectToAction(nameof(Index));
        }


    }
}
