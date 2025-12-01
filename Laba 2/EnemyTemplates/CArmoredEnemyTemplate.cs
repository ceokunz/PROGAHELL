using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class CArmoredEnemyTemplate : CEnemyTemplate
    {
        double armor;
        public double Armor
        {
            get { return armor; }
            set { if (value > 0) armor = value; else armor = 50; }
        }

        public CArmoredEnemyTemplate(string name, string maxHitpoints, string goldReward, double spawnChance, string iconPath, double armor) : base(name, maxHitpoints, goldReward, spawnChance, iconPath)
        {
            this.Armor = armor;
        }

    }
    
}
