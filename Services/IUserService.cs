using Web_Sample.Models;

namespace Web_Sample.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        User GetById(int id);
        IEnumerable<User> GetAll();
        void Create(User user);
        void Update(User user);
        void Delete(int id);
    }
}
