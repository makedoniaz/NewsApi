using Microsoft.EntityFrameworkCore;
using NewsAPI.DataContext.Base;
using NewsAPI.DTOs;
using NewsAPI.Models;
using NewsAPI.Repositories.Interfaces;

namespace NewsAPI.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly INewsContext context;

    public ArticleRepository(INewsContext context)
    {
        this.context = context;
    }

    public IQueryable<Article> GetAll()
    {
        return this.context.Article.AsQueryable();
    }

    public Article? GetById(int id)
    {
        return this.context.Article.FirstOrDefault(article => article.Id == id);
    }

    public int Post(Article article)
    {
        this.context.Article.Add(article);
        this.context.SaveChanges();

        return article.Id;
    }

    public int? DeleteById(int id)
    {
        var article = this.GetById(id);

        if (article == null)
            return null;

        this.context.Article.Remove(article);
        this.context.SaveChanges();

        return id;
    }

    public int UpdateById(Article article)
    {
        context.Article.Update(article);
        context.SaveChanges();

        return article.Id;
    }
}
