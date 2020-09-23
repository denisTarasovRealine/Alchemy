using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var lohLessList = new LOHLessList<int>())
            {
                foreach (int i in Enumerable.Range(1,48))
                {
                    lohLessList.Add(i);
                }
            }
        }
    }
}
