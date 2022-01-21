using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavrsniMojaPriprema1.Models
{
    public class TopDrzaveDTO
    {
        public string Drzava { get; set; }

        public decimal ProsecnaCenaAutomobila { get; set; }

        public int BrojAutomobila { get; set; }

        public int BrojProizvodjaca { get; set; }
    }
}