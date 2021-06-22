using GSystemsMVC.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GSystemsMVC.Models
{
    public class TareaViewModel
    {
        public string ID { get; set; }

        [Display(Name = "Descipción de la tarea")]
        [Required, MaxLength(500)]
        public string IncidenciaDesc { get; set; }

        [Display(Name = "Duración")]
        [Required]
        public string Duracion { get; set; }
        [Display(Name = "Prioridad")]
        [Required]
        public Prioridad Prioridad { get; set; }
    }
}
