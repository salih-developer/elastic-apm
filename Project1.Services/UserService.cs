using Microsoft.Extensions.Logging;
using Project1.DAL.Model;
using Project1.DAL.Repo;
using Project1.DAL.Repos;
using Project1.Services.Interface;

namespace Project1.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IGenericRepository<User> userRepository, ILogger<UserService> logger)
        {
            this._userRepository = userRepository;
            this._logger = logger;
        }

        public async Task CreateAsync(User user)
        {
            await _userRepository.CreateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public IQueryable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(int id, User user)
        {
            await _userRepository.UpdateAsync(id, user);
        }
    }
}
