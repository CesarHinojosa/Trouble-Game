using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trouble.BL.Models;
using Trouble.BL;
using Trouble.PL.Data;

namespace Trouble.API.Controllers
{
    public class UserGameController : ControllerBase
    {
        private readonly ILogger<UserGameController> logger;
        private readonly DbContextOptions<TroubleEntities> options;

        public UserGameController(ILogger<UserGameController> logger,
                                    DbContextOptions<TroubleEntities> options)
        {
            this.logger = logger;
            this.options = options;
            logger.LogWarning("I was here!");
        }

        [HttpPost("{rollback?}")]
        //needs to be Guid or int (public Guid)??
        //Guid user?
        public int Post([FromBody] Guid userId, Guid gameId,  bool rollback = false)
        {
            try
            {
                return new UserGameManager(options).Insert(userId, gameId, rollback);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}/{rollback?}")]
        public int Put(Guid id, [FromBody] Guid userId, Guid gameId, bool rollback = false)
        {
            try
            {
                return new UserGameManager(options).Update(userId, gameId, rollback);
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
