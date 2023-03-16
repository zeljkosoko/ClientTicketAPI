using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ClientTicketAPI.Models;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using ClientTicketAPI.CustomModels;

namespace ClientTicketAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FinishController : ControllerBase
    {
        private readonly TicketDbContext _context;
        public FinishController(TicketDbContext context)
        {
            _context = context;
        }

        [HttpPut]
        public string Finish(TiketVM tiketVM)
        {
            //int id, int korisnikZavrsen, DateTime datumZavrsen, int utroseno, string opisResenja, int? vremeFakturisanja, int idVrstaProblema
            //Zavrsavanje tiketa:
            //1. Nadji tiket sa datim id-jem i statusom 2
            //2. Promeni status tiketa u 3, unesi id korisnika koji je zavrsio i kada je zavrsio, koliko je vremena utroseno, opis resenja
            //3. Ako tiket nije pronadjen ili je vec zavrsen, vrati odgovarajucu poruku. Inace, vrati poruku da je uspesno zavrsen.
            try
            {
                Akt_Tiket finishedTicket = _context.Akt_Tiket.Where(t => t.Id == tiketVM.id && t.Status == 2).FirstOrDefault();

                finishedTicket.KorisnikZavrsen = 1;
                finishedTicket.DatumZavrsen = tiketVM.datumZavrsen;
                finishedTicket.Utroseno = tiketVM.utroseno;
                finishedTicket.OpisResenja = tiketVM.opisResenja;
                //servis mora da unese neku vrednost za INT polje
                finishedTicket.VremeFakturisanja = tiketVM.vremeFakturisanja;
                finishedTicket.IdVrstaProblema = tiketVM.idVrstaProblema;
                finishedTicket.Status = 3;

                _context.SaveChanges();

                return "Tiket je uspešno završen.";
            }
            catch (NullReferenceException)
            {
                return "Ne postoji takav tiket ili je već završen.";
            }
            catch (SqlNullValueException)
            {
                return "Nisu uneti svi obavezni podaci.";
            }
            catch (SqlException)
            {
                return "Nije uspostavljena veza sa bazom.";
            }
            catch (DbUpdateException)
            {
                return "Tiket nije završen u bazi klijenta. Servisu nisu prosledjeni ispravni podaci.";
            }
            catch (Exception)
            {
                return "Došlo je do greške.";
            }
        }
    }
}
