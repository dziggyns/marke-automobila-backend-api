using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZavrsniMojaPriprema1.Models;

namespace ZavrsniMojaPriprema1.Interfaces
{
    public interface IStatistikaRepository
    {
        IQueryable<Automobil> PrikaziNajslabije();
        IEnumerable<StatistikaDTO> PrikaziStatistiku();
        IEnumerable<MarkeProsekDTO> PrikaziMarkeProsek();
        IEnumerable<TopDrzaveDTO> PrikaziTopDrzave();
    }
}
