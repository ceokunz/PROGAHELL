using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    internal class CHealingEnemyTemplate: CEnemyTemplate
    {
        private double healChance;
        private double healPercentage;

        public double HealChance
        {
            get { return healChance; }
            set { if (value >= 0 && value <= 1.0) healChance = value; else healChance = 0.1; }
        }

        public double HealPercentage
        {
            get { return healPercentage; }
            set { if (value >= 0 && value <= 1.0) healPercentage = value; else healPercentage = 0.05; }
        }

        public CHealingEnemyTemplate(string name, string maxHitpoints, string goldReward, double spawnChance, string iconPath, double healChance, double healPercentage) : base(name, maxHitpoints, goldReward, spawnChance, iconPath)
        {

            this.HealChance = healChance;
            this.HealPercentage = healPercentage;
        }

    }
}

