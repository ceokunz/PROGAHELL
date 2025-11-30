using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class HealingEnemy : CEnemyTemplate
    {
        public int HealChancePercent { get; set; } = 30;
        public BigNumber HealAmount { get; set; } = new BigNumber("1");

        public HealingEnemy(string name, string maxHitpoints, string goldReward, double spawnChance, string iconPath, int healChancePercent, string healAmount) : base(name, maxHitpoints, goldReward, spawnChance, iconPath)
        {
            HealChancePercent = Math.Clamp(healChancePercent, 0, 100);
            HealAmount = new BigNumber(healAmount);
        }
    }
    
}
