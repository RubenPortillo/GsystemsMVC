using GSystemsMVC.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GSystemsMVC.Models
{
    public class IncidenciaViewModel
    {
        public string ID { get; set; }

        [Display(Name = "Descipción de la incidencia")]
        [Required, MaxLength(500)]
        public string IncidenciaDesc { get; set; }

        [Display(Name = "Ubicación")]
        [Required]
        public string Ubicacion { get; set; }

        [Required]
        public DateTime FIncidencia { get; set; }

        [Display(Name = "Prioridad")]
        [Required]
        public Prioridad Prioridad { get; set; }

        public List<IncidenciaViewModel> Incidencias { get; set; }
    }
}
