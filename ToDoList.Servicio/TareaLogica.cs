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


        }

    }


    public interface ITareaLogica
    {
        Task<Tarea> AgregarAsync(Tarea tarea);
        Task<Tarea> CompletarAsync(int idTarea);
        Task<Tarea> EliminarAsync(int idTarea);
        Task<List<Tarea>> ObtenerTodasAsync();
        Task<List<Tarea>> ObtenerPorEstadoAsync(int idestado);
        Task<List<Tarea>> ObtenerPorTableroAsync(int idtablero);
        Task<List<Tarea>> ObtenerPorEstadoYTableroAsync(int idestado, int idtablero);
    }
}
