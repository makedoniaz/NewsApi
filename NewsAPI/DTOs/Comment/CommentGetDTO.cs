using System.ComponentModel.DataAnnotations;

namespace NewsAPI.DTOs.Comment;

public class CommentGetDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Text { get; set; }

    [Required]
    public int ArticleId { get; set; }

    [Required]
    public string AuthorLogin { get; set; }

    [Required]
    public DateTime CreationDate { get; set; }
}
