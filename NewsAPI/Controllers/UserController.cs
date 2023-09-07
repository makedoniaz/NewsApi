using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs.User;
using NewsAPI.Logic.Base;
using NewsAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace NewsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserLogic logic;

    public UserController(IUserLogic logic)
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
        var user = this.logic.GetById(id);

        if (user == null)
            return NotFound(id);

        return Ok(user);
    }

    [HttpPost]
    public IActionResult Post(UserDTO user)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        return Ok(this.logic.Post(user));
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
    public IActionResult UpdateById(User user)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        int? id = logic.UpdateById(user);

        if (id == null)
            return NotFound(id);

        return Ok(id);
    }

    [HttpPost]
    [Route("Login")]
    public IActionResult Login(UserDTO user)
    {
        var response = this.logic.Login(user);

        if (response == null)
            return NotFound();

        return Ok(response);
    }
}
