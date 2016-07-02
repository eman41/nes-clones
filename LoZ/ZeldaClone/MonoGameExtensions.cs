// MonoGameExtensions.cs - 19/11/2015
// Eric Policaro

using Microsoft.Xna.Framework;
using System;

namespace ZeldaClone
{
    public static class MonoGameExtensions
    {
        public static bool IsCloseTo(this Vector2 me, Vector2 other, float threshold = float.Epsilon)
        {
            Vector2 delta = me - other;
            return (Math.Abs(delta.X) <= Math.Abs(threshold) &&
                    Math.Abs(delta.Y) <= Math.Abs(threshold));
        }
    }
}