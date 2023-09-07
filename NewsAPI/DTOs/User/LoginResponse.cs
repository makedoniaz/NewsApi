namespace NewsAPI.DTOs.User;

public class LoginResponse
{
    public int UserId { get; set; }

    public string? Login { get; set; }

    public bool IsAuthenticated { get; set; }
}
