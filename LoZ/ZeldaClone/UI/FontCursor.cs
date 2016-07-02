using Microsoft.Xna.Framework;

namespace ZeldaClone.UI
{
    public class FontCursor
    {
        public FontCursor(Vector2 start, int charSize)
        {
            _start = start;
            x = (int)start.X;
            y = (int)start.Y;
            _charSize = charSize;
        }

        public Rectangle Cursor
        {
            get { return new Rectangle(x, y, _charSize, _charSize); }
        }

        public void NewLine()
        {
            x = (int)_start.X;
            MoveDown();
        }

        public void MoveLeft() { x -= _charSize; }
        public void MoveRight() { x += _charSize; }
        public void MoveUp() { y -= _charSize; }
        public void MoveDown() { y += _charSize; }

        private int x;
        private int y;

        private readonly int _charSize;
        private readonly Vector2 _start;
    }
}
