using Mim.DataBase;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;


namespace Mim.Data;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public string? Promocode { get; set; }

    public decimal Price { get; set; }

    public int OrderStatusId { get; set; }

    public bool? HasReview { get; set; }

    public Product Product => DB.Instance.ListProducts.FirstOrDefault(s => s.Id == ProductId);
    [JsonIgnore]
    public string Status => DB.Instance.ListOrderStatuses.FirstOrDefault(s => s.Id == OrderStatusId)?.Title;
}
