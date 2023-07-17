using Project1.DAL.Model;

namespace Project1.Services.Interface
{
    public interface IUserTaskTrackService
    {
        Task CreateAsync(UserTaskTrack task);
        Task DeleteAsync(int id);
        IQueryable<UserTaskTrack> GetAll();
        Task<UserTaskTrack> GetByIdAsync(int id);
        Task UpdateAsync(int id, UserTaskTrack task);
    }
}