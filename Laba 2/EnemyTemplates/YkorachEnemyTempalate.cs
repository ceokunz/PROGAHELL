using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    internal class YkorachEnemyTempalate : CEnemyTemplate
    {
        private double shrink;

        public double Shrink
        {
            get { return shrink; }
            set { if (value >= 0 && value <= 1.0) shrink = value; else shrink = 0.05; }
        }
        public YkorachEnemyTempalate(string name, string maxHitpoints, string goldReward, double spawnChance, string iconPath, double shrink) : base(name, maxHitpoints, goldReward, spawnChance, iconPath)
        {
            this.Shrink = shrink;
        }
    }
}
