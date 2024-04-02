using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trouble.BL.Models;
using Trouble.BL;
using Trouble.PL.Data;

namespace Trouble.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly DbContextOptions<TroubleEntities> options;

        public UserController(ILogger<UserController> logger,
                                     DbContextOptions<TroubleEntities> options)
        {
            this.logger = logger;
            this.options = options;
            logger.LogWarning("I was here!");
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            logger.LogWarning("Game-->");
            return new UserManager(options).Load();
        }

        [HttpGet("{id}")]
        public User Get(Guid id)
        {
            return new UserManager(options).LoadById(id);
        }

        [HttpPost("{rollback?}")]
        //needs to be Guid or int (public Guid)??
        public int Post([FromBody] User user, bool rollback = false)
        {
            try
            {
                return new UserManager(options).Insert(user, rollback);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}/{rollback?}")]
        public int Put(Guid id, [FromBody] User user, bool rollback = false)
        {
            try
            {
                return new UserManager(options).Update(user, rollback);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}/{rollback?}")]
        public int Delete(Guid id, bool rollback = false)
        {
            try
            {
                return new UserManager(options).Delete(id, rollback);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
