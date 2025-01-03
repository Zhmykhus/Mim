using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mim.Data;

public partial class Category
{
    public int Id { get; set; }

    public string? Title { get; set; }
    
}
