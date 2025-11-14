using GestorTareas.Models;
using GestorTareas.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestorTareas.Pages.Tareas
{
    public class EditModel : PageModel
    {
        private readonly ITareaService _tareaService;
        private readonly IUsuarioService _usuarioService;

        public EditModel(ITareaService tareaService, IUsuarioService usuarioService)
        {
            _tareaService = tareaService;
            _usuarioService = usuarioService;
        }

        [BindProperty]
        public Tarea Tarea { get; set; } = new();

        public IEnumerable<SelectListItem> UsuariosItems { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGet(int id)
        {
            var tareaDb = await _tareaService.ObtenerAsync(id);

            if (tareaDb == null)
                return RedirectToPage("Index");

            Tarea = tareaDb;
            await CargarUsuariosAsync();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                await CargarUsuariosAsync();
                return Page();
            }

            try
            {
                await _tareaService.ActualizarAsync(Tarea);
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al actualizar: {ex.Message}");
                await CargarUsuariosAsync();
                return Page();
            }
        }

        private async Task CargarUsuariosAsync()
        {
            UsuariosItems = (await _usuarioService.ListarAsync())
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Nombre
                });
        }
    }
}
