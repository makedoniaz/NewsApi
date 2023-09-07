using System.ComponentModel.DataAnnotations;

namespace NewsAPI.DTOs.User;

public class UserDTO
{
    [Required]
    public string Login { get; set; }

    [Required]
    public string Password { get; set; }
}
