using System.ComponentModel.DataAnnotations;

namespace NewsAPI.DTOs.Comment;

public class CommentCreateDTO
{
    [Required]
    [MaxLength(3000)]
    public string Text { get; set; }

    [Required] 
    public int ArticleId { get; set; }

    [Required]
    public int AuthorId { get; set; }
}
