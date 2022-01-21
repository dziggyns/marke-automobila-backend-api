using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZavrsniMojaPriprema1.Models
{
    public class PretragaDTO
    {
        [Required]
        [Range(0.0, double.MaxValue)]
        [RegularExpression(@"^[0-9]+([.,][0-9]{1,3})?$")]
        public decimal minCena { get; set; }

        [Required]
        [Range(0.0, double.MaxValue)]
        [RegularExpression(@"^[0-9]+([.,][0-9]{1,3})?$")]
        public decimal maxCena { get; set; }
    }
}