//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.IO;
//using Trouble.BL;
//using Trouble.BL.Models;
//using Trouble.PL.Data;

//namespace Trouble.API.Controllers
//{
//    public class GameController : ControllerBase
//    {
//        private readonly ILogger<GameController> logger;
//        private readonly DbContextOptions<TroubleEntities> options;


//        public GameController(ILogger<GameController> logger,
//                                      DbContextOptions<TroubleEntities> options)
//        {
//            this.logger = logger;
//            this.options = options;
//            logger.LogWarning("I was here!");
//        }

//        [HttpGet]
//        public IEnumerable<Game> Get()
//        {
//            logger.LogWarning("Game-->");

//            //When Do I know if it just needs options or both Logger and options?
//            return new GameManager(options).Load();
//        }
//        [HttpGet("{id}")]
//        public Game Get(Guid id)
//        {
//            return new GameManager(options).LoadById(id);
//        }

//        //needs to be Guid (public Guid)??
//        [HttpPost("{rollback?}")]
//        public int Post([FromBody] Game game, bool rollback = false)
//        {
//            try
//            {
//                return new GameManager(options).Insert(game, rollback);
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        //needs to be Guid (public Guid)??
//        [HttpPut("{id}/{rollback?}")]
//        public int Put(Guid id, [FromBody] Game game, bool rollback = false)
//        {
//            try
//            {
//                return new GameManager(options).Update(game, rollback);
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        //needs to be Guid (public Guid)??
//        [HttpDelete("{id}/{rollback?}")]
//        public int Delete(Guid id, bool rollback = false)
//        {
//            try
//            {
//                return new GameManager(options).Delete(id, rollback);
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }
//    }
//}
