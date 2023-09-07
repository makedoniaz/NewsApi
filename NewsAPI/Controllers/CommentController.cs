using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs.Comment;
using NewsAPI.Logic.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NewsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentLogic logic;

    public CommentController(ICommentLogic logic)
    {
        this.logic = logic;
    }

    [HttpGet("{articleId}")]
    public IActionResult GetByArticleId([Required] int articleId)
    {
        return Ok(this.logic.GetByArticleId(articleId));
    }

    [HttpPost]
    public IActionResult Post(CommentCreateDTO comment)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        int? id = this.logic.Post(comment);

        if (id == null)
            return NotFound(comment);

        return Ok(id);
    }

    [HttpDelete]
    public IActionResult DeleteById([Required] int id)
    {
        int? newId = this.logic.DeleteById(id);

        if (newId == null)
            return NotFound(newId);

        return Ok(newId);
    }

    [HttpDelete("{articleId}")]
    public IActionResult DeleteByArticleId([Required] int articleId)
    {
        int id = this.logic.DeleteByArticleId(articleId);

        return Ok(id);
    }
}
