using NewsAPI.DTOs;
using NewsAPI.Logic.Base;
using NewsAPI.Logic.Interfaces;
using NewsAPI.Models;
using NewsAPI.Repositories;
using NewsAPI.Repositories.Interfaces;
using System.Transactions;

namespace NewsAPI.Logic;

public class ArticleLogic : IArticleLogic
{
    private readonly ICommentLogic commentLogic;
    private readonly IArticleRepository articleRepository;

    public ArticleLogic(IArticleRepository articleRepository, ICommentLogic commentLogic)
    {
        this.articleRepository = articleRepository;
        this.commentLogic = commentLogic;
    }

    public IEnumerable<Article> GetAll()
    {
        var articles = this.articleRepository.GetAll();
        return SortArticles(articles).ToList();
    }

    public Article? GetById(int id)
    {
        return this.articleRepository.GetById(id);
    }

    public int Post(ArticleCreateDTO article)
    {
        var newArticle = new Article()
        {
            Header = article.Header,
            Text = article.Text,
            CreationDate = DateTime.Now
        };

        using TransactionScope scope = new TransactionScope();

        int id = this.articleRepository.Post(newArticle);

        scope.Complete();

        return id;
    }

    public int? DeleteById(int id)
    {

        using TransactionScope scope = new TransactionScope();

        commentLogic.DeleteByArticleId(id);
        int? deletedId = this.articleRepository.DeleteById(id);

        scope.Complete();

        if (deletedId == null)
            return null;

        return id;
    }

    public int? UpdateById(ArticleUpdateDTO article)
    {
        var newArticle = articleRepository.GetById(article.Id);

        if (newArticle == null)
            return null;

        newArticle.Header = article.Header;
        newArticle.Text = article.Text;
        newArticle.CreationDate = DateTime.Now;

        using TransactionScope scope = new TransactionScope();

        articleRepository.UpdateById(newArticle);

        scope.Complete();

        return newArticle.Id;
    }

    public FilterResponse? FilterArticles(ArticleFilterDTO filter, int pageSize, int currentPage)
    {
        if (pageSize <= 0 || currentPage <= 0)
            return null;

        var articles = articleRepository.GetAll();

        if (!string.IsNullOrEmpty(filter.Header))
            articles = articles.Where((article) => article.Header.Contains(filter.Header));

        if (!string.IsNullOrEmpty(filter.Text))
            articles = articles.Where((article) => article.Text.Contains(filter.Text));

        if (filter.From != null)
            articles = articles.Where((article) => article.CreationDate >= filter.From.Value);

        if (filter.To != null)
            articles = articles.Where((article) => article.CreationDate <= filter.To.Value);

        articles = SortArticles(articles);

        var temp = articles.Skip((currentPage - 1) * pageSize).Take(pageSize + 1);
        articles = temp.Take(pageSize);

        var response = new FilterResponse()
        {
            Articles = articles,
            HasNextPage = temp.Count() == pageSize + 1,
        };

        return response;
    }

    public IQueryable<Article> SortArticles(IQueryable<Article> articles)
    {
        return articles.OrderByDescending(article => article.CreationDate);
    }
}
