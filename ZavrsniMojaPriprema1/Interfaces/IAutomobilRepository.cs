using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZavrsniMojaPriprema1.Models;

namespace ZavrsniMojaPriprema1.Interfaces
{
    public interface IAutomobilRepository
    {
        IQueryable<Automobil> GetAll();
        Automobil GetById(int id);
        void Add(Automobil automobil);
        void Update(Automobil automobil);
        void Delete(Automobil automobil);
        IQueryable<Automobil> PretragaPoCeni(decimal minCena, decimal maxCena);
        IQueryable<Automobil> PrikaziPoSnazi(int snaga);
        // ako nam treba listanje automobila po marki
        IQueryable<Automobil> PrikaziAutomobilePoIdKategorije(int id);
    }
}
