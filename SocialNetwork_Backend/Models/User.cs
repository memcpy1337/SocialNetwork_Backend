using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SocialNetwork_Backend.Models
{
    public class User
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string UniqueUrlName { get; set; }
        public string PhotoUrl { get; set; }
        public string status { get; set; }
        public bool followed { get; set; }
    }
}
