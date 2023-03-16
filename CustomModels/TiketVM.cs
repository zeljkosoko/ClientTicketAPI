using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientTicketAPI.CustomModels
{
    public class TiketVM
    {
        //Objekat se pravi i polja se ne ogranicavaju tj popuniti ce sa nekim vrednostima: int sa 0 a string sa NULL i napraviti objekat
        public TiketVM()
        {}
        public TiketVM(
            int id,
            string naslov, 
            string opisProblema,
            DateTime datumKreiran,
            int korisnikZapocet, 
            DateTime datumZapocet,
            int korisnikZavrsen,
            DateTime datumZavrsen, 
            int utroseno,
            string opisResenja,
            int vremeFakturisanja,
            int idVrstaProblema,
            string inicijatorIme,
            string inicijatorPrezime
            )
        {
            this.id = id;
            this.naslov = naslov;
            this.opis = opisProblema;
            this.datumKreiran = datumKreiran;
            this.korisnikZapocet = korisnikZapocet;
            this.datumZapocet = datumZapocet;
            this.korisnikZavrsen = korisnikZavrsen;
            this.datumZavrsen = datumZavrsen;
            this.utroseno = utroseno;
            this.opisResenja = opisResenja;
            this.vremeFakturisanja = vremeFakturisanja;
            this.idVrstaProblema = idVrstaProblema;
            this.inicijatorIme = inicijatorIme;
            this.inicijatorPrezime = inicijatorPrezime;
        }
        public int id { get; set; }
        public string naslov { get; set; }
        public string opis { get; set; }
        public DateTime datumKreiran { get; set; }
        public int korisnikZapocet { get; set; }
        public DateTime datumZapocet { get; set; }
        public int korisnikZavrsen { get; set; }
        public DateTime datumZavrsen { get; set; }
        public int utroseno { get; set; }
        public string opisResenja { get; set; }
        public int vremeFakturisanja { get; set; }
        public int idVrstaProblema { get; set; }
        public string inicijatorIme { get; set; }
        public string inicijatorPrezime { get; set; }

    }
}
