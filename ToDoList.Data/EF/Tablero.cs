﻿using System;
using System.Collections.Generic;

namespace ToDoList.Data.EF;

public partial class Tablero
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
