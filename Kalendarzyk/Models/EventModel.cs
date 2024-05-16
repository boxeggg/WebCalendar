using System.ComponentModel.DataAnnotations;

namespace Kalendarzyk.Models
{
    public class EventModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set;}
        public DateTime EndTime { get; set; }
        public LocationModel Location { get; set; }
        public UserModel User { get; set; }

        public EventModel(IFormCollection form, LocationModel location) {

            Name = form["EventModel.Name"].ToString();
            Description = form["EventModel.Description"].ToString();
            StartTime = DateTime.Parse(form["EventModel.StartTime"].ToString());
            EndTime = DateTime.Parse(form["EventModel.EndTime"].ToString());
            Location = location;
        }
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
