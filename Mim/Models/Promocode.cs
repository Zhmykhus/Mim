using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mim.Data;

public partial class Promocode
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Size { get; set; }

    public int ProductId { get; set; }

}
