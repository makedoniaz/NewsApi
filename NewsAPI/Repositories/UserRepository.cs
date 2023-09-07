using NewsAPI.DataContext.Base;
using NewsAPI.Models;
using NewsAPI.Repositories.Interfaces;

namespace NewsAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly INewsContext context;

        public UserRepository(INewsContext context)
        {
            this.context = context;
        }

        public IQueryable<User> GetAll()
        {
            return this.context.User.AsQueryable();
        }

        public User? GetById(int id)
        {
            return this.context.User.FirstOrDefault(user => user.Id == id);
        }

        public int Post(User user)
        {
            this.context.User.Add(user);
            this.context.SaveChanges();

            return user.Id;
        }

        public int? DeleteById(int id)
        {
            var user = this.GetById(id);

            if (user == null)
                return null;

            this.context.User.Remove(user);
            this.context.SaveChanges();

            return id;
        }

        public int UpdateById(User user)
        {
            context.User.Update(user);
            context.SaveChanges();
            return user.Id;
        }
    }
}
