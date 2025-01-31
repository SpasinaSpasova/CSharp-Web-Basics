﻿using Microsoft.AspNetCore.Identity;

namespace Library.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<ApplicationUserBook> ApplicationUsersBooks { get; set; }  = new List<ApplicationUserBook>();
    }
}
