using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class YkorachEnemy : Enemy
    {
        private int ykorachSize;
        public int YkorachSize
        {
            get => ykorachSize;
            set
            {
                if (ykorachSize != value)
                {
                    ykorachSize = value;
                    OnPropertyChanged(nameof(YkorachSize));
                }
            }
        }

        public YkorachEnemy(string Name, BigNumber MaxHitpoints, BigNumber GoldReward, BigNumber CurrentHitpoints, bool IsDead, IconItem Icon, int YkorachSize) : base(Name, MaxHitpoints, GoldReward, CurrentHitpoints, IsDead, Icon)
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
                YkorachSize = Math.Max(0, YkorachSize - 1);
                return false;
            }
        }
    }
}
