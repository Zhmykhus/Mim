using System;
using System.Collections.Generic;

namespace Mim.Data;

public partial class PasswordReset
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Code { get; set; } = null!;
}
