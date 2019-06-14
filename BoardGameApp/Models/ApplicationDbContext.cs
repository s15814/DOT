using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BoardGameApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<BoardGame> BoardGames { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<BoardGameCopy> BoardGameCopies { get; set; }

        public ApplicationDbContext() : base("Database", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}