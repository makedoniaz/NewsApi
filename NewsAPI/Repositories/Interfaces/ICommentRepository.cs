using NewsAPI.Models;

namespace NewsAPI.Repositories.Interfaces;

public interface ICommentRepository
{
    IQueryable<Comment> GetByArticleId(int articleId);

    int Post(Comment element);

    int? DeleteById(int id);

    void DeleteComments(IEnumerable<Comment> comments);
}
