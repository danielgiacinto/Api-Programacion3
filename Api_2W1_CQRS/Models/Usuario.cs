using System;
using System.Collections.Generic;

namespace Api_2W1_CQRS.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string Password { get; set; } = null!;
}
