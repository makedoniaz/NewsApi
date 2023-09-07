using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DataContext;
using NewsAPI.DataContext.Base;
using NewsAPI.Logic;
using NewsAPI.Logic.Base;
using NewsAPI.Logic.Interfaces;
using NewsAPI.Models;
using NewsAPI.Repositories;
using NewsAPI.Repositories.Interfaces;

namespace NewsAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddScoped<INewsContext, NewsContext>();

        builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
        builder.Services.AddScoped<IArticleLogic, ArticleLogic>();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserLogic, UserLogic>();

        builder.Services.AddScoped<ICommentRepository, CommentRepository>();
        builder.Services.AddScoped<ICommentLogic, CommentLogic>();

        builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
        {
            build.WithOrigins("http://localhost:3000/").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
        }
        ));

        builder.Services.AddDbContext<NewsContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("NewsContextConnectionString")));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("corspolicy");

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}