using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ClientTicketAPI.Models;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;
using ClientTicketAPI.CustomModels;
using Microsoft.EntityFrameworkCore;

namespace ClientTicketAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CreateController : ControllerBase  
    {
        private readonly TicketDbContext _context;
        public CreateController(TicketDbContext dbContext)
        {
            _context = dbContext;
        }

        //---------Postupak kreiranje tiketa kod klijenta:
        //1. Salje se inicijator kreiranja tiketa sa naslovom i opisom
        //2. Servis treba da vrati broj tiketa koji je kreirao kod klijenta
        //3. Ako servis ne uspe da kreira tiket, vraca odgovarajuci odgovor 

        [HttpPost]
        public string Create(TiketVM tiketVM)
        {
            //string inicijator, string naslov, string opis
            //ako su podaci pogresnog tipa, servis nece ni da se odazove
            //inicijator moze imati razlicit id kod nas i kod klijenta jer ima vise klijenata-zato prvo trazimo id od klijenta u bazi pa ga onda saljemo da je on zahtevao
            try
            {
                int idInicijator = _context.Sif_Korisnik.SingleOrDefault(k => k.Ime + k.Prezime == tiketVM.inicijatorIme + tiketVM.inicijatorPrezime).Id;
                    
                Akt_Tiket zebraconTicket = new Akt_Tiket
                {
                    DatumKreiran = tiketVM.datumKreiran,
                    IdKorisnikInicijator = idInicijator,
                    Naslov = tiketVM.naslov,
                    OpisProblema = tiketVM.opis,
                    PoslatTiket = true,
                    RadnikKreiran = 1, //Uvek Zebracon Solutions kod njih
                    Status = 1,
                    Tiket = GetClientTicketDocNo()
                };

                _context.Akt_Tiket.Add(zebraconTicket);
                _context.SaveChanges();
                string result = zebraconTicket.Tiket + "/" + zebraconTicket.Id;

                return result;
            }
            catch(NullReferenceException)
            {
                return "Ne postoji takav inicijator.";
            }
            catch (SqlNullValueException)
            {
                return "Naslov/opis u bazi ne može biti prazan.";
            }
            catch (SqlException)
            {
                return "Nije uspostavljena veza sa bazom.";
            }
            catch (DbUpdateException)
            {
                return "Tiket nije kreiran u bazi klijenta. Servisu nisu prosledjeni ispravni podaci.";
            }
            catch (Exception)
            {
                return "Došlo je do greške.";
            }
        }

        private string GetClientTicketDocNo()
        {
            int y = DateTime.Now.Year % 100;

            int currYearTickets = _context.Akt_Tiket.Where(t => t.Tiket.StartsWith("T" + y)).ToList().Count();

            if (_context.Akt_Tiket.Count() == 0 || currYearTickets == 0)
            {
                return "T" + y + "-" + "00001";
            }
            else
            {
                string lastTDno = _context.Akt_Tiket.ToList().Last().Tiket;
                string firstPart = lastTDno.Remove(4);

                string secondPart = lastTDno.Remove(0, 4);
                string secondPartA = ""; 
                for (int i = 0; i < secondPart.Length - 1; i++)
                {
                    if (secondPart[i] == '0')
                    {
                        secondPartA += "0";
                    }
                    else
                    {
                        break;
                    }
                }
                string secondPartB = secondPart.Remove(0, secondPartA.Length);
                int num = int.Parse(secondPartB);
                ++num;
                string nextNumS = num.ToString();
                string secondPartNew = "00000".Remove(5 - nextNumS.Length) + nextNumS;

                return firstPart + secondPartNew;
            }
        }
    }
}
