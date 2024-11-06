using System;
using System.Collections.Generic;

namespace CapaEntidades;

public partial class Personal
{
    public int IdPersonal { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Dni { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public int IdCargo { get; set; }

    public DateTime Fyhcreacion { get; set; }

    public byte EstadoRegistro { get; set; }

    public bool RegistroEliminado { get; set; }

    public virtual Cargo IdCargoNavigation { get; set; } = null!;
}
