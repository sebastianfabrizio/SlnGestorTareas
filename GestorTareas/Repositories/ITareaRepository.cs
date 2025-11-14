using GestorTareas.Models;

namespace GestorTareas.Repositories
{
    public interface ITareaRepository
    {
        Task<Tarea> GetByIdAsync(int id);
        Task<List<Tarea>> GetAllAsync();
        Task CreateAsync(Tarea tarea);
        Task UpdateAsync(Tarea tarea);
        Task DeleteAsync(int id);
    }
}
