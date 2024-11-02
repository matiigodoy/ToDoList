using Microsoft.EntityFrameworkCore;
using System.Threading;
using ToDoList.Data.EF;

namespace ToDoList.Servicio
{
    public class TareaLogica:ITareaLogica
    {

        ToDoListDbContext _context;
        public TareaLogica(ToDoListDbContext context)
        {

            _context = context;

        }
        public async Task<Tarea> AgregarAsync(Tarea tarea)
        {
            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();
            return tarea;
        }

        public async Task<Tarea> CompletarAsync(int idTarea)
        {

            var tarea = await _context.Tareas.FindAsync(idTarea);
            if (tarea == null)
            {
                return null;
            }

            tarea.IdEstado = 1;

            await _context.SaveChangesAsync();
            return tarea;
        }


        public async Task<Tarea> EliminarAsync(int idTarea)
        {

            var tarea = await _context.Tareas.FindAsync(idTarea);
            if (tarea == null)
            {
                return null;
            }

            _context.Tareas.Remove(tarea);

            await _context.SaveChangesAsync();
            return tarea;
        }

        /*
                public async Task<List<Tarea>> ObtenerTodasAsync()
                {
                    return await _context.Tareas.OrderBy(t => t.IdEstadoNavigation.Nombre).ToListAsync();


                }

                public async Task<List<Tarea>> ObtenerPorEstadoAsync(int idestado)
                {
                    return await _context.Tareas.Include(t => t.IdEstadoNavigation.Nombre).Where(t=>t.IdEstadoNavigation.Id == idestado).OrderBy(t => t.Titulo).ToListAsync();


                }
                public async Task<List<Tarea>> ObtenerPorTableroAsync(int idtablero)
                {
                    return await _context.Tareas.Include(t => t.IdTableroNavigation.Nombre).Where(t => t.IdTableroNavigation.Id == idtablero).OrderBy(t => t.Titulo).ToListAsync();


                }

                public async Task<List<Tarea>> ObtenerPorEstadoYTableroAsync(int idestado, int idtablero)
                {
                    return await _context.Tareas.Include(t => t.IdEstadoNavigation.Nombre).Include(t=>t.IdTableroNavigation.Nombre).Where(t => t.IdEstadoNavigation.Id == idestado && t.IdTableroNavigation.Id == idtablero).OrderBy(t => t.Titulo).ToListAsync();


                }*/



        public async Task<List<Tarea>> ObtenerTareasAsync(int? idEstado = null, int? idTablero = null)
        {
            // Empieza la consulta base sin filtros.
            var consulta = _context.Tareas.AsQueryable();

            // Aplica el filtro de idEstado solo si tiene valor y es distinto de 0.
            if (idEstado.HasValue && idEstado.Value != 0)
            {
                consulta = consulta.Where(t => t.IdEstadoNavigation.Id == idEstado.Value);
            }

            // Aplica el filtro de idTablero solo si tiene valor y es distinto de 0.
            if (idTablero.HasValue && idTablero.Value != 0)
            {
                consulta = consulta.Where(t => t.IdTableroNavigation.Id == idTablero.Value);
            }

            // Incluye las relaciones necesarias y ordena los resultados por Titulo.
            consulta = consulta
                .Include(t => t.IdEstadoNavigation)
                .Include(t => t.IdTableroNavigation)
                .OrderBy(t => t.Titulo);

            // Ejecuta la consulta y devuelve la lista.
            return await consulta.ToListAsync();
        }

    }


    public interface ITareaLogica
    {
        Task<Tarea> AgregarAsync(Tarea tarea);
        Task<Tarea> CompletarAsync(int idTarea);
        Task<Tarea> EliminarAsync(int idTarea);

        /*
        Task<List<Tarea>> ObtenerTodasAsync();
        Task<List<Tarea>> ObtenerPorEstadoAsync(int idestado);
        Task<List<Tarea>> ObtenerPorTableroAsync(int idtablero);
        Task<List<Tarea>> ObtenerPorEstadoYTableroAsync(int idestado, int idtablero);
        */

        Task<List<Tarea>> ObtenerTareasAsync(int? idEstado = null, int? idTablero = null);
    }
}
