using System.ComponentModel.DataAnnotations;

namespace BibliotecaCrud.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Correo es obligatorio")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
        public string Password { get; set; }
        [Required(ErrorMessage = "El campo Confirmar Contraseña es obligatorio")]
        public string ConfirmPassword { get; set; }

    }
}
