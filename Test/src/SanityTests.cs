using System.Collections.Generic;
using NUnit.Framework;

namespace Test
{
    public class SanityTests
    {
        [Test]
        public void TestCapacityCount()
        {
            const int length = 2;
            var list = new List<int>(length);

            Assert.IsFalse(list.Count == length);
            list.Add(7);
            list.Add(42);
            Assert.IsTrue(list.Count == length);
        }
    }
}