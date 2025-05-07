using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Kalendarzyk.Models
{
    public class EventModel
    {

        [BindNever]
        [ValidateNever]
        public LocationModel Location { get; set; }

        [BindNever]
        [ValidateNever]
        public UserModel User { get; set; }
    
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartTime { get; set;}
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public int LocationId { get; set; }

        public string UserId { get; set; }

        public void UpdateEvent(IFormCollection form, LocationModel location)
        {

            Name = form["EventModel.Name"].ToString();
            Description = form["EventModel.Description"].ToString();
            StartTime = DateTime.Parse(form["EventModel.StartTime"].ToString());
            EndTime = DateTime.Parse(form["EventModel.EndTime"].ToString());
            Location = location;
        }
        public EventModel() { }
    }
    
}
