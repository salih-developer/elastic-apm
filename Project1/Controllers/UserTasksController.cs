using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project1.DAL.Model;
using Project1.Services;
using Project1.Services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTasksController : ControllerBase
    {
        private readonly ILogger<UserTasksController> _logger;
        private readonly IUserTaskService _userTaskService;

        public UserTasksController(ILogger<UserTasksController> logger, IUserTaskService userTaskService)
        {
            _logger = logger;
            this._userTaskService = userTaskService;
        }

        [HttpGet]
        public IEnumerable<UserTask> GetAll()
        {
            return _userTaskService.GetAll()
                .Include(x => x.User)
                .Include(x => x.UserTaskStatus)
                .ToList();
        }
        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _userTaskService.GetByIdAsync(id));
        }
        // POST api/users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserTask task)
        {
            await _userTaskService.CreateAsync(task);
            return CreatedAtAction("Get", new { id = task.Id }, task);
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserTask task)
        {
            await _userTaskService.UpdateAsync(id, task);
            return NoContent();
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userTaskService.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet]
        [Route("Listoftoday")]
        public async Task<IEnumerable<object>> Listoftoday(int id)
        {
            var rs =await _userTaskService.GetAll().Include(x=>x.UserTaskStatus).Where(x => x.UserId == id)
                     .GroupBy(x => new { x.StartDate, x.StartTime })
                     .Select(f => new { f.First().StartDate, f.First().StartTime, datas = f.ToArray() }).ToArrayAsync();
            return rs;
        }
    }
}
