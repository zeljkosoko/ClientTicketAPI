using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientTicketAPI.Models;
using Microsoft.EntityFrameworkCore;
using ClientTicketAPI.CustomModels;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace ClientTicketAPI.Repository
{
    public class TicketRepository : ITicketRepository
    {
        #region Fields
        private readonly TicketDbContext dbContext;
        #endregion

        #region Constructor
        public TicketRepository(TicketDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        #endregion

        #region Methods
        public int GetIdKorisnik(string ime, string prezime)
        {
            try
            {
                Sif_Korisnik korisnik = dbContext.Sif_Korisnik.SingleOrDefault(k => k.Ime + k.Prezime == ime + prezime);
                return korisnik.Id;
            }
            catch(ArgumentNullException)
            {
                //izbacuje gresku koju hvata try block u kome se poziva ova methoda a catch radi re-throw exception. !!!!!!!!!!
                throw new ArgumentNullException("ne postoji takav korisnik"); 
            }
            catch(InvalidOperationException)
            {
                throw new InvalidOperationException("vise korisnika sa istim imenom");
            }
            catch(Exception)
            {
                throw new Exception("Opsta greska- nema korisnika");
            }
        }
        public Akt_Tiket GetCreatedTiket(object id)
        {
            try
            {
                Akt_Tiket createdTiket = dbContext.Akt_Tiket.Where(t => t.Id == (int)id && t.Status == 1).FirstOrDefault();
                return createdTiket;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("tiket nije kreiran.");
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("Ne postoji takav tiket ili je već započet.");
            }
            catch (SqlNullValueException)
            {
                throw new SqlNullValueException( "Korisnik ili datum u bazi ne može biti prazan.");
            }
            catch (Exception)
            {
                throw new Exception("Opsta greska- tiket nije kreiran.");
            }
        }
        public Akt_Tiket GetStartedTiket(object id)
        {
            try
            {
                Akt_Tiket startedTiket = dbContext.Akt_Tiket.Where(t => t.Id == (int)id && t.Status == 2).FirstOrDefault();
                return startedTiket;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("tiket nije zapocet.");
            }
            catch (Exception)
            {
                throw new Exception("Opsta greska- tiket nije zapocet.");
            }
        }
        public Akt_Tiket GetFinishedTiket(object id)
        {
            try
            {
                Akt_Tiket finishedTiket = dbContext.Akt_Tiket.Where(t => t.Id == (int)id && t.Status == 3 && (t.VremeFakturisanja == null || t.VremeFakturisanja == 0)).FirstOrDefault();
                return finishedTiket;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("tiket nije zavrsen.");
            }
            catch (Exception)
            {
                throw new Exception("Opsta greska- tiket nije zavrsen.");
            }
        }
        public string GetClientTicketDocNo()
        {
            try
            {
                int y = DateTime.Now.Year % 100;

                int currYearTickets = dbContext.Akt_Tiket.Where(t => t.Tiket.StartsWith("T" + y)).ToList().Count();

                if (dbContext.Akt_Tiket.Count() == 0 || currYearTickets == 0)
                {
                    return "T" + y + "-" + "00001";
                }
                else
                {
                    string lastTDno = dbContext.Akt_Tiket.ToList().Last().Tiket;
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
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("GRESKA - GetClientTicketDocNo");
            }
            catch (OverflowException)
            {
                throw new ArgumentNullException("GRESKA - GetClientTicketDocNo");
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("GRESKA - GetClientTicketDocNo");
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentNullException("GRESKA - GetClientTicketDocNo");
            }
            catch (Exception)
            {
                throw new Exception("Opsta greska- GetClientTicketDocNo");
            }
        }
        public void SaveToDB(Akt_Tiket noviTiket)
        {
            try
            {
                dbContext.Akt_Tiket.Add(noviTiket);
                dbContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException("Tiket nije sacuvan u bazi, servisu nisu prosledjeni ispravni podaci.");
            }
            catch (Exception)
            {
                throw new Exception("Opsta greska- Tiket nije sacuvan u bazi.");
            }
        }
        #endregion

    }
}
