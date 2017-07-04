using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace opinion_Service.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> UserAccount { get; set; }
        public DbSet<Opinion> Opinions { get; set; }
    }
}