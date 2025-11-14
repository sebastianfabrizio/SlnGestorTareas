using GestorTareas.Data;
using GestorTareas.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Repositories
{
    public class TareaRepository : ITareaRepository
    {
        private readonly TareasContext _context;
        public TareaRepository(TareasContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Tarea tarea)
        {
            await _context.Tarea.AddAsync(tarea);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Tarea.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Tarea>> GetAllAsync()
        {
            return await _context.Tarea
            .Include(x => x.Usuario)
            .ToListAsync();
        }

        public async Task<Tarea> GetByIdAsync(int id)
        {
            return await _context.Tarea.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateAsync(Tarea tarea)
        {
            _context.Tarea.Update(tarea);
            await _context.SaveChangesAsync();
        }
    }
}
