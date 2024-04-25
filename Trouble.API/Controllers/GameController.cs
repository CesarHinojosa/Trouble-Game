using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Trouble.BL;
using Trouble.BL.Models;
using Trouble.PL.Data;

namespace Trouble.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> logger;
        private readonly DbContextOptions<TroubleEntities> options;


        public GameController(ILogger<GameController> logger,
                                      DbContextOptions<TroubleEntities> options)
        {
            this.logger = logger;
            this.options = options;
            logger.LogWarning("I was here!");
        }

        [HttpGet]
        public IEnumerable<Game> Get()
        {
            logger.LogWarning("Game-->");
            return new GameManager(options).Load();
        }

        [HttpGet("{id}")]
        public Game Get(Guid id)
        {
            return new GameManager(options).LoadById(id);
        }

        [HttpGet("GetByUser/{userId}")]
        public IEnumerable<Game> GetByUserId(Guid userId)
        {
            return new GameManager(options).LoadByUserId(userId);
        }

        [HttpPost("{rollback?}")]
        public int Post([FromBody] Game game, bool rollback = false)
        {
            try
            {
                return new GameManager(options).Insert(game, rollback);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}/{rollback?}")]
        public int Put(Guid id, [FromBody] Game game, bool rollback = false)
        {
            try
            {
                return new GameManager(options).Update(game, rollback);
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
                return new GameManager(options).Delete(id, rollback);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
