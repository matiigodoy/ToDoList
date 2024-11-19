using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Data.EF;
using ToDoList.Servicio;
using ToDoList.Web.Models;

namespace ToDoList.Web.Controllers
{
    public class TableroController : Controller
    {
        private readonly ILogger<TableroController> _logger;
        private ITableroLogica _tableroLogica;

        public TableroController(ITableroLogica tableroLogica)
        {
            _tableroLogica = tableroLogica;
        }

        public async Task<IActionResult> MisTableros()
        {
            var tableros = await _tableroLogica.ObtenerTodosAsync() ?? new List<Tablero>();
            return View(tableros);
        }

        public async Task<IActionResult> AgregarTablero()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarTablero(Tablero tablero)
        {
            await _tableroLogica.AgregarAsync(tablero);
            return RedirectToAction("MisTableros");
        }

        public async Task<IActionResult> EliminarTablero(int idTablero)
        {
            await _tableroLogica.EliminarAsync(idTablero);

            return RedirectToAction("MisTableros");
        }
    }
}
