using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Project1.DAL.Model;
using Project1.DAL.Repo;
using Project1.Services.Interface;

namespace Project1.Services
{
    public class UserTaskStatusService : IUserTaskStatusService
    {
        private readonly IGenericRepository<UserTaskStatus> _userTaskStatusRepository;
        private readonly ILogger<UserTaskStatusService> _logger;

        public UserTaskStatusService(IGenericRepository<UserTaskStatus> userTaskStatusRepository, ILogger<UserTaskStatusService> logger)
        {
            this._userTaskStatusRepository = userTaskStatusRepository;
            this._logger = logger;
        }

        public IQueryable<UserTaskStatus> GetAll()
        {
            return _userTaskStatusRepository.GetAll();
        }
    }
}