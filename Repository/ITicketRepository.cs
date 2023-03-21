using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientTicketAPI.Models;
using ClientTicketAPI.CustomModels;

namespace ClientTicketAPI.Repository
{
    //REPOSITORY Microservice communicates with DAL
    //It encapsulates methods for DAL and controllers no need dbContext
    public interface ITicketRepository
    {
        int GetIdKorisnik(string ime, string prezime);
        Akt_Tiket GetCreatedTiket(object id);
        Akt_Tiket GetStartedTiket(object id);
        Akt_Tiket GetFinishedTiket(object id);
        string GetClientTicketDocNo();
        void SaveToDB(Akt_Tiket noviTiket);
    }
}
