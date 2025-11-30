using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class ArmoredEnemy : CEnemyTemplate
    {
        public int ArmorReduction { get; set; } = 2;

        public ArmoredEnemy(string name, string maxHitpoints, string goldReward, double spawnChance, string iconPath, int armorReduction) : base(name, maxHitpoints, goldReward, spawnChance, iconPath)
        {
            ArmorReduction = Math.Max(1, armorReduction);
        }
    }
    
}
