using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ZeldaClone.UI
{
    public class SpriteCharSet : IEnumerable<Rectangle>
    {
        public SpriteCharSet(string s, int charSize, IFontMap fontMap)
        {
            _fontMap = fontMap;
            _charSize = charSize;
            _chars = new List<Rectangle>();
            
            CalculateCharacters(s);
        }

        public Rectangle this[int i]
        {
            get { return _chars[i]; }
            set { _chars[i] = value; }
        }

        public int Count
        {
            get { return _chars.Count; }
        }

        private void CalculateCharacters(string s)
        {
            foreach (char c in s)
            {
                Rectangle charRect = Rectangle.Empty;
                if (c != '\n')
                {
                    charRect = RectFromMap(c);
                }

                _chars.Add(charRect);
            }
        }

        private Rectangle RectFromMap(char c)
        {
            Point charIndex = _fontMap.GetIndex(c);
            var absolute = new Vector2
            {
                X = charIndex.X * _charSize,
                Y = charIndex.Y * _charSize
            };

            return new Rectangle
            {
                X = (int)absolute.X,
                Y = (int)absolute.Y,
                Width = _charSize,
                Height = _charSize
            };
        }

        public IEnumerator<Rectangle> GetEnumerator()
        {
            return _chars.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _chars.GetEnumerator();
        }

        private readonly int _charSize;
        private readonly List<Rectangle> _chars;
        private readonly IFontMap _fontMap;
    }
}
