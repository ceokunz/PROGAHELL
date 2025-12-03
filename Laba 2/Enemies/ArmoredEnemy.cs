using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
     class ArmoredEnemy : Enemy
    {
        private int armor;
        public ArmoredEnemy(string Name, BigNumber MaxHitpoints, BigNumber GoldReward, BigNumber CurrentHitpoints, bool IsDead, IconItem Icon, int armor) : base(Name, MaxHitpoints, GoldReward, CurrentHitpoints, IsDead, Icon)
        {
            this.armor = armor;
        }

        public override bool TakeDamage(BigNumber dmg, out BigNumber goldReward)
        {

            goldReward = new BigNumber("0");
            if (IsDead) return false;

            long effectivePercent = 100 - armor;
            BigNumber reducedDmg = dmg.Multiply(effectivePercent).Divide(100);

            if (reducedDmg >= CurrentHitpoints)
            {
                // УМИРАЕМ =)))
                CurrentHitpoints = new BigNumber("0");
                IsDead = true;
                goldReward = this.GoldReward;
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
