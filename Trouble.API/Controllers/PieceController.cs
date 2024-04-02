using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trouble.BL.Models;
using Trouble.BL;
using Trouble.PL.Data;
using Microsoft.SqlServer.Server;

namespace Trouble.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PieceController : ControllerBase
    {
        private readonly ILogger<PieceController> logger;
        private readonly DbContextOptions<TroubleEntities> options;


        public PieceController(ILogger<PieceController> logger,
                                      DbContextOptions<TroubleEntities> options)
        {
            this.logger = logger;
            this.options = options;
            logger.LogWarning("I was here!");
        }

        [HttpGet]
        public IEnumerable<Piece> Get()
        {
            logger.LogWarning("Piece-->");
            return new PieceManager(options).Load();
        }

        [HttpGet("{id}")]
        public Piece Get(Guid id)
        {
            return new PieceManager(options).LoadById(id);
        }

        [HttpPost("{rollback?}")]
        //needs to be Guid (public Guid)??
        public int Post([FromBody] Piece piece, bool rollback = false)
        {
            try
            {
                return new PieceManager(options).Insert(piece, rollback);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPut("{id}/{rollback?}")]
        public int Put(Guid id, [FromBody] Piece piece, bool rollback = false)
        {
            try
            {
                return new PieceManager(options).Update(piece, rollback);
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
                return new PieceManager(options).Delete(id, rollback);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
