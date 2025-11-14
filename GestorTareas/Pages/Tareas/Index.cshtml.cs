using GestorTareas.Models;
using GestorTareas.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorTareas.Pages.Tareas
{
    public class IndexModel : PageModel
    {
        private readonly ITareaService _service;

        public IndexModel(ITareaService service)
        {
            _service = service;
        }

        public List<Tarea> ListaTareas { get; set; } = new();

        public async Task OnGet()
        {
            ListaTareas = await _service.ListarAsync();
        }

        public async Task<IActionResult> OnPostCambiarEstadoAsync([FromForm] EstadoRequest request)
        {
            if (request == null || request.Id == 0 || string.IsNullOrWhiteSpace(request.Estado))
                return new JsonResult(new { success = false, message = "Datos inválidos" });

            try
            {
                await _service.CambiarEstadoAsync(request.Id, request.Estado);
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> OnPostEliminarAsync(int id)
        {
            await _service.EliminarAsync(id);
            return new JsonResult(new { success = true });
        }

    }
}
