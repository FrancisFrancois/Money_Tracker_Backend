using Money_Tracker.BLL.Models;

namespace Money_Tracker.BLL.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<User> GetAll();
        public User? GetById(int id);
        public User Insert(User user);
        public bool Update(int id, User user);
        public bool Delete(int id);
    }
}

