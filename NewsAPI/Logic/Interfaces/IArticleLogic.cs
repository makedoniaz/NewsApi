using NewsAPI.DTOs;
using NewsAPI.Models;

namespace NewsAPI.Logic.Base;

public interface IArticleLogic
{
    IEnumerable<Article> GetAll();

    Article? GetById(int id);

    int Post(ArticleCreateDTO article);

    int? DeleteById(int id);

    int? UpdateById(ArticleUpdateDTO article);

    FilterResponse? FilterArticles(ArticleFilterDTO filter, int pageSize, int currentPage);
}
