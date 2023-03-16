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
    public class BillController : ControllerBase
    {
        private readonly TicketDbContext _context;
        public BillController(TicketDbContext context)
        {
            _context = context;
        }

        [HttpPut]
        public string Bill(TiketVM tiketVM)
        {
            //int id, int vremeFakturisanja
            //Fakturisanje tiketa:
            //1. Nadji tiket sa datim id-jem i statusom 3 i nepopunjenim vremenom fakturisanja
            //2. Unesi vremeFakturisanja
            //3. Ako tiket nije pronadjen ili je vec fakturisan, vrati odgovarajucu poruku. Inace, vrati poruku da je uspesno fakturisan.
            try
            {
                Akt_Tiket billedTicket = _context.Akt_Tiket.Where(t => t.Id == tiketVM.id && t.Status == 3 && (t.VremeFakturisanja == null || t.VremeFakturisanja == 0)).FirstOrDefault();

                billedTicket.VremeFakturisanja = tiketVM.vremeFakturisanja;
                _context.SaveChanges();

                return "Tiket je uspešno fakturisan.";
            }
            catch (NullReferenceException)
            {
                return "Ne postoji takav tiket ili je već fakturisan.";
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
                return "Tiket nije fakturisan u bazi klijenta. Servisu nisu prosledjeni ispravni podaci.";
            }
            catch (Exception)
            {
                return "Došlo je do greške.";
            }
        }
    }
}
