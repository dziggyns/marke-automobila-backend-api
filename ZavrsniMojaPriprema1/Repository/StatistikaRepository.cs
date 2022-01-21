using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZavrsniMojaPriprema1.Interfaces;
using ZavrsniMojaPriprema1.Models;

namespace ZavrsniMojaPriprema1.Repository
{
    public class StatistikaRepository : IStatistikaRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IQueryable<Automobil> PrikaziNajslabije()
        {
            return db.Automobili.OrderByDescending(o => o.KonjskihSnaga).Take(3);
        }


        public IEnumerable<MarkeProsekDTO> PrikaziMarkeProsek()
        {
            return db.Automobili.GroupBy(a => a.Marka.Naziv)
                                .Select(b => new MarkeProsekDTO 
                                {
                                    MarkaAutomobila = b.Key,
                                    ProsecnaCena = b.Average(x => x.Cena),
                                    ProsecnoKonja = b.Average(x => x.KonjskihSnaga)
                                });
        }


        public IEnumerable<StatistikaDTO> PrikaziStatistiku()
        {
            return db.Automobili.GroupBy(a => a.Marka.Naziv)
                                .Select(b => new StatistikaDTO 
                                { 
                                    MarkaAutomobila = b.Key, 
                                    BrojAutomobila = b.Where(c => c.Marka.Naziv == b.Key).Count()
                                    //BrojAutomobila = o.Select(c => c.MarkaId).Count() isto
                                })
                                .OrderByDescending(x => x.BrojAutomobila);
        }


        public IEnumerable<TopDrzaveDTO> PrikaziTopDrzave()
        {
            return db.Automobili.Where(x => x.Cena >= 50000)
                                .GroupBy(a => a.Marka.Drzava)
                                .Select(c => new TopDrzaveDTO 
                                {
                                    Drzava = c.Key,
                                    ProsecnaCenaAutomobila = c.Average(x => x.Cena),
                                    BrojAutomobila = c.Where(x => x.Marka.Drzava == c.Key).Count(),
                                    //BrojAutomobila = a.Select(c => c.MarkaId).Count(), isto
                                    BrojProizvodjaca = c.Select(x => x.MarkaId).Distinct().Count()
                                })
                                .Where(x => x.BrojProizvodjaca >= 2)
                                .OrderByDescending(x => x.ProsecnaCenaAutomobila);
        }
    }
}