using System.Collections.Generic;
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
            using var lohLessList = new LOHLessCollection<int>();
            foreach (int i in Enumerable.Range(1,48))
            {
                lohLessList.Add(i);
            }

            Assert.AreEqual(48, lohLessList.Count);
        }


        [TestMethod]
        public void EmptyCollection_OnClear()
        {
            using var lohLessList = new LOHLessCollection<int>();
            foreach (int i in Enumerable.Range(1, 48))
            {
                lohLessList.Add(i);
            }

            Assert.AreEqual(48, lohLessList.Count);

            lohLessList.Clear();
            Assert.AreEqual(0, lohLessList.Count);

            foreach (int i in Enumerable.Range(1, 48))
            {
                lohLessList.Add(i);
            }
            Assert.AreEqual(48, lohLessList.Count);
        }

        [TestMethod]
        public void Return8_OnRemove()
        {
            

            var t = 2 << 1;
            var t2 = 4 >> 1;

            using var lohLessList = new LOHLessCollection<int>();
            foreach (int i in Enumerable.Range(1, 48))
            {
                lohLessList.Add(i);
            }

            Assert.IsTrue(lohLessList.Remove(42));

            foreach (var i in lohLessList)
            {
                
            }

            //List<int> s;
            //s.Remove()

            //Assert.AreEqual(48, lohLessList.Count);
        }
    }
}
