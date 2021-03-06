﻿using System;

namespace SnakeAI_Hamiltonian
{
    public struct IntVector2
    {
        public static readonly IntVector2 Default = new IntVector2(-1, -1);

        public IntVector2(int x, int y)
        {
            X = x;
            Y = y;
        }

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

        #region Overrides

        public static IntVector2 operator +(IntVector2 a, IntVector2 b) { return new IntVector2(a.X + b.X, a.Y + b.Y); }

        public static IntVector2 operator -(IntVector2 a, IntVector2 b) { return new IntVector2(a.X - b.X, a.Y - b.Y); }

        public override string ToString() { return $"{{{X}, {Y}}}"; }
        public override bool Equals(object? obj) { return base.Equals(obj); }

        public bool Equals(IntVector2 other) { return X == other.X && Y == other.Y; }

        public override int GetHashCode() { return HashCode.Combine(X, Y); }

        #endregion
    }
}