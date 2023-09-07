using System.ComponentModel.DataAnnotations;

namespace NewsAPI.DTOs;

public class ArticleUpdateDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Header { get; set; }

    [Required]
    public string Text { get; set; }
}
