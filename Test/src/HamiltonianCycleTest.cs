using System;
using NUnit.Framework;
using SnakeAI_Hamiltonian;

namespace Test
{
    public class HamiltonianCycleTest
    {
        private HamiltonianCycle _hamiltonian;
        private Random _random;

        [SetUp]
        public void Setup()
        {
            _hamiltonian = new HamiltonianCycle();
            _random = new Random();
        }

        [Test]
        public void Test2X2Success()
        {
            var size = new IntVector2(2, 2);
            foreach (var pos in _hamiltonian.CalculateCycle(size)) Console.Write(pos + ", ");
            Assert.Pass();
        }

        [Test]
        public void Test3X3Failure()
        {
            var size = new IntVector2(3, 3);
            Assert.Throws<CouldNotFindCycleException>(() => _hamiltonian.CalculateCycle(size));
        }

        [Test]
        public void Test3X4Success()
        {
            var size = new IntVector2(3, 4);
            foreach (var pos in _hamiltonian.CalculateCycle(size)) Console.Write(pos + ", ");
            Assert.Pass();
        }

        [Test]
        public void Test6X7From55Success()
        {
            var size = new IntVector2(6, 7);
            var startPos = new IntVector2(0, 0);
            foreach (var pos in _hamiltonian.CalculateCycle(size, startPos)) Console.Write(pos + ", ");
            Assert.Pass();
        }

        [Test]
        [Timeout(30000)]
        // [Ignore("This has a high chance of never completing.")]
        public void TestRandomSuccess()
        {
            var size = new IntVector2(_random.Next(2, 10), _random.Next(2, 10));
            if (size.X % 2 == 1 && size.Y % 2 == 1) size.X++;
            Console.WriteLine($"\n{size.X}x{size.Y}");
            foreach (var pos in _hamiltonian.CalculateCycle(size)) Console.Write(pos + ", ");
            Assert.Pass();
        }

        [Test]
        [Timeout(30000)]
        // [Ignore("This has a high chance of never completing.")]
        public void TestRandomFail()
        {
            var size = new IntVector2(_random.Next(2, 10), _random.Next(2, 10));
            if (size.X % 2 == 0) size.X++;
            if (size.Y % 2 == 0) size.Y++;
            Console.WriteLine($"\n{size.X}x{size.Y}");
            Assert.Throws<CouldNotFindCycleException>(() => _hamiltonian.CalculateCycle(size));
        }
    }
}