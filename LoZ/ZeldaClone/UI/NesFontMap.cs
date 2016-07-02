using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ZeldaClone.UI
{
    public class NesFontMap : IFontMap
    {
        public NesFontMap()
        {
            var row0 = new Dictionary<char, Point>
            {
                {' ', new Point(0, 0) },
                {'!', new Point(1, 0) },
                {'"', new Point(2, 0) },
                {'#', new Point(3, 0) },
                {'$', new Point(4, 0) },
                {'%', new Point(5, 0) },
                {'&', new Point(6, 0) },
                {'\'', new Point(7, 0) },
                {'(', new Point(8, 0) },
                {')', new Point(9, 0) },
                {'*', new Point(10, 0) },
                {'+', new Point(11, 0) },
                {',', new Point(12, 0) },
                {'-', new Point(13, 0) },
                {'.', new Point(14, 0) },
                {'/', new Point(15, 0) },
            };

            var row1 = new Dictionary<char, Point>
            {
                {'0', new Point(0, 1) },
                {'1', new Point(1, 1) },
                {'2', new Point(2, 1) },
                {'3', new Point(3, 1) },
                {'4', new Point(4, 1) },
                {'5', new Point(5, 1) },
                {'6', new Point(6, 1) },
                {'7', new Point(7, 1) },
                {'8', new Point(8, 1) },
                {'9', new Point(9, 1) },
                {':', new Point(10, 1) },
                {';', new Point(11, 1) },
                {'<', new Point(12, 1) },
                {'=', new Point(13, 1) },
                {'>', new Point(14, 1) },
                {'?', new Point(15, 1) },
            };

            var row2 = new Dictionary<char, Point>
            {
                {'@', new Point(0, 2) },
                {'A', new Point(1, 2) },
                {'B', new Point(2, 2) },
                {'C', new Point(3, 2) },
                {'D', new Point(4, 2) },
                {'E', new Point(5, 2) },
                {'F', new Point(6, 2) },
                {'G', new Point(7, 2) },
                {'H', new Point(8, 2) },
                {'I', new Point(9, 2) },
                {'J', new Point(10, 2) },
                {'K', new Point(11, 2) },
                {'L', new Point(12, 2) },
                {'M', new Point(13, 2) },
                {'N', new Point(14, 2) },
                {'O', new Point(15, 2) },
            };

            var row3 = new Dictionary<char, Point>
            {
                {'P', new Point(0, 3) },
                {'Q', new Point(1, 3) },
                {'R', new Point(2, 3) },
                {'S', new Point(3, 3) },
                {'T', new Point(4, 3) },
                {'U', new Point(5, 3) },
                {'V', new Point(6, 3) },
                {'W', new Point(7, 3) },
                {'X', new Point(8, 3) },
                {'Y', new Point(9, 3) },
                {'Z', new Point(10, 3) },
                {'[', new Point(11, 3) },
                {'\\', new Point(12, 3) },
                {']', new Point(13, 3) },
                {'^', new Point(14, 3) },
                {'_', new Point(15, 3) },
            };

            var row4 = new Dictionary<char, Point>
            {
                {'`', new Point(0, 4) },
                {'a', new Point(1, 4) },
                {'b', new Point(2, 4) },
                {'c', new Point(3, 4) },
                {'d', new Point(4, 4) },
                {'e', new Point(5, 4) },
                {'f', new Point(6, 4) },
                {'g', new Point(7, 4) },
                {'h', new Point(8, 4) },
                {'i', new Point(9, 4) },
                {'j', new Point(10, 4) },
                {'k', new Point(11, 4) },
                {'l', new Point(12, 4) },
                {'m', new Point(13, 4) },
                {'n', new Point(14, 4) },
                {'o', new Point(15, 4) },
            };

            var row5 = new Dictionary<char, Point>
            {
                {'p', new Point(0, 5) },
                {'q', new Point(1, 5) },
                {'r', new Point(2, 5) },
                {'s', new Point(3, 5) },
                {'t', new Point(4, 5) },
                {'u', new Point(5, 5) },
                {'v', new Point(6, 5) },
                {'w', new Point(7, 5) },
                {'x', new Point(8, 5) },
                {'y', new Point(9, 5) },
                {'z', new Point(10, 5) },
                {'{', new Point(11, 5) },
                {'|', new Point(12, 5) },
                {'}', new Point(13, 5) },
                {'~', new Point(14, 5) },
                {'*', new Point(15, 5) },
            };

            _table = new [] { row0, row1, row2, row3, row4, row5 };
            _rowIndex = new Dictionary<char, int>();
            for (int i = 0; i < _table.Length; i++)
            {
                foreach(var column in _table[i])
                {
                    _rowIndex[column.Key] = i;
                }
            }
        }

        public Point GetIndex(char c)
        {
            int row = _rowIndex[c];
            return _table[row][c];
        }

        private readonly Dictionary<char, int> _rowIndex;
        private readonly Dictionary<char, Point>[] _table;
    }
}