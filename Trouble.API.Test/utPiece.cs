using Microsoft.SqlServer.Server;
using Trouble.BL.Models;

namespace Trouble.API.Test
{
    [TestClass]
    public class utPiece : utBase
    {
        //[TestMethod]
        //public async Task LoadTestAsync()
        //{
        //    await base.LoadTestAsync<Piece>();
        //}

        [TestMethod]
        public async Task InsertTestAsync()
        {
            Piece piece = new Piece { Color = "Test Color" };
            await base.InsertTestAsync<Piece>(piece);

        }


        //[TestMethod]
        //public async Task DeleteTestAsync()
        //{
        //    await base.DeleteTestAsync1<Piece>(new KeyValuePair<string, string>("Color", "Other"));
        //}

        //[TestMethod]
        //public async Task LoadByIdTestAsync()
        //{
        //    await base.LoadByIdTestAsync<Piece>(new KeyValuePair<string, string>("Color", "Other"));
        //}

        //[TestMethod]
        //public async Task UpdateTestAsync()
        //{
        //    Piece piece = new Piece { Color = "Test COlor" };
        //    await base.UpdateTestAsync<Piece>(new KeyValuePair<string, string>("Color", "Other"), piece);

        //}
    }
}