using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeAI_Hamiltonian
{
    public class HamiltonianCycle
    {
        private readonly Random _random;
        private Path _path;
        private List<Path> _triedPaths;
        private IntVector2[] _vertices;
        public HamiltonianCycle() { _random = new Random(); }
        public bool Random { get; set; }

        public IEnumerable<IntVector2> CalculateCycle(IntVector2 size)
        {
            return CalculateCycle(size, IntVector2.Default);
        }

        public IEnumerable<IntVector2> CalculateCycle(IntVector2 size, IntVector2 startVertex)
        {
            _path = new Path(size.Area);
            _triedPaths = new List<Path>();
            _vertices = new IntVector2[size.Area];
            for (var x = 0; x < size.X; x++)
            for (var y = 0; y < size.Y; y++)
                _vertices[x * size.Y + y] = new IntVector2(x, y);

            if (startVertex.Equals(IntVector2.Default)) startVertex = _vertices[_random.Next(_vertices.Length)];
            if (!IterativePathCalculation(startVertex)) throw new CouldNotFindCycleException();

            _triedPaths = null;
            return _path.ToList();
        }

        private bool IterativePathCalculation(IntVector2 startVertex)
        {
            _path.Add(startVertex);
            while (!_path.IsEmpty())
            {
                var remainingAdjacentVertices = GetAdjacentVertices(_path.Top(), _triedPaths).ToList();

                if (remainingAdjacentVertices.Count > 0)
                {
                    GoForward(remainingAdjacentVertices);
                    continue;
                }

                if (IsFinished(startVertex)) return true;
                GoBack();
            }

            return false;
        }

        private void GoForward(IReadOnlyList<IntVector2> remainingAdjacentVertices)
        {
            var vertex = Random
                             ? remainingAdjacentVertices.OrderBy(x => Guid.NewGuid()).First()
                             : remainingAdjacentVertices[0];
            if (!_path.Contains(vertex)) _path.Add(vertex);
            if (!IsEveryRemainingVertexReachable(vertex)) GoBack();
        }

        private void GoBack()
        {
            _triedPaths.Add(_path.Clone());
            _path.RemoveTop();
        }

        #region Helper Methods

        /// <summary>
        ///     Checks whether the algorithm has finished.
        ///     Zhe algorithm has finished if all vertices are in the path and the startVertex is a neighbor of the last Vertex.
        /// </summary>
        /// <param name="startVertex"></param>
        /// <returns>If The Algorithm has finished.</returns>
        private bool IsFinished(IntVector2 startVertex)
        {
            return _vertices.Length == _path.Length &&
                   GetAdjacentVertices(_path.Top(), onlyUnMarked: false).Contains(startVertex);
        }

        private bool IsMarked(IntVector2 vertex) { return _path.Contains(vertex); }

        private bool IsEveryRemainingVertexReachable(IntVector2 startVertex)
        {
            var marked = new List<IntVector2>(_vertices.Length - _path.Length);
            marked.AddRange(_vertices.Where(IsMarked));

            var neighbors = _vertices.Where(v => startVertex.IsAdjacent(v) && !marked.Contains(v)).ToList();
            while (neighbors.Any())
            {
                marked.AddRange(neighbors);
                var tempNeighbors = new List<IntVector2>();
                foreach (var neighbor in neighbors)
                    tempNeighbors.AddRange(_vertices
                                               .Where(v =>
                                                          !marked.Contains(v) &&
                                                          neighbors.Any(n => n.IsAdjacent(v))));

                neighbors = tempNeighbors;
            }


            return marked.Count == _vertices.Length - _path.Length;
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

        #endregion
    }
}