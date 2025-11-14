using GestorTareas.Models;
using GestorTareas.Repositories;
namespace GestorTareas.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;

        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Usuario>> ListarAsync()
        {
            return await _repo.ObtenerTodosAsync();
        }

    
    }
}
