﻿using Microsoft.EntityFrameworkCore;
using SmokeTestLogin.Data.Entities;
using SmokeTestLogin.Data.Utils;

namespace SmokeTestLogin.Data
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().HasData(SeedData.Seed());

        }
    }
}