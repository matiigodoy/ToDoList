﻿using Microsoft.EntityFrameworkCore;
using System.Threading;
using ToDoList.Data.EF;

namespace ToDoList.Servicio
{
    public class PrioridadLogica : IPrioridadLogica
    {

	    AzureListDbContext _context;
        public PrioridadLogica(AzureListDbContext context)
        {

            _context = context;

        }
        public async Task<List<Prioridad>> ObtenerTodosAsync()
        {
            return await _context.Prioridads.ToListAsync();

        }

    }

    public interface IPrioridadLogica
    {
        Task<List<Prioridad>> ObtenerTodosAsync();
    }
}