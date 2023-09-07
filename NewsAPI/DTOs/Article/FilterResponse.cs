using NewsAPI.Models;

namespace NewsAPI.DTOs;

public class FilterResponse
{
    public IQueryable<Article>? Articles { get; set; }

    public bool HasNextPage { get; set; }
}
