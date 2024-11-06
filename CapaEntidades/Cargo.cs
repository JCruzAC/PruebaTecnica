using System;
using System.Collections.Generic;

namespace CapaEntidades;

public partial class Cargo
{
    public int IdCargo { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime Fyhcreacion { get; set; }

    public byte EstadoRegistro { get; set; }

    public bool RegistroEliminado { get; set; }

    public virtual ICollection<Personal> Personals { get; set; } = new List<Personal>();
}
