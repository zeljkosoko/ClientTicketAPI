using System;
using Microsoft.AspNetCore.Mvc;
using ClientTicketAPI.Repository;
using ClientTicketAPI.CustomModels;
using ClientTicketAPI.Models;

namespace ClientTicketAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Bill2Controller : ControllerBase
    {
        private readonly ITicketRepository microserviceForDAL;
        public Bill2Controller(ITicketRepository msForDAL)
        {
            microserviceForDAL = msForDAL;
        }

        [HttpPut]
        public string Bill2(TiketVM tiketVM)
        {
            //int id, int vremeFakturisanja
            //Fakturisanje tiketa:
            //1. Nadji tiket sa datim id-jem i statusom 3 i nepopunjenim vremenom fakturisanja
            //2. Unesi vremeFakturisanja
            //3. Ako tiket nije pronadjen ili je vec fakturisan, vrati odgovarajucu poruku. Inace, vrati poruku da je uspesno fakturisan.
            try
            {
                Akt_Tiket finishedTicket = microserviceForDAL.GetFinishedTiket(tiketVM.id);

                finishedTicket.VremeFakturisanja = tiketVM.vremeFakturisanja;
                microserviceForDAL.SaveToDB(finishedTicket);

                return "Tiket je uspešno fakturisan.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
