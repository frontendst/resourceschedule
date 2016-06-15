using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ResourceManagementSystem2.Models
{
    public class DbContext : System.Data.Entity.DbContext
    {
        public DbContext()
            : base("RmsConnection")
        { }

        public DbSet<Programmer> Programmers { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Specialization> Specializations { get; set; }
    }
}