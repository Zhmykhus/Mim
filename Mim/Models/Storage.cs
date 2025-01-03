using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mim.Data;

public partial class Storage
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int? StorageStatusId { get; set; }

    public int Id { get; set; }
    
}
