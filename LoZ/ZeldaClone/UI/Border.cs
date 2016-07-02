// Border.cs - 22/11/2015
// Eric Policaro

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaClone.UI
{
    public class Border
    {
        public Border(BorderView view, int gridSize, int width, int height)
        {
            _view = view;
            _gridSize = gridSize;
            _width = width;
            _height = height;
        }

        public void Draw(SpriteBatch sb, Vector2 position)
        {
            for (int y = 0; y < _height; y++)
            {
                var nextPos = new Vector2
                {
                    X = position.X,
                    Y = position.Y + (y * _gridSize)
                };
                for (int x = 0; x < _width; x++)
                {
                    nextPos.X = position.X + (x * _gridSize);
                    if (x == 0 && y == 0)
                    {
                        sb.Draw(_view.TopLeft, nextPos, Color.White);
                    }
                    else if (x == 0 && y == (_height - 1))
                    {
                        sb.Draw(_view.BottomLeft, nextPos, Color.White);
                    }
                    else if (x == (_width - 1) && y == 0)
                    {
                        sb.Draw(_view.TopRight, nextPos, Color.White);
                    }
                    else if (x == (_width - 1) && y == (_height - 1))
                    {
                        sb.Draw(_view.BottomRight, nextPos, Color.White);
                    }
                    else if (x == 0 || x == (_width - 1))
                    {
                        sb.Draw(_view.Vertical, nextPos, Color.White);
                    }
                    else if (y == 0 || y == (_height - 1))
                    {
                        sb.Draw(_view.Horizontal, nextPos, Color.White);
                    }
                }
            }
        }

        private readonly int _height;
        private readonly int _width;
        private readonly BorderView _view;
        private readonly int _gridSize;
    }

    public class BorderView
    {
        public Texture2D TopLeft { get; set; }
        public Texture2D TopRight { get; set; }
        public Texture2D BottomLeft { get; set; }
        public Texture2D BottomRight { get; set; }
        public Texture2D Vertical { get; set; }
        public Texture2D Horizontal { get; set; }
    }
}