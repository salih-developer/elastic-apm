using Microsoft.Extensions.Logging;
using Project1.DAL.Model;
using Project1.DAL.Repo;
using Project1.Services.Interface;

namespace Project1.Services
{
    public class UserTaskTrackService : IUserTaskTrackService
    {
        private readonly IGenericRepository<UserTaskTrack> _taskTrackRepository;
        private readonly ILogger<UserService> _logger;

        public UserTaskTrackService(IGenericRepository<UserTaskTrack> taskTrackRepository, ILogger<UserService> logger)
        {
            this._taskTrackRepository = taskTrackRepository;
            this._logger = logger;
        }

        public async Task CreateAsync(UserTaskTrack task)
        {
            await _taskTrackRepository.CreateAsync(task);
        }

        public async Task DeleteAsync(int id)
        {
            await _taskTrackRepository.DeleteAsync(id);
        }

        public IQueryable<UserTaskTrack> GetAll()
        {
            return _taskTrackRepository.GetAll();
        }

        public async Task<UserTaskTrack> GetByIdAsync(int id)
        {
            return await _taskTrackRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(int id, UserTaskTrack task)
        {
            await _taskTrackRepository.UpdateAsync(id, task);
        }
    }
}
