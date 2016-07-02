// PlayerData.cs 20 11 2015

using System.Collections.Generic;

namespace ZeldaClone
{
    public class PlayerData
    {
        public int HeartContainers { get; set; }

        public int RemainingHealth { get; set; }

        public int Rupees { get; set; }

        public int Keys { get; set; }

        public int Bombs { get; set; }

        public int ItemEquipped { get; set; }

        public int EquippedPowerLevel
        {
            get { return Items[ItemEquipped]; }
        }

        public int SwordLevel
        {
            get { return Items[15]; }
        }

        public List<int> Items = new List<int>
        {
            2, 0, 0, 0, 
            0, 0, 0, 0, 
            0, 0, 0, 0, 
            0, 0, 0, 3
        };

        public List<bool> Triforce = new List<bool>
        {
            false, false, false, false, 
            false, false, false, false
        };
    }
}