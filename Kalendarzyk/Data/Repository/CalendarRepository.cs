using Kalendarzyk.Models;
using Kalendarzyk.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Kalendarzyk.Data.Repository
{
    public interface ICalendarRepository
    {
        public List<EventModel> GetEvents();
        public List<EventModel> GetMyEvents(string userId);
        public EventModel GetEvent(int id);
        public void CreateEvent(EventViewModel vm, string currentUserName);
        public void UpdateEvent(IFormCollection form);  
        public void DeleteEvent(int id);
        public void DeleteLocation(int id);
        public List<LocationModel> UserLocations(string userId);
        public LocationModel GetLocation(int id);
        public void CreateLocation(LocationModel location);
        
    }
    public class CalendarRepository : ICalendarRepository
    {
        private ApplicationDbContext  db;
        public CalendarRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public List<EventModel> GetEvents()
        {
            return db.Events.ToList();
        }
        public List<EventModel> GetMyEvents(string userId)
        {
            return db.Events.Where(x => x.User.Id == userId).ToList();
        }
        public EventModel GetEvent(int id)
        {
            return db.Events.FirstOrDefault(x => x.Id == id);
        }
        public void CreateEvent(EventViewModel vm, string currentUserName)
        {
           
            var user = db.UserModel
                               .FirstOrDefault(u => u.UserName == currentUserName);
            if (user == null)
                throw new InvalidOperationException("Nie znaleziono zalogowanego użytkownika.");

          
            var location = db.Locations
                                   .FirstOrDefault(l => l.Id == vm.EventModel.LocationId);
            if (location == null)
                throw new InvalidOperationException("Nie znaleziono wskazanej lokalizacji.");

          
            var obj = vm.EventModel;
            obj.UserId = user.Id;         
            obj.User = user;            
            obj.Location = location;      

            
            db.Events.Add(obj);
            db.SaveChanges();
        }
        public void UpdateEvent(IFormCollection form)
        {
            var lokacja = form["Location"].ToString();
            var eventid = int.Parse(form["EventModel.Id"]);
            var myevent = db.Events.FirstOrDefault(x=> x.Id == eventid);
            var locations = db.Locations.FirstOrDefault(x => x.Name == lokacja);
            myevent.UpdateEvent(form, locations);
            db.Entry(myevent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
        public void DeleteEvent(int id)
        {
            var myevent = db.Events.Find(id);
            db.Events.Remove(myevent);
            db.SaveChanges();
        }
        public void DeleteLocation(int id)
        {
            var mylocation = db.Locations.Find(id);
            db.Locations.Remove(mylocation);
            db.SaveChanges();
        }
        public List<LocationModel> UserLocations(string userId)
        {
            return db.Locations.Where(x => x.UserId == userId).ToList();
        }
        public LocationModel GetLocation(int id)
        {
            return db.Locations.Find(id);
        }
        public void CreateLocation(LocationModel location)
        {
            db.Locations.Add(location);
            db.SaveChanges();
        }

    }
}
