﻿using LxGreg.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LxGreg.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Order> orders { get; set; }
        public DbSet<Stock> stocks { get; set; }
        public DbSet<Unit> units { get; set; }
        public DbSet<Store> stores { get; set; }
        public DbSet<Asset> assets { get; set; }
        public DbSet<Manager> managers { get; set; }
    }
}
