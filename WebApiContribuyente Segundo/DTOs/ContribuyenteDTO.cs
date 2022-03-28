using System.ComponentModel.DataAnnotations;
using WebApiContribuyente_Segundo.Validaciones;

namespace WebApiContribuyente_Segundo.DTOs
{
    public class ContribuyenteDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede tener hasta 150 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
    }
}
