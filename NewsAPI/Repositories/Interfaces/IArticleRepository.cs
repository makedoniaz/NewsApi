using NewsAPI.Models;

namespace NewsAPI.Repositories.Interfaces;

public interface IArticleRepository
{
    IQueryable<Article> GetAll();

    Article? GetById(int id);

    int Post(Article element);

    int? DeleteById(int id);

    int UpdateById(Article element);
}
