using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class CEnemyTemplate
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
            set
            {
                maxHitpoints = value;
            }
                //if(maxHitpoints == null)
                //    maxHitpoints = new BigNumber("1");
                //if (maxHitpoints.CompareTo(value) > 0)
                //        maxHitpoints = value;                       
        }

        BigNumber goldReward;
        public BigNumber GoldReward
        {
            get { return goldReward; }
            set
            {
                goldReward = value;
            }
            //{
            //    if (goldReward == null)
            //        goldReward = new BigNumber("1");
            //    if (goldReward.CompareTo(value) > 0)
            //        goldReward = value;
            //}

        }
        double spawnChance;
        public double SpawnChance
        {
            get { return spawnChance; }
            set { if (value > 0) spawnChance = value; else spawnChance = 1; }
        }
        public IconItem Icon { get; set; }

        ETypes type;
        public ETypes Type
        {
            get { return type; }
        }
        public CEnemyTemplate(string name, string maxHitpoints, string goldReward,
                            double spawnChance, string iconPath, ETypes type = ETypes.None)
        {
            Name = name;
            MaxHitpoints = new BigNumber(maxHitpoints);
            GoldReward = new BigNumber(goldReward);
            SpawnChance = spawnChance;
            Icon = new IconItem(iconPath);

            this.type = type;
        }

        public override string ToString()
        {
            return $"{Name} (HP: {MaxHitpoints}, Gold: {GoldReward}, Chance: {SpawnChance:P2})";
        }
    }

    public enum ETypes
    {
        None = 0,
        Armored = 1,
        Ykorach = 2,
        Healing = 3
    }

}
