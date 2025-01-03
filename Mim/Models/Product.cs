using Mim.DataBase;
using Mim.Tools;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace Mim.Data;

public partial class Product
{
    public int Id { get; set; }

    public int Article { get; set; }

    public string Title { get; set; } = null!;

    public decimal Cost { get; set; }

    public ushort Quantity { get; set; }

    public string? Description { get; set; }

    public int? Rating { get; set; }

    public int ProviderId { get; set; }

    public int? SizeId { get; set; }

    public int CategoryId { get; set; }

    public int Discount { get; set; }

    public byte[]? Image { get; set; }

    [JsonConverter(typeof(TimeOnlyJsonConvert))]
    public TimeOnly? CreationTime { get; set; }

    public bool? IsDeleted { get; set; }

    public int TimeDelivery { get; set; }

    public string CategoryName => DB.Instance.ListCategories.FirstOrDefault(s => s.Id == CategoryId)?.Title;
    public string SizeName => DB.Instance.ListSize.FirstOrDefault(s => s.Id == SizeId)?.Title;
    public string ProviderName => DB.Instance.ListProviders.FirstOrDefault(s => s.Id == ProviderId)?.Name;

    [JsonIgnore]
    public string Promocode { get; set; }
}
