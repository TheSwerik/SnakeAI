using System;

namespace SnakeAI_Hamiltonian.Structs
{
    public class IntVector2
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Area => X * Y;

        public bool IsAdjacent(IntVector2 b)
        {
            var path = (this - b).Abs();
            if (path.X == 0) return path.Y == 1;
            if (path.Y == 0) return path.X == 1;
            return false;
        }

        public IntVector2 Abs() { return new IntVector2(Math.Abs(X), Math.Abs(Y)); }

        #region Constructors

        public IntVector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public IntVector2(int size) { X = Y = size; }

        public IntVector2(Random random, int maxValue = int.MaxValue)
        {
            X = random.Next(maxValue);
            Y = random.Next(maxValue);
        }

        public IntVector2(Random random, IntVector2 size)
        {
            X = random.Next(size.X);
            Y = random.Next(size.Y);
        }

        #endregion

        #region Overrides

        public static IntVector2 operator +(IntVector2 a, IntVector2 b) { return new IntVector2(a.X + b.X, a.Y + b.Y); }

        public static IntVector2 operator -(IntVector2 a, IntVector2 b) { return new IntVector2(a.X - b.X, a.Y - b.Y); }

        public static bool operator ==(IntVector2 vector2, int i) { return vector2.X == i && vector2.Y == i; }

        public static bool operator !=(IntVector2 vector2, int i) { return vector2.X != i && vector2.Y != i; }

        public override string ToString() { return $"{{{X}, {Y}}}"; }

        private bool Equals(IntVector2 other) { return X == other.X && Y == other.Y; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((IntVector2) obj);
        }

        public override int GetHashCode() { return HashCode.Combine(X, Y); }

        #endregion
    }
}