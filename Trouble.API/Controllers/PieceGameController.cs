using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trouble.BL;
using Trouble.BL.Models;
using Trouble.PL.Data;

namespace Trouble.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PieceGameController : ControllerBase
    {
        private readonly ILogger<PieceGameController> logger;
        private readonly DbContextOptions<TroubleEntities> options;

        public PieceGameController(ILogger<PieceGameController> logger,
                                    DbContextOptions<TroubleEntities> options)
        {
            this.logger = logger;
            this.options = options;
            logger.LogWarning("I was here!");
        }

        [HttpGet]
        public IEnumerable<PieceGame> Get()
        {
            logger.LogWarning("PieceGame-->");
            return new PieceGameManager(options).Load();
        }

        [HttpGet("{id}")]
        public PieceGame Get(Guid id)
        {
            return new PieceGameManager(options).LoadById(id);
        }

        [HttpGet("{gameId}")]
        public IEnumerable<PieceGame> GetByGame(Guid gameId)
        {
            logger.LogWarning("PieceGame-->");
            return new PieceGameManager(options).Load(gameId);
        }

        [HttpPost("{rollback?}")]
        //needs to be Guid or int (public Guid)??
        //Guid user?
        public int Post([FromBody] Guid pieceId, Guid gameId, bool rollback = false)
        {
            try
            {
                return new PieceGameManager(options).Insert(pieceId, gameId, rollback);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPut("{id}/{rollback?}")]
        public int Put(Guid id, [FromBody] Guid userId, Guid gameId, int location, bool rollback = false)
        {

            try
            {
                //Update wrong?
                return new PieceGameManager(options).Update(userId, gameId, location, rollback);
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
                return new PieceGameManager(options).Delete(id, rollback);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
