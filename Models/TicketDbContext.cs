using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ClientTicketAPI.Models
{
    public class TicketDbContext: DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options):base(options)
        {
        }
        public DbSet<Akt_Tiket> Akt_Tiket { get; set; }
        public DbSet<Sif_Korisnik> Sif_Korisnik { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }
    }
}
