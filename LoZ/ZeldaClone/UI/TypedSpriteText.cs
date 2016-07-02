using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaClone.UI
{
    public class TypedSpriteText
    {
        public const int FontSize = 16;
        
        public TypedSpriteText(string text, Vector2 position, SoundEffect sound, Texture2D texture)
        {
            _texture = texture;
            _sound = sound;
            _position = position;
            _charSet = new SpriteCharSet(text, FontSize, new NesFontMap());
            TypeDelay = DefaultTypeDelay;

            Reset();
        }

        public double TypeDelay { get; set; }

        public bool Enabled { get; set; }

        public void Reset()
        {
            _charIndex = 0;
            Enabled = true;
            _counter = TypeDelay;
        }

        public void Update(GameTime time)
        {
            if (Enabled)
            {
                _counter += time.ElapsedGameTime.TotalSeconds;
                if (_counter >= TypeDelay)
                {
                    _sound.Play();
                    _counter = 0.0;
                    _charIndex++;
                    if (_charIndex >= _charSet.Count)
                    {
                        _charIndex = _charSet.Count;
                        Enabled = false;
                    }
                }
            }
        }

        public void DrawString(SpriteBatch sb, Color color)
        {
            _cursor = new FontCursor(_position, FontSize);
            for (int i = 0; i < _charIndex; i++)
            {
                var letter = _charSet[i];
                if (letter == Rectangle.Empty)
                {
                    _cursor.NewLine();
                }
                else
                {
                    sb.Draw(_texture, _cursor.Cursor, letter, color);
                    _cursor.MoveRight();
                }
            }    
        }

        private int _charIndex;
        private double _counter;

        private FontCursor _cursor; 
        private readonly SpriteCharSet _charSet;
        private readonly SoundEffect _sound;
        private readonly Vector2 _position;
        private readonly Texture2D _texture;

        private const double DefaultTypeDelay = 0.1;
    }
}