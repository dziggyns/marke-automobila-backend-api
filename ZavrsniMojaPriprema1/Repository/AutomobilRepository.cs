using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ZavrsniMojaPriprema1.Interfaces;
using ZavrsniMojaPriprema1.Models;

namespace ZavrsniMojaPriprema1.Repository
{
    public class AutomobilRepository : IAutomobilRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IQueryable<Automobil> GetAll()
        {
            return db.Automobili;
        }


        public Automobil GetById(int id)
        {
            return db.Automobili.FirstOrDefault(p => p.Id == id);
        }


        public void Add(Automobil automobil)
        {
            try 
            {
                db.Automobili.Add(automobil);
                db.SaveChanges();
            }
            catch (SqlException e)
            {
                throw e;
            }
        }


        public void Update(Automobil automobil)
        {
            db.Entry(automobil).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }


        public void Delete(Automobil automobil)
        {
            db.Automobili.Remove(automobil);
            db.SaveChanges();
        }


        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public IQueryable<Automobil> PretragaPoCeni(decimal minCena, decimal maxCena)
        {
            return db.Automobili.Where(a => a.Cena >= minCena && a.Cena <= maxCena).OrderBy(c => c.Cena);
        }


        public IQueryable<Automobil> PrikaziPoSnazi(int snaga)
        {
            return db.Automobili.Where(a => a.KonjskihSnaga <= snaga).OrderByDescending(c => c.KonjskihSnaga);
        }


        public IQueryable<Automobil> PrikaziAutomobilePoIdKategorije(int id)
        {
            return db.Automobili.Where(a => a.MarkaId == id).OrderBy(x => x.Marka.Naziv);
        }
    }
}