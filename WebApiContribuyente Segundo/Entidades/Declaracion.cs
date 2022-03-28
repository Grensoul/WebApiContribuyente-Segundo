using System.ComponentModel.DataAnnotations;
using WebApiContribuyente_Segundo.Validaciones;

namespace WebApiContribuyente_Segundo.Entidades
{
    public class Declaracion
    {

        public int Id { get; set; }
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo puede tener hasta 250 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
    }
}
