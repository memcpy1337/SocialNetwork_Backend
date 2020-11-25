using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Profile Profile { get; set; }
        public ICollection<Post> Posts { get; set; }
        public User()
        {
            Posts = new List<Post>();
        }

    }
    public class Profile
    {
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        public bool LookingForAJob { get; set; }
        public string LookingForAJobDescription { get; set; }
        public string Contacts { get; set; }
        public User User { get; set; }

    }
    public class Post
    {
        public int PostId { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public virtual User User { get; set; }

    }
}
