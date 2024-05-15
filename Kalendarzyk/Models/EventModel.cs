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

    }
}
