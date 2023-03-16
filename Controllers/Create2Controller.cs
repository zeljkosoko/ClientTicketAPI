using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;
using ClientTicketAPI.Repository;
using ClientTicketAPI.CustomModels;
using ClientTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientTicketAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Create2Controller : ControllerBase
    {
        private readonly ITicketRepository iRepository; //Repository microservis u api servisu komunicira sa dbContextom

        public Create2Controller(ITicketRepository _iRepository)
        {
            iRepository = _iRepository;
        }
        [HttpPost]
        public string Create2(TiketVM tiketVM)
        {
            //string inicijator, string naslov, string opis
            //ako su podaci pogresnog tipa, servis nece ni da se odazove
            //inicijator moze imati razlicit id kod nas i kod klijenta jer ima vise klijenata-zato prvo trazimo id od klijenta u bazi pa ga onda saljemo da je on zahtevao
            try
            {
                int idInicijator = iRepository.GetIdKorisnik(tiketVM.inicijatorIme, tiketVM.inicijatorPrezime);

                Akt_Tiket zebraconTicket = new Akt_Tiket
                {
                    DatumKreiran = tiketVM.datumKreiran,
                    IdKorisnikInicijator = idInicijator,
                    Naslov = tiketVM.naslov,
                    OpisProblema = tiketVM.opis,
                    PoslatTiket = true,
                    RadnikKreiran = 1, //Uvek Zebracon Solutions kod njih
                    Status = 1,
                    Tiket = iRepository.GetClientTicketDocNo()
                };

                iRepository.InsertToDB(zebraconTicket);

                string result = zebraconTicket.Tiket + "/" + zebraconTicket.Id;

                return result;
            }
           
            catch (Exception ex) //radi re-throw od TicketRepository exception
            {
                return ex.Message;
            }
        }

        
    }
}
