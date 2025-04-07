using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuppX.App.Models;

public class LoginRequest
{
    [Required]
    public required string Login { get; set; }
    [Required]
    public required string Password { get; set; }
}

public class RegisterRequest
{
    [Required]
    public required string Login { get; set; }
    [Required]
    public required string Password { get; set; }
}

public class RefreshRequest
{
    [Required]
    public required string RefreshToken { get; set; }
}