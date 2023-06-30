using System;
using System.Collections.Generic;

namespace Api_2W1_CQRS.Models;

public partial class Sexo
{
    public int Id { get; set; }

    public string Sexo1 { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
