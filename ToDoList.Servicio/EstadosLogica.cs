using Microsoft.EntityFrameworkCore;
using System.Threading;
using ToDoList.Data.EF;

namespace ToDoList.Servicio
{
    public class EstadosLogica : IEstadosLogica
    {

        ToDoListDbContext _context;
        public EstadosLogica(ToDoListDbContext context)
        {

            _context = context;

        }
        public async Task<List<Estado>> ObtenerTodosAsync()
        {
            return await _context.Estados.ToListAsync();

        }

    }

    public interface IEstadosLogica
    {
        Task<List<Estado>> ObtenerTodosAsync();
    }
}