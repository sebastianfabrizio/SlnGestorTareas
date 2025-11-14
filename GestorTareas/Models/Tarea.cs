using System;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Models
{
    public class Tarea
    {
        public int Id { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(200)]
        public string Titulo { get; set; } = null!;

        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Display(Name = "Fecha de creación")]
        [DataType(DataType.DateTime)]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Fecha de vencimiento")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La fecha de vencimiento es obligatorio.")]
        public DateTime? FechaVencimiento { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Pendiente";

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Debe seleccionar un usuario.")]
        public int? UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
