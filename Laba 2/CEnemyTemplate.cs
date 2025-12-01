using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public abstract class CEnemyTemplate
    {
        string name;
        public string Name
        {
            get { return name; }
            set { if (value.Length > 0) name = value; else name = "unknown"; }
        }
        BigNumber maxHitpoints;
        public BigNumber MaxHitpoints 
        { 
            get { return maxHitpoints; }
            set { if (maxHitpoints.CompareTo(value) > 0) maxHitpoints = value; else maxHitpoints = new BigNumber("1"); }
            
        }
        BigNumber goldReward;
        public BigNumber GoldReward
        {
            get { return goldReward; }
            set { if (goldReward.CompareTo(value) > 0) goldReward = value; else goldReward = new BigNumber("0"); } 
        }
        double spawnChance;
        public double SpawnChance
        {
            get { return spawnChance; }
            set { if (value > 0) spawnChance = value; else spawnChance = 1; }
        }
        public IconItem Icon { get; set; }
        public CEnemyTemplate(string name, string maxHitpoints, string goldReward,
                            double spawnChance, string iconPath)
        {
            Name = name;
            MaxHitpoints = new BigNumber(maxHitpoints);
            GoldReward = new BigNumber(goldReward);
            SpawnChance = spawnChance;
            Icon = new IconItem(iconPath);
        }

        public override string ToString()
        {
            return $"{Name} (HP: {MaxHitpoints}, Gold: {GoldReward}, Chance: {SpawnChance:P2})";
        }
    }
}
