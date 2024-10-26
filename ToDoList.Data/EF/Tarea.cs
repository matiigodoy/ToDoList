using System;
using System.Collections.Generic;

namespace ToDoList.Data.EF;

public partial class Tarea
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int? IdTablero { get; set; }

    public int? IdEstado { get; set; }

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual Tablero? IdTableroNavigation { get; set; }
}
