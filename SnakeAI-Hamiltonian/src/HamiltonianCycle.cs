using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeAI_Hamiltonian
{
    public class HamiltonianCycle
    {
        private readonly Random _random;
        private List<IntVector2> _markedVertices;
        private IntVector2 _startVertex;
        private IntVector2[] _vertices;
        public HamiltonianCycle() { _random = new Random(); }
        public bool Random { get; set; }

        public IEnumerable<IntVector2> CalculateCycle(IntVector2 size)
        {
            _markedVertices = new List<IntVector2>(size.Area);
            _vertices = new IntVector2[size.Area];
            for (var x = 0; x < size.X; x++)
            for (var y = 0; y < size.Y; y++)
                _vertices[x * size.Y + y] = new IntVector2(x, y);

            _startVertex = _vertices[_random.Next(_vertices.Length)];
            if (RecursivePathCalculation(_startVertex)) return _markedVertices.ToArray();
            throw new CouldNotFindCycleException();
        }

        private bool RecursivePathCalculation(IntVector2 vertex)
        {
            _markedVertices.Add(vertex);
            var remainingVertices = GetAdjacentPositions(vertex).ToList();
            if (remainingVertices.Count <= 0 && _vertices.All(IsMarked) &&
                GetAdjacentPositions(vertex, false).Contains(_startVertex)) return true;

            if (Random && remainingVertices.OrderBy(x => Guid.NewGuid()).Any(RecursivePathCalculation)) return true;
            if (!Random && remainingVertices.Any(RecursivePathCalculation)) return true;

            _markedVertices.Remove(vertex);
            return false;
        }

        private IEnumerable<IntVector2> GetAdjacentPositions(IntVector2 vertex, bool onlyUnMarked = true)
        {
            return _vertices.Where(v => !(onlyUnMarked && IsMarked(v)) && vertex.IsAdjacent(v));
        }

        private bool IsMarked(IntVector2 vertex) { return _markedVertices.Contains(vertex); }
    }
}