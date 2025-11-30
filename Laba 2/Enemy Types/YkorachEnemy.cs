using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class YkorachEnemy : CEnemyTemplate
    {
        public int YkorachSize { get; set; } = 100;

        public YkorachEnemy(string name, string maxHitpoints, string goldReward, double spawnChance, string iconPath, int ykorachSize) : base(name, maxHitpoints, goldReward, spawnChance, iconPath)
        {
            YkorachSize = ykorachSize;
        }
    }
}
