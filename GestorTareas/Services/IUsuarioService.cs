using GestorTareas.Models;

namespace GestorTareas.Services
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> ListarAsync();
       
    }
}
