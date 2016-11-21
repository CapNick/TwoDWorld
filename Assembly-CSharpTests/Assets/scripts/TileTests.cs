using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class TileTests
    {
        Tile tile = new Tile(1, 1);
        Tile tile2 = new Tile(2, 1);
        [TestMethod()]
        public void IsTileEmptyTest()
        {
            Assert.AreEqual(tile.IsTileEmpty(), true);
            Assert.AreEqual(tile2.IsTileEmpty(), false);
        }
    }
}