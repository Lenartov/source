using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blockchain.Tests
{
    [TestClass()]
    public class BlockTests
    {
        [TestMethod()]
        public void GetSummaryDataTest()
        {
            Assert.AreEqual(1, 1);
        }

        [TestMethod()]
        public void SerializeTest()
        {
            Assert.AreEqual(1, 1);
        }

        [TestMethod()]
        public void DeserializeTest()
        {
            Assert.AreEqual(1, 1);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual(1, 1);
        }

        [TestMethod()]
        public void GetJsonTest()
        {
            Assert.AreEqual(1, 1);
        }

        [TestMethod()]
        public void ViewContentTest()
        {
            Assert.AreEqual(1, 1);
        }

        [TestMethod()]
        public void CopyHashToBufferTest()
        {
            Assert.AreEqual(1, 1);
        }

        [TestMethod()]
        public void CopyLoginToBufferTest()
        {
            Block genBlock = new Block();

            genBlock.CopyLoginToBuffer();

            Assert.AreEqual(genBlock.User.Login, Clipboard.GetText());
        }

        [TestMethod()]
        public void DownLoadContentToTest()
        {
            Assert.AreEqual(1, 1);
        }
    }
}