using System;
using System.Collections.Generic;

namespace GestorTareas.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<Tarea> Tarea { get; set; } = new List<Tarea>();
}
