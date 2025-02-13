
using System.Diagnostics.CodeAnalysis;

namespace Assignment_Session_2_LINQ
{
    internal class StringEqualityComparer<T> : IEqualityComparer<string>
    {
        public bool Equals(string? x, string? y)
        {
            var sortedX = new string(x?.OrderBy(C => C).ToArray());
            var sortedY = new string(y?.OrderBy(C => C).ToArray());
            return sortedX == sortedY;
        }

        public int GetHashCode([DisallowNull] string obj)
        {
            var sorted = new string(obj.OrderBy(C => C).ToArray());
            return HashCode.Combine(sorted);
        }
    }
}
 
