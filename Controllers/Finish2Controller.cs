using Microsoft.AspNetCore.Mvc;
using ClientTicketAPI.Repository;
using ClientTicketAPI.CustomModels;
using ClientTicketAPI.Models;
using System;

namespace ClientTicketAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Finish2Controller : ControllerBase
    {
        private readonly ITicketRepository microserviceForDAL;
        public Finish2Controller(ITicketRepository msToDAL)
        {
            microserviceForDAL = msToDAL;
        }

        [HttpPut]
        public string Finish2(TiketVM tiketVM)
        {
            //int id, int korisnikZavrsen, DateTime datumZavrsen, int utroseno, string opisResenja, int? vremeFakturisanja, int idVrstaProblema
            //Zavrsavanje tiketa:
            //1. Nadji tiket sa datim id-jem i statusom 2
            //2. Promeni status tiketa u 3, unesi id korisnika koji je zavrsio i kada je zavrsio, koliko je vremena utroseno, opis resenja
            //3. Ako tiket nije pronadjen ili je vec zavrsen, vrati odgovarajucu poruku. Inace, vrati poruku da je uspesno zavrsen.
            try
            {
                Akt_Tiket startedTicket = microserviceForDAL.GetStartedTiket(tiketVM.id);

                startedTicket.KorisnikZavrsen = 1;
                startedTicket.DatumZavrsen = tiketVM.datumZavrsen;
                startedTicket.Utroseno = tiketVM.utroseno;
                startedTicket.OpisResenja = tiketVM.opisResenja;
                //servis mora da unese neku vrednost za INT polje
                startedTicket.VremeFakturisanja = tiketVM.vremeFakturisanja;
                startedTicket.IdVrstaProblema = tiketVM.idVrstaProblema;
                startedTicket.Status = 3;

                microserviceForDAL.SaveToDB(startedTicket);

                return "Tiket je uspešno završen.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
