using System.ComponentModel.DataAnnotations;

namespace BibliotecaCrud.Models
{
    public class LibraryModel
    {
        
        public int IdBook { get; set; }

        [Required(ErrorMessage = "El campo Titulo es obligatorio")]
        public string? Title { get; set; }

        [Required (ErrorMessage = "El campo Categoria es obligatorio")]
        public string? Category { get; set; }

        [Required (ErrorMessage = "El campo Editorial es obligatorio")]
        public string? Author { get; set; }

        [Required (ErrorMessage = "El campo Autor es obligatorio")]
        public string? Editorial { get; set; }

        [Required (ErrorMessage = "El campo Cantidad es obligatorio")]
        public int? Amount { get; set; }

    }
}
