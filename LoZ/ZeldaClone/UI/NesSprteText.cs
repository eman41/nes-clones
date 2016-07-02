// NesSprteText.cs - 22/11/2015
// Eric Policaro

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaClone.UI
{
    public class NesSprteText
    {
        public const int FontSize = 16;
        
        public NesSprteText(Texture2D texture)
        {
            _texture = texture;
        }

        public void DrawString(SpriteBatch sb, Vector2 position, string text, Color color)
        {
            var charSet = new SpriteCharSet(text, FontSize, new NesFontMap());
            var cursor = new FontCursor(position, FontSize);

            foreach(var letter in charSet)
            {
                if (letter == Rectangle.Empty)
                {
                    cursor.NewLine();
                }
                else
                {
                    sb.Draw(_texture, cursor.Cursor, letter, color);
                    cursor.MoveRight();
                }
            }
        }

        private readonly Texture2D _texture;
    }
}