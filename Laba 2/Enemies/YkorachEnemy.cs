using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class YkorachEnemy : Enemy
    {
        private double ykorModifier;


        public YkorachEnemy(string Name, BigNumber MaxHitpoints, BigNumber GoldReward, BigNumber CurrentHitpoints, bool IsDead, IconItem Icon, double ykorModifier) : base(Name, MaxHitpoints, GoldReward, CurrentHitpoints, IsDead, Icon)
        {
            this.ykorModifier = ykorModifier;
        }

        public double ScaleFactor
        {
            get
            {
                if (MaxHitpoints.ToDouble() <= 0)
                    return 1.0;

                double current = CurrentHitpoints.ToDouble();
                double max = MaxHitpoints.ToDouble();

                double hpRatio = current / max; 
               
                double scale = 1.0 - (1.0 - hpRatio) * ykorModifier;

                return Math.Max(0.3, scale);
            }
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
