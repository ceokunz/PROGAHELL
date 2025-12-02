using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    internal class CYkorachEnemyTemplate : CEnemyTemplate
    {
        private double ykor;

        public double Ykor
        {
            get { return ykor; }
            set { if (value >= 0 && value <= 1.0) ykor = value; else ykor = 0.05; }
        }
        public CYkorachEnemyTemplate(string name, string maxHitpoints, string goldReward, double spawnChance, string iconPath, double ykor) : base(name, maxHitpoints, goldReward, spawnChance, iconPath)
        {
            this.Ykor = ykor;
        }
    }
}
