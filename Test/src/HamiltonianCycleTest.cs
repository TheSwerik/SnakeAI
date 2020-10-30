using System;
using NUnit.Framework;
using SnakeAI_Hamiltonian;

namespace Test
{
    public class HamiltonianCycleTest
    {
        private HamiltonianCycle _hamiltonian;
        [SetUp] public void Setup() { _hamiltonian = new HamiltonianCycle(); }

        [Test]
        public void Test2X2()
        {
            var size = new IntVector2(2, 2);
            foreach (var pos in _hamiltonian.CalculateCycle(size)) Console.Write(pos + ", ");
            Assert.Pass();
        }

        [Test]
        public void Test3X3()
        {
            var size = new IntVector2(3, 3);
            Assert.Throws<CouldNotFindCycleException>(() => _hamiltonian.CalculateCycle(size));
        }

        [Test]
        [Timeout(30000)]
        // [Ignore("This has a high chance of never completing.")]
        public void TestRandomSuccess()
        {
            var size = new IntVector2(new Random().Next(2, 10), new Random().Next(2, 10));
            if (size.X % 2 == 1 && size.Y % 2 == 1) size.X++;
            Console.WriteLine($"\n{size.X}x{size.Y}");
            foreach (var pos in _hamiltonian.CalculateCycle(size)) Console.Write(pos + ", ");
            Assert.Pass();
        }

        [Test]
        [Timeout(30000)]
        // [Ignore("This has a high chance of never completing.")]
        public void TestRandomSuccess74()
        {
            var size = new IntVector2(7, 4);
            foreach (var pos in _hamiltonian.CalculateCycle(size)) Console.Write(pos + ", ");
            Assert.Pass();
        }

        [Test]
        [Timeout(30000)]
        // [Ignore("This has a high chance of never completing.")]
        public void TestRandomFail()
        {
            var size = new IntVector2(new Random().Next(2, 10), new Random().Next(2, 10));
            if (size.X % 2 == 0) size.X++;
            if (size.Y % 2 == 0) size.Y++;
            Console.WriteLine($"\n{size.X}x{size.Y}");
            Assert.Throws<CouldNotFindCycleException>(() => _hamiltonian.CalculateCycle(size));
        }
    }
}