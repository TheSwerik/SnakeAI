using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeAI_Hamiltonian
{
    public class HamiltonianCycle
    {
        private readonly Random _random;
        private Path _path;
        private IntVector2 _startVertex;
        private IntVector2[] _vertices;
        public HamiltonianCycle() { _random = new Random(); }
        public bool Random { get; set; }

        public IEnumerable<IntVector2> CalculateCycle(IntVector2 size)
        {
            _path = new Path(size.Area);
            _vertices = new IntVector2[size.Area];
            for (var x = 0; x < size.X; x++)
            for (var y = 0; y < size.Y; y++)
                _vertices[x * size.Y + y] = new IntVector2(x, y);

            _startVertex = _vertices[_random.Next(_vertices.Length)];
            // _startVertex = _vertices.First(v => v.X == 2 && v.Y == 1);
            Console.WriteLine(_startVertex);
            if (IterativePathCalculation(_startVertex)) return _path.ToList();
            throw new CouldNotFindCycleException();
        }

        private bool IterativePathCalculation(IntVector2 startVertex)
        {
            _path.Add(startVertex);
            var tried = new List<Path>();
            while (true)
            {
                var remainingAdjacentVertices = GetAdjacentVertices(_path.Top(), tried).ToList();

                if (remainingAdjacentVertices.Count > 0)
                {
                    // Go a step further
                    var vertex = Random
                                     ? remainingAdjacentVertices.OrderBy(x => Guid.NewGuid()).First()
                                     : remainingAdjacentVertices[0];
                    if (!_path.Contains(vertex)) _path.Add(vertex);
                    continue;
                }

                // Finishes if all vertices are in the path and the startVertex is a Neighbor of the last Vertex
                if (_vertices.Length == _path.Length &&
                    GetAdjacentVertices(_path.Top(), onlyUnMarked: false).Contains(startVertex))
                    return true; // Finished

                // Go a back:
                tried.Add(_path.Clone());
                _path.RemoveTop();
                if (_path.IsEmpty()) return false;
            }
        }

        private IEnumerable<IntVector2> GetAdjacentVertices(IntVector2 vertex, ICollection<Path> paths = null,
                                                            bool onlyUnMarked = true)
        {
            return _vertices.Where(v =>
                                   {
                                       if (onlyUnMarked && IsMarked(v) || !vertex.IsAdjacent(v)) return false;
                                       if (paths == null) return true;
                                       var tempPath = _path.Clone();
                                       tempPath.Add(v);
                                       return paths.All(p => !p.Equals(tempPath));
                                   });
        }

        private bool IsMarked(IntVector2 vertex) { return _path.Contains(vertex); }
    }
}