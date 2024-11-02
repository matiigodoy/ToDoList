using Microsoft.AspNetCore.Mvc;
using ToDoList.Servicio;
using ToDoList.Data.EF;

namespace ToDoList.Web.Controllers
{
    public class TareaController : Controller
    {
        private IEstadosLogica _estadosLogica;
        private ITareaLogica _tareaLogica;
        private ITableroLogica _tableroLogica;
        public TareaController(IEstadosLogica estadosLogica, ITareaLogica tareaLogica, ITableroLogica tableroLogica)
        {
            _estadosLogica = estadosLogica;
            _tareaLogica = tareaLogica;
            _tableroLogica = tableroLogica;
        }

        public async Task<IActionResult> AgregarTarea()
        {
            ViewBag.Tableros = await _tableroLogica.ObtenerTodosAsync();
            ViewBag.Estados = await _estadosLogica.ObtenerTodosAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarTarea(Tarea tarea)
        {
            if (ModelState.IsValid && tarea.IdEstado > 0 && tarea.IdTablero > 0)
            {
                await _tareaLogica.AgregarAsync(tarea);
                return RedirectToAction("ListaTareas");
            }
            ViewBag.Tableros = await _tableroLogica.ObtenerTodosAsync();
            ViewBag.Estados = await _estadosLogica.ObtenerTodosAsync();
            return View(tarea);
        }


        public async Task<IActionResult> ListaTareas(int? idEstado, int? idTablero)
        {
            ViewBag.Tableros = await _tableroLogica.ObtenerTodosAsync();
            ViewBag.Estados = await _estadosLogica.ObtenerTodosAsync();
            ViewBag.IdTableroSeleccionado = idTablero ?? 0;
            ViewBag.IdEstadoSeleccionado = idEstado ?? 0;

            var tareas = await _tareaLogica.ObtenerTareasAsync(idEstado, idTablero);



            /*
                        if (idEstado.HasValue && idEstado.Value != 0 && idTablero.HasValue && idTablero.Value != 0)
                        {
                            tareas = await _tareaLogica.ObtenerPorEstadoYTableroAsync(idEstado.Value, idTablero.Value);
                        }
                        else if (idEstado.HasValue && idEstado.Value != 0)
                        {
                            tareas = await _tareaLogica.ObtenerPorEstadoAsync(idEstado.Value);
                        }
                        else if (idTablero.HasValue && idTablero.Value != 0)
                        {
                            tareas = await _tareaLogica.ObtenerPorTableroAsync(idTablero.Value);
                        }
                        else
                        {
                            tareas = await _tareaLogica.ObtenerTodasAsync();
                        }*/

            return View(tareas);
        }


        public async Task<IActionResult> EliminarTarea(int idTarea, int? idEstado, int? idUniverso)
    {
        await _tareaLogica.EliminarAsync(idTarea);

        return RedirectToAction("ListaTareas", new { idEstado = idEstado , idUniverso = idUniverso });
    }


        public async Task<IActionResult> CompleatarTarea(int idTarea, int? idEstado, int? idUniverso)
        {
            await _tareaLogica.CompletarAsync(idTarea);

            return RedirectToAction("ListaTareas", new { idEstado = idEstado, idUniverso = idUniverso });
        }



    }

}
