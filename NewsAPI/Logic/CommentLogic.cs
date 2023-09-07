using Microsoft.EntityFrameworkCore;
using NewsAPI.DTOs;
using NewsAPI.DTOs.Comment;
using NewsAPI.Logic.Interfaces;
using NewsAPI.Models;
using NewsAPI.Repositories.Interfaces;
using System.Transactions;

namespace NewsAPI.Logic;

public class CommentLogic : ICommentLogic
{
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;
    private readonly IArticleRepository articleRepository;

    public CommentLogic(ICommentRepository commentRepository, IUserRepository userRepository, IArticleRepository articleRepository)
    {
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
        this.articleRepository = articleRepository;
    }

    public IEnumerable<CommentGetDTO> GetByArticleId(int articleId)
    {
        var comments = this.commentRepository.GetByArticleId(articleId);
        var users = this.userRepository.GetAll();

        var result = from c in comments
                     join u in users on c.AuthorId equals u.Id
                     select new CommentGetDTO
                     {
                         Id = c.Id,
                         Text = c.Text,
                         ArticleId = c.ArticleId,
                         AuthorLogin = u.Login,
                         CreationDate = c.CreationDate
                     };

        return result.ToList();
    }

    public int? Post(CommentCreateDTO comment)
    {
        bool isValid = userRepository.GetById(comment.AuthorId) != null && articleRepository.GetById(comment.ArticleId) != null;

        if (!isValid)
            return null;

        var newComment = new Comment()
        {
            Text = comment.Text,
            ArticleId = comment.ArticleId,
            AuthorId = comment.AuthorId,
            CreationDate = DateTime.Now
        };

        using TransactionScope scope = new TransactionScope();

        int id = this.commentRepository.Post(newComment);

        scope.Complete();

        return id;
    }

    public int? DeleteById(int id)
    {
        using TransactionScope scope = new TransactionScope();

        int? deletedId = this.commentRepository.DeleteById(id);

        scope.Complete();

        if (deletedId == null)
            return null;

        return id;
    }

    public int DeleteByArticleId(int articleId)
    {
        var comments = this.commentRepository.GetByArticleId(articleId);

        using TransactionScope scope = new TransactionScope();

        this.commentRepository.DeleteComments(comments);

        scope.Complete();

        return articleId;
    }
}
