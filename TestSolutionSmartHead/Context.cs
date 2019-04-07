using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TestSolutionSmartHead.Models;

namespace TestSolutionSmartHead
{
    public class Context : DbContext
    {
        public Context() : base("DbConnection")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Idea> Ideas { get; set; }
    }
}