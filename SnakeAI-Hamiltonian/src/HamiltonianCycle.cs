﻿using System;
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
            _startVertex = _vertices.First(v => v.X == 2 && v.Y == 1);
            Console.WriteLine(_startVertex);
            if (IterativePathCalculation(_startVertex)) return _path.ToList();
            throw new CouldNotFindCycleException();
        }

        private bool IterativePathCalculation(IntVector2 startVertex)
        {
            var vertex = startVertex;
            var tried = new List<List<IntVector2>>();
            while (true)
            {
                if (!_path.Contains(vertex)) _path.Add(vertex);
                var remainingVertices = GetAdjacentPositions(vertex, tried).ToList();

                if (remainingVertices.Count > 0)
                {
                    // Go a step further
                    vertex = Random ? remainingVertices.OrderBy(x => Guid.NewGuid()).First() : remainingVertices[0];
                    continue;
                }

                if (_vertices.All(IsMarked) && GetAdjacentPositions(vertex, onlyUnMarked: false).Contains(startVertex))
                    return true; // Finished

                // Go a back:
                tried.Add(_path.ToList());
                _path.RemoveTop();
                if (!_path.Any()) return false;
                vertex = _path.Top();
            }
        }

        private IEnumerable<IntVector2> GetAdjacentPositions(IntVector2 vertex,
                                                             ICollection<List<IntVector2>> paths = null,
                                                             bool onlyUnMarked = true)
        {
            return _vertices.Where(v => !(onlyUnMarked && IsMarked(v)) && vertex.IsAdjacent(v));
        }

        private bool IsMarked(IntVector2 vertex) { return _path.Contains(vertex); }
    }
}