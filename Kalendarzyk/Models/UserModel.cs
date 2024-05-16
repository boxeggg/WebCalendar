using Microsoft.AspNetCore.Identity;

namespace Kalendarzyk.Models
{
    public class UserModel : IdentityUser
    {
        public virtual ICollection<EventModel> Events { get; set; }
    }
}
