using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure.Design;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialNetwork_Backend.Models;

namespace SocialNetwork_Backend.BDContext
{
    public class UserContext : DbContext
    {
        public UserContext() { }
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}
