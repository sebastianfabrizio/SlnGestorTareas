using GestorTareas.Models;
using GestorTareas.Repositories;

namespace GestorTareas.Services
{
    public class TareaService : ITareaService
    {
        private readonly ITareaRepository _repo;

        public TareaService(ITareaRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Tarea>> ListarAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Tarea> ObtenerAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task CrearAsync(Tarea tarea)
        {
            tarea.Estado = "Pendiente";
            tarea.FechaCreacion = DateTime.Now;
            await _repo.CreateAsync(tarea);
        }

        public async Task ActualizarAsync(Tarea tarea)
        {
            await _repo.UpdateAsync(tarea);
        }

        public async Task EliminarAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task CambiarEstadoAsync(int id, string nuevoEstado)
        {
            var tarea = await _repo.GetByIdAsync(id);
            if (tarea == null) return;
            var permitidos = new[] { "Pendiente", "En progreso", "Completada" };
            if (!permitidos.Contains(nuevoEstado))
                throw new Exception("Estado inválido.");

            tarea.Estado = nuevoEstado;

            await _repo.UpdateAsync(tarea);
        }
    }
}
