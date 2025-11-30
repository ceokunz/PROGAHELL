using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class ArmoredEnemy : Enemy
    {
        private int armorReduction; 
        public int ArmorReduction
        {
            get => armorReduction;
            set => armorReduction = Math.Max(1, value); //защита от деления на 0
        }

        public ArmoredEnemy(string name, BigNumber maxHitpoints, BigNumber goldReward,
            BigNumber currentHitpoints, bool isDead, IconItem icon, int armorReduction)
            : base(name, maxHitpoints, goldReward, currentHitpoints, isDead, icon)
        {
            ArmorReduction = armorReduction;
        }

        public override bool TakeDamage(BigNumber dmg, out BigNumber goldReward)
        {
            goldReward = new BigNumber("0");
            if (IsDead) return false;

           
            BigNumber reducedDmg = dmg.Divide(ArmorReduction); //целочисленное деление

            if (reducedDmg.CompareTo(CurrentHitpoints) >= 0)
            {
                CurrentHitpoints = new BigNumber("0");
                IsDead = true;
                goldReward = GoldReward;
                return true;
            }
            else
            {
                CurrentHitpoints = CurrentHitpoints.Subtract(reducedDmg);
                return false;
            }
        }
    }
    
}
