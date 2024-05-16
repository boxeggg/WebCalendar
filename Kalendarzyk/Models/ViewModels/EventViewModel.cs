using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kalendarzyk.Models.ViewModels
{
    public class EventViewModel
    {
        public EventModel EventModel { get; set; }
        public List<SelectListItem> Location { get; set; } = new List<SelectListItem>();
        public string Name { get; set; }
        public EventViewModel(EventModel myevent, List<LocationModel> locations)
        {
            EventModel = myevent;
            Name = myevent.Location.Name;
            foreach (var loc in locations) {
                Location.Add(new SelectListItem() { Text = loc.Name });

            }
        }
        public EventViewModel(List<LocationModel> locations)
        {
            foreach (var loc in locations)
            {
                Location.Add(new SelectListItem() { Text = loc.Name });

            }
        }
        public EventViewModel()
        {

        }
    }
}
