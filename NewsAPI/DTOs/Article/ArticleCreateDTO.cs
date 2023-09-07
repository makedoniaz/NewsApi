using System.ComponentModel.DataAnnotations;

namespace NewsAPI.DTOs;

public class ArticleCreateDTO
{
    [Required]
    [MaxLength(100)]
    public string Header { get; set; }

    [Required]
    public string Text { get; set; }
}
