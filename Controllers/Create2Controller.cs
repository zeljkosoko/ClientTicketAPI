using Microsoft.AspNetCore.Mvc;
using System;
using ClientTicketAPI.Repository;
using ClientTicketAPI.CustomModels;
using ClientTicketAPI.Models;

namespace ClientTicketAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Create2Controller : ControllerBase
    {
        private readonly ITicketRepository microserviceForDAL; //Repository microservis u api servisu komunicira sa dbContextom

        public Create2Controller(ITicketRepository _iRepository)
        {
            microserviceForDAL = _iRepository;
        }

        [HttpPost]
        public string Create2(TiketVM tiketVM)
        {
            //Servis ocekuje da dobije samo: string inicijator, string naslov, string opis - inace se ne odaziva
            //inicijator moze imati razlicit id kod nas i kod klijenta jer ima vise klijenata-zato prvo trazimo id od klijenta u bazi pa ga onda saljemo da je on zahtevao
            try
            {
                int idInicijator = microserviceForDAL.GetIdKorisnik(tiketVM.inicijatorIme, tiketVM.inicijatorPrezime);

                Akt_Tiket zebraconTicket = new Akt_Tiket
                {
                    DatumKreiran = tiketVM.datumKreiran,
                    IdKorisnikInicijator = idInicijator,
                    Naslov = tiketVM.naslov,
                    OpisProblema = tiketVM.opis,
                    PoslatTiket = true,
                    RadnikKreiran = 1, //Uvek Zebracon Solutions kod njih
                    Status = 1,
                    Tiket = microserviceForDAL.GetClientTicketDocNo()
                };

                microserviceForDAL.SaveToDB(zebraconTicket);

                string result = zebraconTicket.Tiket + "/" + zebraconTicket.Id;
                return result;
            }
            catch (Exception ex) //re-throw od Repository exception
            {
                return ex.Message;
            }
        }
    }
}
