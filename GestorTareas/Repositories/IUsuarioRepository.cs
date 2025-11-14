using GestorTareas.Models;

namespace GestorTareas.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> ObtenerTodosAsync();
     
    }
}
