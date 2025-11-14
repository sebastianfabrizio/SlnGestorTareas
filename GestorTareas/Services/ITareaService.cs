using GestorTareas.Models;

namespace GestorTareas.Services
{
    public interface ITareaService
    {
        Task<List<Tarea>> ListarAsync();
        Task<Tarea> ObtenerAsync(int id);
        Task CrearAsync(Tarea tarea);
        Task ActualizarAsync(Tarea tarea);
        Task EliminarAsync(int id);
        Task CambiarEstadoAsync(int id, string nuevoEstado);
    }
}
