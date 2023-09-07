using NewsAPI.DTOs;
using NewsAPI.DTOs.User;
using NewsAPI.Models;

namespace NewsAPI.Logic.Base;

public interface IUserLogic
{
    IEnumerable<User> GetAll();

    User? GetById(int id);

    int Post(UserDTO user);

    int? DeleteById(int id);

    int? UpdateById(User article);

    LoginResponse? Login(UserDTO user);
}
