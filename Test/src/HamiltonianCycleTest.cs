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

        private void InitHamiltonian(bool random) { _hamiltonian.Random = random; }

        private void PrintAndPass(IntVector2 size) { PrintAndPass(size, IntVector2.Default); }

        private void PrintAndPass(IntVector2 size, IntVector2 startPos)
        {
            foreach (var pos in _hamiltonian.CalculateCycle(size, startPos)) Console.Write(pos + ", ");
            Assert.Pass();
        }

        private void Fail(IntVector2 size)
        {
            Assert.Throws<CouldNotFindCycleException>(() => _hamiltonian.CalculateCycle(size));
        }

        #region Test Cases

        [TestCase(true)]
        [TestCase(false)]
        public void Test2X2Success(bool random)
        {
            InitHamiltonian(random);
            var size = new IntVector2(2, 2);

            PrintAndPass(size);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Test3X3Failure(bool random)
        {
            InitHamiltonian(random);
            var size = new IntVector2(3, 3);

            Fail(size);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Test3X4Success(bool random)
        {
            InitHamiltonian(random);
            var size = new IntVector2(3, 4);

            PrintAndPass(size);
        }

        [TestCase(true)]
        [TestCase(false)]
        [Timeout(600_000)] // 10 minutes
        [Ignore("This takes 5 minutes.")]
        public void Test6X7From00Success(bool random)
        {
            InitHamiltonian(random);
            var size = new IntVector2(6, 7);
            var startPos = new IntVector2(0, 0);

            PrintAndPass(size, startPos);
        }

        [TestCase(true)]
        [TestCase(false)]
        [Timeout(300_000)] // 5 minutes
        // [Ignore("This has a high chance of taking a long time.")]
        public void TestRandomSuccess(bool random)
        {
            InitHamiltonian(random);
            var size = new IntVector2(_random.Next(2, 10), _random.Next(2, 10));
            if (size.X % 2 == 1 && size.Y % 2 == 1) size.X++;
            // size does not consist of 2 odd numbers.

            Console.WriteLine($"\n{size.X}x{size.Y}");
            PrintAndPass(size);
        }

        [TestCase(true)]
        [TestCase(false)]
        [Timeout(300_000)] // 5 minutes
        // [Ignore("This has a high chance of taking a long time.")]
        public void TestRandomFail(bool random)
        {
            InitHamiltonian(random);
            var size = new IntVector2(_random.Next(2, 10), _random.Next(2, 10));
            if (size.X % 2 == 0) size.X++;
            if (size.Y % 2 == 0) size.Y++;
            // size consists of 2 odd numbers.

            Fail(size);
        }

        #endregion
    }
}