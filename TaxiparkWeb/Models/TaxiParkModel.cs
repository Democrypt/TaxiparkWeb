using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaxiparkWeb.Models
{
    public class AutoModel
    {
        public int AutoId { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
    }

    public class DriverModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int AutoId { get; set; }

        [NotMapped] public List<AutoModel> AutoModels { get; set; }
    }
}