using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientTicketAPI.Models
{
    public class Sif_Korisnik
    {
        public Sif_Korisnik()
        {
        }
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int IdRadnoMesto { get; set; }
        public string Lozinka { get; set; }
        public DateTime Datum { get; set; }
        public bool Aktivan { get; set; }
        public bool PrintFlag { get; set; }
    }
}
