using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsAPI.Models;

[Table("Comment")]
public class Comment
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Text { get; set; }

    [Required]
    public int ArticleId { get; set; }

    [Required]
    public int AuthorId { get; set; }

    [Required]
    public DateTime CreationDate { get; set; }
}
