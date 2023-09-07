using NewsAPI.DataContext.Base;
using NewsAPI.Models;
using NewsAPI.Repositories.Interfaces;

namespace NewsAPI.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly INewsContext context;

    public CommentRepository(INewsContext context)
    {
        this.context = context;
    }

    public IQueryable<Comment> GetAll()
    {
        return this.context.Comment.AsQueryable();
    }

    public Comment? GetById(int id)
    {
        return this.context.Comment.FirstOrDefault((c) => c.Id == id);
    }

    public IQueryable<Comment> GetByArticleId(int articleId)
    {
        return this.context.Comment.Where((c) => c.ArticleId == articleId);
    }

    public int Post(Comment comment)
    {
        this.context.Comment.Add(comment);
        this.context.SaveChanges();

        return comment.Id;
    }

    public int? DeleteById(int id)
    {
        var comment = this.GetById(id);

        if (comment == null)
            return null;

        this.context.Comment.Remove(comment);
        this.context.SaveChanges();

        return id;
    }

    public void DeleteComments(IEnumerable<Comment> comments)
    {
        this.context.Comment.RemoveRange(comments.ToList());
        this.context.SaveChanges();
    }

    public int UpdateById(Comment comment)
    {
        context.Comment.Update(comment);
        context.SaveChanges();

        return comment.Id;
    }
}
