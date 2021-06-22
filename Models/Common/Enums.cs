using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GSystemsMVC.Models.Common
{
    public enum Perfil
    {
        [Display(Name = "Adminsitrador")]
        Admin = 0,
        [Display(Name = "Empleado")]
        Empleado = 1
    }
    public enum Turno
    {
        [Display(Name = "Mañana")]
        Manhana = 0,
        [Display(Name = "Tarde")]
        Tarde = 0,
        [Display(Name = "Noche")]
        Noche = 3
    }

    public enum Prioridad
    {
        [Display(Name = "Baja")]
        Baja = 0,
        [Display(Name = "Media")]
        Media = 1,
        [Display(Name = "Alta")]
        Alta = 2
    }
}
