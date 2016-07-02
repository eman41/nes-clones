using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaClone.UI
{
    public class ItemView
    {
        public Dictionary<int, List<Texture2D>> ItemIcons { get; set; }

        public Texture2D GetItem(int itemIndex, int powerLevel)
        {
            List<Texture2D> textures;
            if (ItemIcons.TryGetValue(itemIndex, out textures))
            {
                int powerLevelIndex = powerLevel - 1;
                if (powerLevelIndex >= 0 && powerLevelIndex < textures.Count)
                {
                    return textures[powerLevelIndex];
                }
                else
                {
                    throw new ArgumentException(
                        string.Format("Invalid power level ({0}) for item {1}", powerLevel, itemIndex));
                }
            }

            throw new ArgumentException(string.Format("Invalid item index {0}", itemIndex));
        }
    }
}
