using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuppX.App.Models;

public class LoginRequest
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
}

public class RegisterRequest
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
}

public class RefreshRequest
{
    [Required]
    public string RefreshToken { get; set; }
}