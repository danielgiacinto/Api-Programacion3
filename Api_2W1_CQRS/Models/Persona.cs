using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Api_2W1_CQRS.Models;

public partial class Persona
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Dni { get; set; }

    public int IdSexo { get; set; }

    public int IdPais { get; set; }

    public int IdProvincia { get; set; }

    public virtual Pai IdPaisNavigation { get; set; } = null!;

    public virtual Provincium IdProvinciaNavigation { get; set; } = null!;

    public virtual Sexo IdSexoNavigation { get; set; } = null!;
}
