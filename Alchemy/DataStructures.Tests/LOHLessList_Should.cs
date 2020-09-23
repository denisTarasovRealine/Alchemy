using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures.Tests
{
    [TestClass]
    public class LOHLessList_Should
    {
        [TestMethod]
        public void Return48_OnAdd48Items()
        {
            using var lohLessList = new LOHLessList<int>();
            foreach (int i in Enumerable.Range(1,48))
            {
                lohLessList.Add(i);
            }

            Assert.AreEqual(48, lohLessList.Count);
        }
    }
}