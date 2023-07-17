using Project1.DAL.Model;

namespace Project1.Services.Interface
{
    public interface IUserTaskStatusService
    {
        IQueryable<UserTaskStatus> GetAll();
    }
}