using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Bilete
    {
        public int BileteiD { get; set; }
        public String Spectacol { get; set; }
        public int rand { get; set; }
        public int numar { get; set; }
    }

    public class BileteDBContext : DbContext
    {
        public DbSet<Bilete> Bilete { get; set; }
    } 
}