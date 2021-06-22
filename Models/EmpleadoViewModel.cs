using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GSystemsMVC.Models
{
    public class EmpleadoViewModel
    {

        public string ID { get; set; }

        [Display(Name = "Nombre")]
        [Required, MaxLength(50)]
        public string Nombre { get; set; }

        [Display(Name = "Primer Apellido")]
        [Required, MaxLength(50)]
        public string Apellido1 { get; set; }
        [Display(Name = "Segundo Apellido")]
        [Required, MaxLength(50)]
        public string Apellido2 { get; set; }

        [Display(Name = "E-Mail")]
        [Required]
        public string Mail { get; set; }

    }
}
