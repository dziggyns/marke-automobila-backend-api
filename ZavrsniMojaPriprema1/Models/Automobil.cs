using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZavrsniMojaPriprema1.Models
{
    public class Automobil
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Model { get; set; }

        [Required]
        [Range(0.0, double.MaxValue)]
        public decimal Cena { get; set; }

        [Required]
        [Range(1970, 2020)]
        public int GodinaProizvodnje { get; set; }

        [Required]
        [Range(0, 500)]
        public int KonjskihSnaga { get; set; }

        // Navigation property
        public int MarkaId { get; set; }
        public virtual Marka Marka { get; set; }
    }
}