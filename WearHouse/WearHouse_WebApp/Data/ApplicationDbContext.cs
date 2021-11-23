using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using WearHouse_WebApp.Models;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<ApplicationUser>()
                .HasMany<dbWearable>(u => u.Wearables)
                .WithOne(w => w.ApplicationUser)
                .HasForeignKey(k => k.UserId)
                .IsRequired();

            base.OnModelCreating(modelbuilder);
        }

        //https://stackoverflow.com/questions/51934680/add-relationships-to-the-applicationuser-class-in-asp-net-identity-database-fir
        //public DbSet<ApplicationUser> applicationUser { get; set; }
        public DbSet<dbWearable> dbWearables { get; set; }
        public DbSet<dbComments> dbCommentss { get; set; }
    }
}
