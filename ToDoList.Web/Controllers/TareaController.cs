using Microsoft.AspNetCore.Mvc;
using ToDoList.Servicio;
using ToDoList.Data.EF;

namespace ToDoList.Web.Controllers
{
    public class TareaController : Controller
    {
        private IPrioridadLogica _prioridadLogica;
        private ITareaLogica _tareaLogica;
        private ITableroLogica _tableroLogica;


        public TareaController(IPrioridadLogica prioridadLogica, ITareaLogica tareaLogica, ITableroLogica tableroLogica)
        {
            _prioridadLogica = prioridadLogica;
            _tareaLogica = tareaLogica;
            _tableroLogica = tableroLogica;
        }

        public async Task<IActionResult> AgregarTarea(int idTablero)
        {
	        ViewBag.TableroId = idTablero;
            ViewBag.Prioridades = await _prioridadLogica.ObtenerTodosAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarTarea(Tarea tarea, int idTablero)
        {
            if (ModelState.IsValid && tarea.IdPrioridad > 0)
            {
	            tarea.IdTablero = idTablero;
                await _tareaLogica.AgregarAsync(tarea);
                return RedirectToAction("ListaTareas" , new { idTablero });
            }
            ViewBag.Prioridades = await _prioridadLogica.ObtenerTodosAsync();
            return View(tarea);
        }


        public async Task<IActionResult> ListaTareas(int idTablero, int? idPrioridad = null)
        {
            ViewBag.Prioridades = await _prioridadLogica.ObtenerTodosAsync();
            
            var tareas = await _tareaLogica.ObtenerTareasAsync(idTablero);

            if (idPrioridad.HasValue && idPrioridad != 0)
            {
                tareas = tareas.Where(t => t.IdPrioridad == idPrioridad).ToList();
			}

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
            ViewBag.TableroId = idTablero;
            ViewBag.PrioridadSeleccionada = idPrioridad;
			return View(tareas);
        }


        public async Task<IActionResult> EliminarTarea(int idTarea, int? idPrioridad, int? idTablero)
    {
        await _tareaLogica.EliminarAsync(idTarea);

        return RedirectToAction("ListaTareas", new { idPrioridad = idPrioridad, idTablero = idTablero });
    }


        public async Task<IActionResult> CompleatarTarea(int idTarea, int? idPrioridad, int? idTablero)
        {
            await _tareaLogica.CompletarAsync(idTarea);

            return RedirectToAction("ListaTareas", new { idPrioridad = idPrioridad, idTablero = idTablero });
        }



    }

}
