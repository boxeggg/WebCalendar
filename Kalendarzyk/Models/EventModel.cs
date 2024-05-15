using System.ComponentModel.DataAnnotations;

namespace Kalendarzyk.Models
{
    public class EventModel
    {
        [Key] 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set;}
        public DateTime EndTime { get; set; }
        public LocationModel Location { get; set; }
        public UserModel User { get; set; }

        public EventModel(IFormCollection form, LocationModel location) {
            Id = int.Parse(form["Id"]);
            Name = form["Name"];
            Description = form["Description"];
            StartTime = DateTime.Parse(form["StartTime"]);
            EndTime = DateTime.Parse(form["EndTime"]);
            Location = location;
        }
        public void UpdateEvent(IFormCollection form, LocationModel location)
        {
            Id = int.Parse(form["Id"]);
            Name = form["Name"];
            Description = form["Description"];
            StartTime = DateTime.Parse(form["StartTime"]);
            EndTime = DateTime.Parse(form["EndTime"]);
            Location = location;
        }
        public EventModel() { }
    }
    
}
