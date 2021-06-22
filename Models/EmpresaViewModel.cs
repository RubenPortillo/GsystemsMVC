using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GSystemsMVC.Models
{
    public class EmpresaViewModel
    {
        public string ID { get; set; }

        [Display(Name = "Nombre")]
        [Required, MaxLength(250)]
        public string Nombre { get; set; }

        [Display(Name = "Direccion")]
        [Required, MaxLength(500)]
        public string Direccion { get; set; }
        [Display(Name = "Activo")]
        [Required]
        public bool Activo { get; set; }

        [Display(Name = "Fecha Contratación")]
        [Required]
        public DateTime FActivo { get; set; }
    }
}
