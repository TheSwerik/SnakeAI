using System.Collections.Generic;
using System.Linq;

namespace SnakeAI_Hamiltonian
{
    public static class ExtensionMethods
    {
        public static bool AnyMatch(this IEnumerable<List<IntVector2>> paths, List<IntVector2> path)
        {
            return paths.Any(p => p.Count == path.Count &&
                                  p.Select((v, i) => new {v, i})
                                   .All(pos => path[pos.i].Equals(pos.v)));
        }
    }
}