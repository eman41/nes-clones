// HeadsUpDisplay.cs - 22/11/2015
// Eric Policaro

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaClone.UI
{
    public class HeadsUpDisplay
    {
        public HeadsUpDisplay(BorderView borderView, HudView hudView, ItemView itemView, Texture2D fontTexture, int gridSize)
        {
            _itemView = itemView;
            _gridSize = gridSize;
            _buttonBorder = new Border(borderView, gridSize, 3, 4);
            _text = new NesSprteText(fontTexture);

            _rupeeBank = new NesBankNumber(_text, hudView.IconRupee);
            _keyBank = new NesBankNumber(_text, hudView.IconKey);
            _bombBank = new NesBankNumber(_text, hudView.IconBomb);

            PlayerData = new PlayerData();

            _hudView = hudView;

            _lifeColor = new Color
            {
                R = 181,
                G = 49,
                B = 32,
                A = 255
            };
            _mapPhColor = new Color
            {
                R = 102,
                G = 102,
                B = 102,
                A = 255
            };
            _mapMarkerPhColor = new Color
            {
                R = 136,
                G = 216,
                B = 0,
                A = 255
            };
        }

        public Vector2 Origin { get; set; }

        public PlayerData PlayerData { get; set; }

        public void Draw(SpriteBatch sb)
        {
            Vector2 mapPos = ConvertToPosition(2, 2);
            Rectangle mapRect = new Rectangle
            {
                X = (int) mapPos.X,
                Y = (int) mapPos.Y,
                Width = 128,
                Height = 64
            };
            sb.FillRectangle(mapRect, _mapPhColor);
            _rupeeBank.Draw(sb, ConvertToPosition(11, 2), PlayerData.Rupees, Color.White);
            _keyBank.Draw(sb, ConvertToPosition(11, 4), PlayerData.Keys, Color.White);
            _bombBank.Draw(sb, ConvertToPosition(11, 5), PlayerData.Bombs, Color.White);

            _buttonBorder.Draw(sb, ConvertToPosition(15, 2));
            _buttonBorder.Draw(sb, ConvertToPosition(18, 2));

            _text.DrawString(sb, ConvertToPosition(23, 2), "-LIFE-", _lifeColor);

            _text.DrawString(sb, ConvertToPosition(16, 2), "B", Color.White);
            _text.DrawString(sb, ConvertToPosition(19, 2), "A", Color.White);

            sb.Draw(_itemView.GetItem(PlayerData.ItemEquipped, PlayerData.EquippedPowerLevel),
                ConvertToPosition(16, 3));

            sb.Draw(_itemView.GetItem(15, PlayerData.SwordLevel),
                ConvertToPosition(19, 3));

            for (int j = 4; j <= 5; j++)
            {
                for (int i = 22; i <= 29; i++)
                {
                    sb.Draw(_hudView.HeartFull, ConvertToPosition(i, j), Color.White);
                }
            }
        }

        private Vector2 ConvertToPosition(int x, int y)
        {
            return new Vector2
            {
                X = Origin.X + (x * _gridSize),
                Y = Origin.Y + (y * _gridSize)
            };
        }

        private int _gridSize;
        private HudView _hudView;
        private Border _buttonBorder;
        private NesSprteText _text;

        private NesBankNumber _rupeeBank;
        private NesBankNumber _keyBank;
        private NesBankNumber _bombBank;

        private Color _lifeColor;
        private Color _mapPhColor;
        private Color _mapMarkerPhColor;
        private ItemView _itemView;
    }

    public class HudElement<T>
    {
        public Point Location { get; set; }

        public T Item { get; set; }
    }
}