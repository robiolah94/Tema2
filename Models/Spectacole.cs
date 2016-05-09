using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Spectacole
    {
        public int SpectacoleiD { get; set; }
        public String Titlu { get; set; }
        public String Regia { get; set; }
        public String Distributia { get; set; }
        public DateTime Data { get; set; }
        public int NumarBilete { get; set; }
    }

    public class SpectacoleDBContext : DbContext
    {
        public DbSet<Spectacole> Spectacole { get; set; }
    } 
}