﻿using Microsoft.AspNetCore.Identity;

namespace Kalendarzyk.Models
{
    public class UserModel : IdentityUser
    {
        public  ICollection<EventModel> Events { get; set; }
        public ICollection<LocationModel> Locations { get; set; }
    }
}
