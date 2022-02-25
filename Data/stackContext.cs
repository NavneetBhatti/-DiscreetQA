using Microsoft.EntityFrameworkCore;
using Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Data
{
    public class stackContext:DbContext
    {
        public stackContext(DbContextOptions<stackContext> options) : base(options)
        {
        }

        public DbSet<Answer> Answer { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Question> Question { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>().ToTable("Answer");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Question>().ToTable("Question");
        }
    }
}




 

