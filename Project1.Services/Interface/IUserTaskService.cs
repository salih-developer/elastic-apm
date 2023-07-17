using Project1.DAL.Model;

namespace Project1.Services.Interface
{
    public interface IUserTaskService
    {
        Task CreateAsync(UserTask task);
        Task DeleteAsync(int id);
        IQueryable<UserTask> GetAll();
        Task<UserTask> GetByIdAsync(int id);
        Task UpdateAsync(int id, UserTask task);
    }
}