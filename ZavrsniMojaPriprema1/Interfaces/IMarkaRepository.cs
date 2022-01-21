using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZavrsniMojaPriprema1.Models;

namespace ZavrsniMojaPriprema1.Interfaces
{
    public interface IMarkaRepository
    {
        IQueryable<Marka> GetAll();
        Marka GetById(int id);
        void Add(Marka marka);
        void Update(Marka marka);
        void Delete(Marka marka);

    }
}
