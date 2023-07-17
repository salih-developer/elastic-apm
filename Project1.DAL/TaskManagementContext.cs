using Microsoft.EntityFrameworkCore;
using Project1.DAL.Model;

namespace Project1.DAL
{
    public class TaskManagementContext : DbContext
    {
        public TaskManagementContext(DbContextOptions<TaskManagementContext> options)
       : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<UserTaskStatus> UserTaskStatuses { get; set; }
        public DbSet<UserTaskTrack> UserTaskTracks { get; set; }
    }
}
