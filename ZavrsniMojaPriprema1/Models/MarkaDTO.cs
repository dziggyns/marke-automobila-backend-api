using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavrsniMojaPriprema1.Models
{
    public class MarkaDTO
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public string Drzava { get; set; }


        public override bool Equals(Object obj)
        {
            if (obj is MarkaDTO)
            {
                var that = obj as MarkaDTO;
                return this.Id == that.Id && this.Naziv == that.Naziv && this.Drzava == that.Drzava;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Id.GetHashCode() ^ Naziv.GetHashCode() ^ Drzava.GetHashCode();
        }
    }
}