using GestorTareas.Models;
using GestorTareas.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestorTareas.Pages.Tareas
{
    public class CreateModel : PageModel
    {
        private readonly ITareaService _tareaService;
        private readonly IUsuarioService _usuarioService;

        public CreateModel(ITareaService tareaService, IUsuarioService usuarioService)
        {
            _tareaService = tareaService;
            _usuarioService = usuarioService;
        }

        [BindProperty]
        public Tarea Tarea { get; set; } = new();

        public IEnumerable<SelectListItem> UsuariosItems { get; set; } = new List<SelectListItem>();

        public async Task OnGet() => await CargarUsuariosAsync();

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                await CargarUsuariosAsync();
                return Page();
            }

            try
            {
                await _tareaService.CrearAsync(Tarea);
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al guardar: {ex.Message}");
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
                })
                .ToList();
        }
    }
}
