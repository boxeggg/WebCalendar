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
        private readonly ApplicationDbContext _context;
        public EventsController(ICalendarRepository repo, ApplicationDbContext context)
        {
            _repo = repo;
            _context = context; 
        }


        public IActionResult Index()
        {
            var user = _context.UserModel.FirstOrDefault(n => n.UserName == User.Identity.Name);

            if (TempData["Alert"] != null)
            {
                ViewData["Alert"] = TempData["Alert"];
            }
            return View(_repo.GetMyEvents(user.Id));
        }


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


        public IActionResult Create()
        {
            var user = _context.UserModel.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var model = new EventViewModel
            {
                EventModel = new EventModel
                {
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(1)
                },
                Location = _repo.UserLocations(user.Id).Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.Name
                }).ToList()
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EventViewModel vm)
        {
            var user = _context.UserModel.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (user == null) return Unauthorized();

            vm.Location = _repo.UserLocations(user.Id).Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.Name
            }).ToList();
            vm.EventModel.UserId = user.Id;
            vm.EventModel.User = user;
            ModelState.Clear();
            TryValidateModel(vm.EventModel);

            if (ModelState.IsValid)
            {

                _context.Events.Add(vm.EventModel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }




            return View(vm);
        }


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
            var user = _context.UserModel.FirstOrDefault(n => n.UserName == User.Identity.Name);
            var userId = user.Id;
            var vm = new EventViewModel(eventModel, _repo.UserLocations(userId));
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EventViewModel vm)
        {
            // 0. Pobierz aktualnie zalogowanego użytkownika
            var user = _context.UserModel
                               .FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (user == null)
                return Unauthorized();

            // 1. Upewnij się, że path id i model.Id się zgadzają
            if (id != vm.EventModel.Id)
                return BadRequest();

            // 2. Sprawdź, czy ten event rzeczywiście należy do aktualnego usera
            if (!_context.Events.Any(e => e.Id == id && e.UserId == user.Id))
                return Forbid();

            // 3. Przygotuj dropdown z lokalizacjami użytkownika
            vm.Location = _repo.UserLocations(user.Id)
                              .Select(l => new SelectListItem
                              {
                                  Value = l.Id.ToString(),
                                  Text = l.Name
                              })
                              .ToList();

            // 4. Nadpisz UserId z kodu, żeby nikt nie podmienił w formularzu
            vm.EventModel.UserId = user.Id;

            // 5. Wyczyść stary ModelState i ręcznie zwaliduj EventModel
            ModelState.Clear();
            TryValidateModel(vm.EventModel, prefix: "EventModel");

            // 6. Jeśli coś nie tak – pokaż formularz z błędami
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            // 7. Wszystko OK – aktualizuj i zapisz
            _context.Events.Update(vm.EventModel);
            _context.SaveChanges();
            return RedirectToAction("Index");
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
