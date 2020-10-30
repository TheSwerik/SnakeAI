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

        public void AddVertex(IntVector2 vector2) { _vertices.Add(vector2); }
        public void RemoveVertex() { _vertices.RemoveAt(_vertices.Count - 1); }
    }
}