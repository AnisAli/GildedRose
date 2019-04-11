﻿using Microsoft.EntityFrameworkCore;
using GildedRose.Data.Models;

namespace GildedRose.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ApplicationUser> Users { get; set; }
        public virtual DbSet<ApplicationRole> Roles { get; set; }
    }
}
