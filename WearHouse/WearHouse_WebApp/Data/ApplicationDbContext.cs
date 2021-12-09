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

            modelbuilder.Entity<ApplicationUser>()
                .HasMany<dbComments>(u => u.Comments)
                .WithOne(c => c.Author)
                .HasForeignKey(k => k.userId);

            modelbuilder.Entity<dbWearable>()
                .HasMany(w => w.Comments)
                .WithOne(c => c.Wearable)
                .HasForeignKey(k => k.WearableId);

            base.OnModelCreating(modelbuilder);
        }

        public DbSet<dbWearable> dbWearables { get; set; }
        public DbSet<dbComments> dbComments { get; set; }
    }
}
