using Microsoft.EntityFrameworkCore;
using System.Threading;
using ToDoList.Data.EF;

namespace ToDoList.Servicio
{
    public class TableroLogica : ITableroLogica
    {

        ToDoListDbContext _context;
        public TableroLogica(ToDoListDbContext context)
        {

            _context = context;

        }
        public async Task<List<Tablero>> ObtenerTodosAsync()
        {
            return await _context.Tableros.ToListAsync();

        }

    }

    public interface ITableroLogica
    {
        Task<List<Tablero>> ObtenerTodosAsync();
    }
}