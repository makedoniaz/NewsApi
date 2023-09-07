

using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;
using NewsAPI.Logic.Base;
using System.ComponentModel.DataAnnotations;

namespace NewsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticleController : ControllerBase
{
    private readonly IArticleLogic logic;
    
    public ArticleController(IArticleLogic logic)
    {
        this.logic = logic;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(this.logic.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById([Required] int id)
    {
        var article = this.logic.GetById(id);

        if (article == null)
            return NotFound(id);

        return Ok(article);
    }

    [HttpPost]
    public IActionResult Post(ArticleCreateDTO article)
    {
        if (!ModelState.IsValid) 
            return BadRequest();

        return Ok(this.logic.Post(article));
    }

    [HttpDelete]
    public IActionResult DeleteById([Required] int id)
    {
        int? newId = this.logic.DeleteById(id);

        if (newId == null)
            return NotFound(newId);

        return Ok(newId);
    }

    [HttpPut]
    public IActionResult UpdateById(ArticleUpdateDTO article)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        int? id = logic.UpdateById(article);

        if (id == null)
            return NotFound(id);

        return Ok(id);
    }

    [Route("Filter")]
    [HttpPost]
    public IActionResult FilterArticles([Required] ArticleFilterDTO filter, [Required] int pageSize, [Required] int currentPage)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = logic.FilterArticles(filter, pageSize, currentPage);

        if (response == null)
            return NotFound(response);

        return Ok(response);
    }
}
