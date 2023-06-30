using System;
using System.Collections.Generic;

namespace Api_2W1_CQRS.Models;

public partial class Provincium
{
    public int Id { get; set; }

    public string Provincia { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
