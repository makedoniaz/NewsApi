using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsAPI.Models;

[Table("Article")]
public class Article
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Header { get; set; }

    [Required]
    public string Text { get; set; }

    [Required]    
    public DateTime CreationDate { get; set; }
}
