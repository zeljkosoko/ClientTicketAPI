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
    public class StartController : ControllerBase
    {
        private readonly TicketDbContext _context;
        public StartController(TicketDbContext context)
        {
            _context = context;
        }

        [HttpPut]
        public string Start(TiketVM tiketVM)
        {
            //int id, int korisnikZapocet, DateTime datumZapocet
            //Zapocinjanje tiketa:
            //1. Nadji tiket sa datim id-jem i statusom 1
            //2. Promeni status tiketa u 2, unesi id korisnika koji je zapoceo, datum zapocinjanja
            //3. Ako tiket nije pronadjen ili je vec zapocet vrati odgovarajucu poruku. Inace, vrati poruku da je zapocet.
            try
            {
                Akt_Tiket startedTicket = _context.Akt_Tiket.Where(t => t.Id == tiketVM.id && t.Status == 1).FirstOrDefault(); 

                startedTicket.KorisnikZapocet = 1; //Uvek Zebracon Solutions kod njih
                startedTicket.DatumZapocet = tiketVM.datumZapocet;
                startedTicket.Status = 2;

                _context.SaveChanges();

                return "Tiket je uspešno započet.";
            }
            catch (NullReferenceException)
            {
                return "Ne postoji takav tiket ili je već započet.";
            }
            catch (SqlNullValueException)
            {
                return "Korisnik ili datum u bazi ne može biti prazan.";
            }
            catch (SqlException)
            {
                return "Nije uspostavljena veza sa bazom.";
            }
            catch (DbUpdateException)
            {
                return "Tiket nije započet u bazi klijenta. Servisu nisu prosledjeni ispravni podaci.";
            }
            catch (Exception)
            {
                return "Došlo je do greške.";
            }
        }
    }
}
