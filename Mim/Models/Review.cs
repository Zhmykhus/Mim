using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mim.Data;

public partial class Review
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public string Descriptoin { get; set; } = null!;

    public int Estimation { get; set; }
    
}
