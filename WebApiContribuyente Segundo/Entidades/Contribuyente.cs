using System.ComponentModel.DataAnnotations;
using WebApiContribuyente_Segundo.Validaciones;

namespace WebApiContribuyente_Segundo.Entidades
{
    public class Contribuyente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El {0} debe de ser ingresado obligatoriamente")]
        [StringLength(15, ErrorMessage = "El {0} debe de tener 15 caracteres como máximo")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
    }
}
