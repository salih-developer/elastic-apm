using Microsoft.EntityFrameworkCore;
using Project1.DAL;
using Project1.DAL.Model;
using Project1.DAL.Repos;

namespace Project1.Test
{
    public class GenericRepositoryTests
    {
        private DbContextOptions<TaskManagementContext> dbContextOptions;

        public GenericRepositoryTests()
        {
            var dbName = $"UserDb_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<TaskManagementContext>()
                .UseInMemoryDatabase(dbName)
                .EnableDetailedErrors(true)
                .Options;
        }
        [Fact]
        public async Task GetUsersAsync_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            // Act
            var userList = await repository.GetAll().ToListAsync();

            // Assert
            Assert.Equal(3, userList.Count);
        }

        [Fact]
        public async Task GetUserByIdAsync_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            // Act
            var user = await repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(user);
            Assert.Equal("User_1", user.Name);
        }

        [Fact]
        public async Task CreateAsync_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            // Act
            await repository.CreateAsync(new User()
            {
                Email = "User_4@hotmail.com",
                Name = "Some User",
                Surname = "Some User Surname",

            }); 

            // Assert
            var userList = await repository.GetAll().ToListAsync();
            Assert.Equal(4, userList.Count);
        }

        [Fact]
        public async Task DeleteAsync_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            // Act
            await repository.DeleteAsync(3);

            // Assert
            var userList = await repository.GetAll().ToListAsync();
            Assert.Equal(2, userList.Count);
        }

        [Fact]
        public async Task GetALL_include_Async_Success_Test()
        {
            var repository = await CreateRepositoryAsync();
            TaskManagementContext context = new TaskManagementContext(dbContextOptions);
            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromMinutes(5), StartDate = DateTime.Now, Subject = "include test",UserTaskStatusId=1 });
            context.SaveChanges();
            var gn=new GenericRepository<UserTask>(context);
            // Act
            var rs =  gn.GetAll().Include(x=>x.User).ToList();

            // Assert
            var userList = await repository.GetAll().ToListAsync();
            Assert.Equal(2, userList.Count);
        }

        [Fact]
        public async Task GetALL_Group_by_Async_Success_Test()
        {
            var repository = await CreateRepositoryAsync();
            TaskManagementContext context = new TaskManagementContext(dbContextOptions);
            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromHours(1), StartDate = DateTime.Now.Date, Subject = "include test", UserTaskStatusId = 1 });
            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromHours(1), StartDate = DateTime.Now.Date, Subject = "include test1", UserTaskStatusId = 1 });
            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromHours(1), StartDate = DateTime.Now.Date, Subject = "include test1", UserTaskStatusId = 1 });


            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromHours(2), StartDate = DateTime.Now.Date, Subject = "include test", UserTaskStatusId = 1 });
            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromHours(2), StartDate = DateTime.Now.Date, Subject = "include test1", UserTaskStatusId = 1 });
            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromHours(2), StartDate = DateTime.Now.Date, Subject = "include test1", UserTaskStatusId = 1 });

            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromHours(3), StartDate = DateTime.Now.Date, Subject = "include test", UserTaskStatusId = 1 });
            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromHours(3), StartDate = DateTime.Now.Date, Subject = "include test1", UserTaskStatusId = 1 });
            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromHours(3), StartDate = DateTime.Now.Date, Subject = "include test1", UserTaskStatusId = 1 });
            context.SaveChanges();
            var gn = new GenericRepository<UserTask>(context);
            // Act
            var rs = gn.GetAll()
                     .GroupBy(x => new { x.StartDate, x.StartTime })
                     .Select(f => new { f.First().StartDate, f.First().StartTime, datas = f.ToArray() }).ToArray();

            // Assert
            var userList = await repository.GetAll().ToListAsync();
            Assert.Equal(2, userList.Count);
        }

        private async Task<GenericRepository<User>> CreateRepositoryAsync()
        {
            TaskManagementContext context = new TaskManagementContext(dbContextOptions);
            await PopulateDataAsync(context);
            return new GenericRepository<User>(context);
        }

        private async Task PopulateDataAsync(TaskManagementContext context)
        {
            int index = 1;

            while (index <= 3)
            {
                var user = new User()
                {
                    Email= $"User_{index}@hotmail.com",
                    Name = $"User_{index}",
                    Surname = $"UserSurname_{index}",
                };

                index++;
                await context.Users.AddAsync(user);
            }

            await context.SaveChangesAsync();
        }
    }
}