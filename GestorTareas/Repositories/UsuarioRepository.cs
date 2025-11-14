using GestorTareas.Data;
using GestorTareas.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly TareasContext _context;

        public UsuarioRepository(TareasContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> ObtenerTodosAsync()
        {
            return await _context.Usuario.ToListAsync();
        }

     
    }
}
