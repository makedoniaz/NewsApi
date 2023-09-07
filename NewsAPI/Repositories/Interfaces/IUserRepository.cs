using NewsAPI.Models;

namespace NewsAPI.Repositories.Interfaces;

public interface IUserRepository
{
    IQueryable<User> GetAll();

    User? GetById(int id);

    int Post(User element);

    int? DeleteById(int id);

    int UpdateById(User element);
}
