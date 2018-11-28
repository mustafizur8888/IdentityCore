﻿using Microsoft.AspNetCore.Identity;

namespace PluralsightDemo
{
    public class PluralsightUser : IdentityUser
    {
        public string Local { get; set; } = "en-GB";
        public string OrgId { get; set; }
    }

    public class Organization
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
