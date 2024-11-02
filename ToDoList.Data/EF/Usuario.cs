using System;
using System.Collections.Generic;

namespace ToDoList.Data.EF;

public partial class Usuario
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Tablero> Tableros { get; set; } = new List<Tablero>();

    public static implicit operator Usuario(int v)
    {
        throw new NotImplementedException();
    }
}
