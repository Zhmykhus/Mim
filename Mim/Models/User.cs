using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mim.Data;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string CardNumber { get; set; } = null!;

    public int RoleId { get; set; }

  
}
