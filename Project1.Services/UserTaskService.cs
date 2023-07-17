using Microsoft.Extensions.Logging;
using Project1.DAL.Model;
using Project1.DAL.Repo;
using Project1.Services.Interface;

namespace Project1.Services
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IGenericRepository<UserTask> _taskRepository;
        private readonly IGenericRepository<UserTaskTrack> _taskTrackRepository;
        private readonly ILogger<UserService> _logger;

        public UserTaskService(IGenericRepository<UserTask> taskRepository, IGenericRepository<UserTaskTrack> taskTrackRepository, ILogger<UserService> logger)
        {
            this._taskRepository = taskRepository;
            this._taskTrackRepository = taskTrackRepository;
            this._logger = logger;
        }

        public async Task CreateAsync(UserTask task)
        {
            await _taskRepository.CreateAsync(task);
        }

        public async Task DeleteAsync(int id)
        {
            await _taskRepository.DeleteAsync(id);
        }

        public IQueryable<UserTask> GetAll()
        {
            return _taskRepository.GetAll();
        }

        public async Task<UserTask> GetByIdAsync(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(int id, UserTask task)
        {
            //unit of work pattern it can be good :)

            var usertask = await _taskRepository.GetByIdAsync(id);
            UserTaskTrack userTaskTrack = new() { UserTaskId = id, UserTaskStatusID = usertask.UserTaskStatusId };

            await _taskRepository.UpdateAsync(id, task);
            //track record
            await _taskTrackRepository.CreateAsync(userTaskTrack);

        }
    }
}
