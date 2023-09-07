using NewsAPI.DTOs.User;
using NewsAPI.Logic.Base;
using NewsAPI.Models;
using NewsAPI.Repositories.Interfaces;
using System.Transactions;

namespace NewsAPI.Logic;

public class UserLogic : IUserLogic
{

    private readonly IUserRepository repository;

    public UserLogic(IUserRepository repository)
    {
        this.repository = repository;
    }

    public IEnumerable<User> GetAll()
    {
        return this.repository.GetAll();
    }

    public User? GetById(int id)
    {
        return this.repository.GetById(id);
    }

    public int Post(UserDTO user)
    {
        var password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password, 10);

        var newUser = new User()
        {
            Login = user.Login,
            Password = password,
        };

        using TransactionScope scope = new TransactionScope();

        int id = this.repository.Post(newUser);

        scope.Complete();

        return id;
    }

    public int? DeleteById(int id)
    {
        using TransactionScope scope = new TransactionScope();

        int? deletedId = this.repository.DeleteById(id);

        scope.Complete();

        if (deletedId == null)
            return null;

        return id;
    }

    public int? UpdateById(User user)
    {
        var newUser = repository.GetAll().FirstOrDefault((u) => u.Id == user.Id);

        if (newUser == null)
            return null;

        newUser.Login = user.Login;
        newUser.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password, 13);

        using TransactionScope scope = new TransactionScope();

        repository.UpdateById(newUser);

        scope.Complete();

        return newUser.Id;
    }

    public LoginResponse? Login(UserDTO user)
    {
        var userToLogin = repository.GetAll().FirstOrDefault(u => u.Login == user.Login);

        if (userToLogin == null)
            return null;

        return new LoginResponse() {
            UserId = userToLogin.Id,
            Login = userToLogin.Login,
            IsAuthenticated = BCrypt.Net.BCrypt.EnhancedVerify(user.Password, userToLogin.Password)
        };
    }
}
