using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    internal class Ykorachenemy : Enemy
    {
        private int ykorachSize;
        public int YkorachSize 
        { 
            get { return ykorachSize; } 
            set { ykorachSize = value; } 
        }

        public Ykorachenemy(string Name, BigNumber MaxHitpoints, BigNumber GoldReward, BigNumber CurrentHitpoints, bool IsDead, IconItem Icon, int YkorachSize) : base(Name, MaxHitpoints, GoldReward, CurrentHitpoints, IsDead, Icon)
        {
             ykorachSize = YkorachSize;
        }

        public override bool TakeDamage(BigNumber dmg, out BigNumber goldReward)
        {
            goldReward = new BigNumber("0");

            if (IsDead) return false;

            if (dmg.CompareTo(CurrentHitpoints) >= 0)
            {
                // УМИРАЕМ =)))
                CurrentHitpoints = new BigNumber("0");
                IsDead = true;
                goldReward = this.GoldReward;
                return true;
            }
            else
            {
                CurrentHitpoints = CurrentHitpoints.Subtract(dmg);
                return false;
            }
        }
    }
}
