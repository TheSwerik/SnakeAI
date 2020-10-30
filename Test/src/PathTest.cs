using NUnit.Framework;
using SnakeAI_Hamiltonian;

namespace Test
{
    public class PathTest
    {
        [Test]
        public void TestCompareEqualPaths()
        {
            var path1 = new Path(1);
            path1.Add(new IntVector2(7, 5));
            var path2 = new Path(1);
            path2.Add(new IntVector2(7, 5));

            Assert.IsTrue(path1.Equals(path2), "Equals is False. {0} != {1}", path1, path2);
            Assert.AreNotEqual(path1, path2, "The two are Equal. {0} != {1}", path1, path2);
            Assert.AreNotSame(path1, path2, "The two are the Same.");
        }

        [Test]
        public void TestCompareDifferentPaths()
        {
            var path1 = new Path(1);
            path1.Add(new IntVector2(7, 5));
            var path2 = new Path(1);
            path2.Add(new IntVector2(1, 6));
            var path3 = new Path(1);
            path3.Add(new IntVector2(7, 5));
            path3.Add(new IntVector2(1, 6));

            Assert.IsFalse(path1.Equals(path2), "Equals is True. {0} != {1}", path1, path2);
            Assert.IsFalse(path1.Equals(path3), "Equals is True. {0} != {1}", path1, path3);
            Assert.IsFalse(path2.Equals(path3), "Equals is True. {0} != {1}", path2, path3);

            Assert.AreNotEqual(path1, path2, "The two are Equal. {0} != {1}", path1, path2);
            Assert.AreNotEqual(path1, path3, "The two are Equal. {0} != {1}", path1, path3);
            Assert.AreNotEqual(path2, path3, "The two are Equal. {0} != {1}", path2, path3);

            Assert.AreNotSame(path1, path2, "The two are the Same.");
            Assert.AreNotSame(path1, path3, "The two are the Same.");
            Assert.AreNotSame(path2, path3, "The two are the Same.");
        }
    }
}