using System;
using Microsoft.AspNetCore.Mvc;
using ClientTicketAPI.Repository;
using ClientTicketAPI.CustomModels;
using ClientTicketAPI.Models;

namespace ClientTicketAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Start2Controller : ControllerBase
    {
        private readonly ITicketRepository microserviceForDAL;
        public Start2Controller(ITicketRepository repoService)
        {
            microserviceForDAL = repoService;
        }

        [HttpPut]
        public string Start2(TiketVM tiketVM)
        {
            //int id, int korisnikZapocet, DateTime datumZapocet
            //Zapocinjanje tiketa:
            //1. Nadji tiket sa datim id-jem i statusom 1
            //2. Promeni status tiketa u 2, unesi id korisnika koji je zapoceo, datum zapocinjanja
            //3. Ako tiket nije pronadjen ili je vec zapocet vrati odgovarajucu poruku. Inace, vrati poruku da je zapocet.
            try
            {
                Akt_Tiket createdTicket = microserviceForDAL.GetCreatedTiket(tiketVM.id);

                createdTicket.KorisnikZapocet = 1; //Uvek Zebracon Solutions kod njih
                createdTicket.DatumZapocet = tiketVM.datumZapocet;
                createdTicket.Status = 2;

                microserviceForDAL.SaveToDB(createdTicket);

                return "Tiket je uspešno započet.";
            }
           
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
