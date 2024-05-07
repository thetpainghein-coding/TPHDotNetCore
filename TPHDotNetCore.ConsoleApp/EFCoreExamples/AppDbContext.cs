﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TPHDotNetCore.ConsoleApp.Services;

namespace TPHDotNetCore.ConsoleApp.EFCoreExamples
{
    internal class AppDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString); //connect the database used
        }
        public DbSet<BlogDto> Blogs { get; set; }
    }
}