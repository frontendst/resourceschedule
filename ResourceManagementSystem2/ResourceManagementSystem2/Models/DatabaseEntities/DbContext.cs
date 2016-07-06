using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ResourceManagementSystem2.Models
{
    public class DbContext : System.Data.Entity.DbContext
    {
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Task>()
        //        .Property(p => p.Month).
        //        .HasComputedColumnSql("MONTH([StartTime])");
        //}

        public DbContext()
            : base("RmsRealDataConnection")
        { }

        public DbSet<Programmer> Programmers { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<Task> Tasks { get; set; }
    }
}