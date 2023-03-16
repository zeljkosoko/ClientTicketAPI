using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClientTicketAPI.Models
{
    public class Akt_Tiket
    {
        public Akt_Tiket()
        {
        }
        public Akt_Tiket(
            string tiket,
            int status,
            string naslov,
            string opisProblema,
            int radnikKreiran,
            DateTime datumKreiran,
            int? korisnikZapocet,
            DateTime? datumZapocet,
            int? korisnikZavrsen,
            DateTime? datumZavrsen,
            int? utroseno,
            #nullable enable
            string? opisResenja,
            int? vremeFakturisanja,
            int? idVrstaProblema,
            bool? poslatTiket,
            int? idKorisnikInicijator
            )
        {
            Tiket = tiket;
            Status = status;
            Naslov = naslov;
            OpisProblema = opisProblema;
            RadnikKreiran = radnikKreiran;
            DatumKreiran = datumKreiran;
            KorisnikZapocet = korisnikZapocet;
            DatumZapocet = datumZapocet;
            KorisnikZavrsen = korisnikZavrsen;
            DatumZavrsen = datumZavrsen;
            Utroseno = utroseno;
            OpisResenja = opisResenja;
            VremeFakturisanja = vremeFakturisanja;
            IdVrstaProblema = idVrstaProblema;
            PoslatTiket = poslatTiket;
            IdKorisnikInicijator = idKorisnikInicijator;
        }
        public int Id { get; set; }

        [MaxLength(9)]
        public string Tiket { get; set; }
        public int Status { get; set; }
        public string Naslov { get; set; }
        public string OpisProblema { get; set; }
        public int RadnikKreiran { get; set; }
        public DateTime DatumKreiran { get; set; }
        public int? KorisnikZapocet { get; set; }
        public DateTime? DatumZapocet { get; set; }
        public int? KorisnikZavrsen { get; set; }
        public DateTime? DatumZavrsen { get; set; }
        public int? Utroseno { get; set; }
        public string? OpisResenja { get; set; }
        public int? VremeFakturisanja { get; set; }
        public int? IdVrstaProblema { get; set; }
        public bool? PoslatTiket { get; set; }
        public int? IdKorisnikInicijator { get; set; }
    }
}
