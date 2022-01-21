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
    public class MarkaRepository : IMarkaRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IQueryable<Marka> GetAll()
        {
            return db.Marke;
        }

        public Marka GetById(int id)
        {
            return db.Marke.FirstOrDefault(k => k.Id == id);
        }


        public void Add(Marka marka)
        {
            try
            {
                db.Marke.Add(marka);
                db.SaveChanges();
            }
            catch (SqlException e)
            {
                if (e.Number == 2601) // Cannot insert duplicate key row in object error
                {
                    throw;
                }
                else
                {
                    throw;
                }
                
            }
        }


        public void Update(Marka marka)
        {
            db.Entry(marka).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }


        public void Delete(Marka marka)
        {
            db.Marke.Remove(marka);
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
    }
}