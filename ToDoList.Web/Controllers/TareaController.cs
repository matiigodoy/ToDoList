using Microsoft.AspNetCore.Mvc;
using ToDoList.Servicio;
using ToDoList.Data.EF;

namespace ToDoList.Web.Controllers
{
    public class TareaController : Controller
    {
        private IEstadosLogica _estadosLogica;
    private ITareaLogica _tareaLogica;
    public TareaController(IEstadosLogica _estadosLogica, ITareaLogica _tareaLogica)
    {
            _estadosLogica = _estadosLogica;
            _tareaLogica = _tareaLogica;
    }

    public async Task<IActionResult> AgregarTarea()
    {
        ViewBag.Estados = await _estadosLogica.ObtenerTodosAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AgregarTarea(Tarea tarea)
    {
        if (ModelState.IsValid && tarea.IdEstado > 0)
        {
            await _tareaLogica.AgregarAsync(tarea);
            return RedirectToAction("ListaTareas");
        }

            ViewBag.Estados = await _estadosLogica.ObtenerTodosAsync();
            return View(tarea);
    }


    public async Task<IActionResult> ListaTareas(int? idEstado)
    {
            ViewBag.Estados = await _estadosLogica.ObtenerTodosAsync();
            ViewBag.IdEstadoSeleccionado = idEstado ?? 0;

        var superheroe = idEstado.HasValue && idEstado.Value != 0 ? await _tareaLogica.ObtenerPorEstadoAsync(idEstado.Value) : await _tareaLogica.ObtenerTodasAsync();



        return View(superheroe);

    }

    public async Task<IActionResult> EliminarTarea(int idTarea, int? idEstado)
    {
        await _tareaLogica.EliminarAsync(idTarea);

        return RedirectToAction("ListaTareas", new { idEstado = idEstado });
    }


        public async Task<IActionResult> CompleatarTarea(int idTarea, int? idEstado)
        {
            await _tareaLogica.CompletarAsync(idTarea);

            return RedirectToAction("ListaTareas", new { idEstado = idEstado });
        }



    }

}
