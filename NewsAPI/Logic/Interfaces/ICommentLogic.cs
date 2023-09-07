using NewsAPI.DTOs;
using NewsAPI.DTOs.Comment;
using NewsAPI.Models;

namespace NewsAPI.Logic.Interfaces;

public interface ICommentLogic
{
    IEnumerable<CommentGetDTO> GetByArticleId(int articleId);

    int? Post(CommentCreateDTO comment);

    int? DeleteById(int id);

    int DeleteByArticleId(int articleId);
}
