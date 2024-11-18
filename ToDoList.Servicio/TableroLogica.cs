using Microsoft.EntityFrameworkCore;
using System.Threading;
using ToDoList.Data.EF;

namespace ToDoList.Servicio
{
    public class TableroLogica : ITableroLogica
    {

	    AzureListDbContext _context;
        public TableroLogica(AzureListDbContext context)
        {

            _context = context;

        }
        public async Task<List<Tablero>> ObtenerTodosAsync()
        {
            return await _context.Tableros.ToListAsync();

        }

        public async Task<Tablero> AgregarAsync(Tablero tablero)
        {
            _context.Tableros.Add(tablero);
            await _context.SaveChangesAsync();
            return tablero;
        }

    }

    public interface ITableroLogica
    {
        Task<List<Tablero>> ObtenerTodosAsync();
        Task<Tablero> AgregarAsync(Tablero tablero);
    }
}