// NesBankNumber.cs - 22/11/2015
// Eric Policaro

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaClone.UI
{
    public class NesBankNumber
    {
        public NesBankNumber(NesSprteText font, Texture2D icon)
        {
            _font = font;
            _icon = icon;
        }

        public void Draw(SpriteBatch sb, Vector2 position, int number, Color color)
        {
            string toDraw = GetStringVersion(number);

            sb.Draw(_icon, position, Color.White);
            Vector2 labelPos = position + new Vector2(_icon.Width, 0);
            _font.DrawString(sb, labelPos, toDraw, color);
        }

        private string GetStringVersion(int number)
        {
            string s;
            if (!_cache.TryGetValue(number, out s))
            {
                string val = number.ToString();
                if (number < 100)
                {
                    val = "X" + val;
                }

                _cache[number] = val;
                s = val;
            }

            return s;
        }

        private readonly Dictionary<int, string> _cache = new Dictionary<int, string>();
        private readonly NesSprteText _font;
        private readonly Texture2D _icon;
    }
}