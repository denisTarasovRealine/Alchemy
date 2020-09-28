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
            foreach (int i in Enumerable.Range(1, 48))
            {
                lohLessList.Add(i);
            }

            Assert.AreEqual(48, lohLessList.Count);
        }
        
        [TestMethod]
        public void CopiedArray_OnCopyToWithZeroOffset()
        {
            using var lohLessList = new LOHLessCollection<int>();
            foreach (int i in Enumerable.Range(1, 48))
            {
                lohLessList.Add(i);
            }

            int[] destinationArray = new int[48];
            lohLessList.CopyTo(destinationArray, 0);

            Assert.AreEqual(48, destinationArray[47]);
        }

        [TestMethod]
        public void CopiedArray_OnCopyToWithNoneZeroOffset()
        {
            var temp = new List<int>(48);
            using var lohLessList = new LOHLessCollection<int>();
            foreach (int i in Enumerable.Range(1, 48))
            {
                lohLessList.Add(i);
                temp.Add(i);
            }

            int[] destinationArray1 = new int[58];
            temp.CopyTo(destinationArray1, 10);

            int[] destinationArray2 = new int[58];
            lohLessList.CopyTo(destinationArray2, 10);

            for (int i = 0; i < 58; i++)
            {
                Assert.AreEqual(destinationArray1[i], destinationArray2[i]);
            }
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
        public void Return47_OnRemoveOneItem()
        {
            using var lohLessList = new LOHLessCollection<int>();
            foreach (int i in Enumerable.Range(1, 48))
            {
                lohLessList.Add(i);
            }

            var isRemoved = lohLessList.Remove(4);
            Assert.IsTrue(isRemoved);
            Assert.AreEqual(47, lohLessList.Count);

            isRemoved = lohLessList.Remove(4);
            Assert.IsFalse(isRemoved);
            Assert.AreEqual(47, lohLessList.Count);

            lohLessList.Add(4);
            Assert.AreEqual(48, lohLessList.Count);

            isRemoved = lohLessList.Remove(4);
            Assert.IsTrue(isRemoved);
            Assert.AreEqual(47, lohLessList.Count);
        }
    }
}
