using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class HealingEnemy : Enemy
    {
        private double healChance;
        private double healPercentage;
        private Random rng = new Random();

        public HealingEnemy(string Name, BigNumber MaxHitpoints, BigNumber GoldReward, BigNumber CurrentHitpoints, bool IsDead, IconItem Icon, double healChance, double healPercentage) : base(Name, MaxHitpoints, GoldReward, CurrentHitpoints, IsDead, Icon)
        {
            this.healChance = healChance;
            this.healPercentage = healPercentage;
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
                OnDefeated();
                return true;
            }
            else
            {
                CurrentHitpoints = CurrentHitpoints.Subtract(dmg);
                OnDamaged(dmg);
            }
            if (rng.NextDouble() < healChance)
            {
                long percent = (long)(healPercentage * 1000);
                BigNumber healAmount = MaxHitpoints.Multiply(percent).Divide(1000);

                CurrentHitpoints = CurrentHitpoints.Add(healAmount);

                if (CurrentHitpoints > MaxHitpoints)
                    CurrentHitpoints = MaxHitpoints;
            }

            return false;
        }
    }
}
