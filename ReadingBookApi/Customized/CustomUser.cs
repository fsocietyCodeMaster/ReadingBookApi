﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ReadingBookApi.Customized
{
    public class CustomUser : IdentityUser
    {
        [StringLength(50)]

        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }
    }
}
