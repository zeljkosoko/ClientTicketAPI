using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientTicketAPI.Models;
using ClientTicketAPI.CustomModels;

namespace ClientTicketAPI.Repository
{
    //Ovde se enkapsulirane sve metode za DAL, koje kontroleri zahtevaju od dbContexta
    public interface ITicketRepository
    {
        int GetIdKorisnik(string ime, string prezime);
        void InsertToDB(Akt_Tiket noviTiket);
        Akt_Tiket GetCreatedTiket(object id);
        Akt_Tiket GetStartedTiket(object id);
        Akt_Tiket GetFinishedTiket(object id);
        public string GetClientTicketDocNo();
        void SaveToDB();
    }
}
