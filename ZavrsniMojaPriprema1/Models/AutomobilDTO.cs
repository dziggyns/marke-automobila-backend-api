using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavrsniMojaPriprema1.Models
{
    public class AutomobilDTO
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public decimal Cena { get; set; }

        public int GodinaProizvodnje { get; set; }

        public int KonjskihSnaga { get; set; }

        public string MarkaNaziv { get; set; }

        public string MarkaDrzava { get; set; }

        public int MarkaId { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj is AutomobilDTO)
            {
                var that = obj as AutomobilDTO;
                return this.Id == that.Id && this.Model == that.Model;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Id.GetHashCode() ^ Model.GetHashCode();
        }
    }
}