using System.Collections.Generic;
using System.Linq;

namespace SnakeAI_Hamiltonian
{
    public readonly struct Path
    {
        private readonly List<IntVector2> _vertices;

        public Path(IReadOnlyCollection<IntVector2> vertices = null)
        {
            _vertices = vertices == null ? new List<IntVector2>() : vertices.ToList();
        }

        public Path(int length = 0, IReadOnlyCollection<IntVector2> vertices = null)
        {
            _vertices = vertices == null ? new List<IntVector2>(length) : vertices.ToList();
        }

        #region Helper Methods

        public int Length => _vertices.Count;
        public void Add(IntVector2 vector2) { _vertices.Add(vector2); }
        public void RemoveTop() { _vertices.RemoveAt(_vertices.Count - 1); }
        public IntVector2 Top() { return _vertices.Last(); }
        public bool IsEmpty() { return !_vertices.Any(); }
        public bool Contains(IntVector2 vertex) { return _vertices.Contains(vertex); }
        public Path Clone() { return new Path(_vertices.ToList()); }
        public IEnumerable<IntVector2> ToList() { return _vertices.ToList(); }

        #endregion

        #region Overrides

        public override string ToString() { return string.Join(", ", _vertices); }
        public override bool Equals(object? obj) { return base.Equals(obj); }

        public bool Equals(Path other)
        {
            return _vertices.Count == other._vertices.Count &&
                   _vertices.Select((v, i) => new {v, i})
                            .All(pos => other._vertices[pos.i].Equals(pos.v));
        }

        public override int GetHashCode() { return _vertices != null ? _vertices.GetHashCode() : 0; }

        #endregion
    }
}