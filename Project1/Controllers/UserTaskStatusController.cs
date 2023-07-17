using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project1.DAL.Model;
using Project1.Services.Interface;
using System.Collections.Generic;

namespace Project1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTaskStatusController : ControllerBase
    {
        private readonly ILogger<UserTaskStatusController> _logger;
        private readonly IUserTaskStatusService _userTaskStatusService;

        public UserTaskStatusController(ILogger<UserTaskStatusController> logger, IUserTaskStatusService userTaskStatusService)
        {
            _logger = logger;
            this._userTaskStatusService = userTaskStatusService;
        }
        [HttpGet]
        public IEnumerable<UserTaskStatus> GetAll()
        {
            return _userTaskStatusService.GetAll();
        }
    }
}
