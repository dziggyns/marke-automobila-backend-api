using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZavrsniMojaPriprema1.Models
{
    public class Marka
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Naziv { get; set; }

        [Required]
        [StringLength(2)]
        [RegularExpression(@"^[a-zA-Z]{2}$")]
        public string Drzava { get; set; }

        // Navigation property
        public virtual ICollection<Automobil> Automobili { get; set; }
    }
}