using System.ComponentModel.DataAnnotations;

namespace NewsAPI.DTOs;

public class ArticleFilterDTO
{
    public string? Header { get; set; }

    public string? Text { get; set; }
    
    public DateTime? From { get; set; }

    public DateTime? To { get; set; }
}
