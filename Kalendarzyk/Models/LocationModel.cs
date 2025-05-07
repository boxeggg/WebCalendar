using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Kalendarzyk.Models
{
    public class LocationModel
    {
  
        public int Id { get; set; }
        public string Name { get; set; }

        [BindNever]
        [ValidateNever]
        public ICollection<EventModel> Events { get; set; }

        public string UserId { get; set; }

        [BindNever]
        [ValidateNever]
        public UserModel User { get; set; }

    }
}
