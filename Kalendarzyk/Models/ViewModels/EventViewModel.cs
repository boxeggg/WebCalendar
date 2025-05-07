using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Kalendarzyk.Models.ViewModels
{
    public class EventViewModel
    {
        public EventModel EventModel { get; set; }
        public List<SelectListItem>? Location { get; set; }

        public IEnumerable<LocationModel>? Locations; 



        public EventViewModel() { }

        public EventViewModel(EventModel model, IEnumerable<LocationModel> locations)
        {
            EventModel = model;

            Location = locations.Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.Name
            }).ToList();
        }
    }
}