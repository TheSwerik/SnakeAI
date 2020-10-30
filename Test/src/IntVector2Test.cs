using NUnit.Framework;
using SnakeAI_Hamiltonian;

namespace Test
{
    public class IntVector2Test
    {
        [Test]
        public void TestCompareEqualIntVector2()
        {
            var vector1 = new IntVector2(7, 5);
            var vector2 = new IntVector2(7, 5);

            Assert.IsTrue(vector1.Equals(vector2), "Equals is False. {0} != {1}", vector1, vector2);
            Assert.AreEqual(vector1, vector2, "The two are not Equal. {0} != {1}", vector1, vector2);
            Assert.AreNotSame(vector1, vector2, "The two are the Same.");
        }

        [Test]
        public void TestCompareDifferentIntVector2()
        {
            var vector1 = new IntVector2(7, 5);
            var vector2 = new IntVector2(1, 6);

            Assert.IsFalse(vector1.Equals(vector2), "Equals is True. {0} != {1}", vector1, vector2);
            Assert.AreNotEqual(vector1, vector2, "The two are Equal. {0} != {1}", vector1, vector2);
            Assert.AreNotSame(vector1, vector2, "The two are the Same.");
        }
    }
}