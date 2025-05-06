using System.ComponentModel.DataAnnotations;

namespace Kalendarzyk.Models
{
    public class LocationModel
    {
  
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<EventModel> Events { get; set; }

        public string UserId { get; set; }
        public UserModel User { get; set; }

    }
}
