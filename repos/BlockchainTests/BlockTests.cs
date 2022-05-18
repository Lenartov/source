using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Tests
{
    [TestClass()]
    public class BlockTests
    {

        [TestMethod()]
        public void SerializeTest()
        {
           /* Block block = new Block();

            string result = block.Serialize();
            string correctValue = "{\"Created\":\"\\/Date(1652216400000+0300)\\/\",\"Data\":\"Penis\",\"Hash\":\"7e543aeaddf41419e5ecd9b1aece5dad247c8f399ab808a17ca40c3656487c54\",\"PrevHash\":\"000000\",\"User\":\"Penis\"}";
            Assert.AreEqual(correctValue, result);

            Block block1 = Block.Deserialize(result);
            Assert.AreEqual(block.Data, block1.Data);
            Assert.AreEqual(block.User, block1.User);
            Assert.AreEqual(block.Created, block1.Created);
            Assert.AreEqual(block.Hash, block1.Hash);
            Assert.AreEqual(block.PrevHash, block1.PrevHash);*/
        }
    }
}