using Money_Tracker.BLL.Interfaces;
using Money_Tracker.BLL.Mappers;
using Money_Tracker.BLL.Models;
using Money_Tracker.DAL.Interfaces;

namespace Money_Tracker.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;

        public UserService(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }
        public IEnumerable<User> GetAll()
        {
            return _UserRepository.GetAll().Select(u => u.ToModel());
        }

        public User? GetById(int id)
        {
            return _UserRepository.GetById(id)?.ToModel();
        }
        public User Insert(User user)
        {
            return _UserRepository.Create(user.ToEntity()).ToModel();
        }
        public bool Update(int id, User user)
        {
            bool updated = _UserRepository.Update(id, user.ToEntity());
            if (!updated)
            {
                throw new Exception("User Not Found");
            }
            return updated;
        }
        public bool Delete(int id)
        {
            bool deleted = _UserRepository.Delete(id);
            if (!deleted)
            {
                throw new Exception("User Not Found");
            }
            return deleted;
        }

    }




}
