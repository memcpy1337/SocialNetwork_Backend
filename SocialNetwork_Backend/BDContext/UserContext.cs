using Microsoft.EntityFrameworkCore;
using SocialNetwork_Backend.Models;
using System;

namespace SocialNetwork_Backend.BDContext
{
    public class UserContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public UserContext() { }
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            //        modelBuilder.Entity<User>()
            //.HasMany(p => p.Posts)
            //.WithOne(p => p.User);
            //        modelBuilder.Entity<Profile>()
            //    .HasOne(u => u.User)
            //    .WithOne(u => u.Profile)
            //    .IsRequired();

            Random rnd = new Random();
            int postId = 2;
            for (int i = 1; i < 500; i++)
            {
                int robotid = rnd.Next(100, 10000);
                User user = new User
                {
                    Id = i,
                    Name = "Robot-" + robotid,
                    UniqueUrlName = "robotpage-" + robotid,
                    PhotoUrl = "https://mirprogramm.ru/wp-content/uploads/2018/02/Discord-logo.png",
                    status = "I am slave " + robotid,
                    followed = false
                };
                int posts = rnd.Next(0, 10);
                object[] vars = new object[posts];
                for (int k = 0; k < posts; k++)
                {
                    vars[k] = new
                    {
                        UserId = user.Id,
                        PostId = postId,
                        Text = "Ya robot number " + robotid,
                        Likes = rnd.Next(0, 100)

                    };
                    postId++;
                }

                modelBuilder.Entity<User>(b =>
                {
                    b.HasKey(o => o.Id);
                    b.Property(o => o.Id).ValueGeneratedOnAdd();
                    b.HasData(user);
                    b.OwnsOne(p => p.Profile).HasData(new
                    {
                        ProfileId = i,
                        UserId = user.Id,
                        LookingForAJob = false,
                        LookingForAJobDescription = "Looking for a job down",
                        Contacts = "vk.com/robot"
                    });

                    b.OwnsMany(o => o.Posts).HasData(vars);
                });
            }

        }
    
    }
}

