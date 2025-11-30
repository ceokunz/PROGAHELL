using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class AverageEnemy : CEnemyTemplate
    {
        public AverageEnemy(string name, string maxHitpoints, string goldReward, double spawnChance, string iconPath) : base(name, maxHitpoints, goldReward, spawnChance, iconPath) 
        { 

        }
    }
}

