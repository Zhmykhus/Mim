using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mim.Data;

public partial class Rating
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? Estimation { get; set; }
   
}
