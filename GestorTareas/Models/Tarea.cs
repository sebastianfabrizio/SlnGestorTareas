using System;
using System.Collections.Generic;

namespace GestorTareas.Models;

public partial class Tarea
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public string Estado { get; set; } = null!;

    public int UsuarioId { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
