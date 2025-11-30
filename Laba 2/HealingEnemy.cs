using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class Healing : Enemy
    {
        private int healChancePercent; 
        private BigNumber healAmount;//фиксированное восстановление

        public int HealChancePercent
        {
            get => healChancePercent;
            set => healChancePercent = Math.Clamp(value, 0, 100);
        }

        public BigNumber HealAmount
        {
            get => healAmount;
            set => healAmount = value ?? throw new ArgumentNullException();
        }

        public Healing(string name, BigNumber maxHitpoints, BigNumber goldReward,
            BigNumber currentHitpoints, bool isDead, IconItem icon,
            int healChancePercent, BigNumber healAmount)
            : base(name, maxHitpoints, goldReward, currentHitpoints, isDead, icon)
        {
            HealChancePercent = healChancePercent;
            HealAmount = healAmount;
        }

        public override bool TakeDamage(BigNumber dmg, out BigNumber goldReward)
        {
            goldReward = new BigNumber("0");
            if (IsDead) return false;

            //сначала получаем урон
            if (dmg.CompareTo(CurrentHitpoints) >= 0)
            {
                //умираем!!! самолечение не срабатывает при смерти!!!
                CurrentHitpoints = new BigNumber("0");
                IsDead = true;
                goldReward = GoldReward;
                return true;
            }
            else
            {
                CurrentHitpoints = CurrentHitpoints.Subtract(dmg);

                //попытка самолечения :(((((
                if (Random.Shared.Next(100) < HealChancePercent)
                {
                    //восстанавливаем HP, но не больше максимума
                    var newHP = CurrentHitpoints.Add(HealAmount);
                    if (newHP.CompareTo(MaxHitpoints) > 0)
                        CurrentHitpoints = MaxHitpoints;
                    else
                        CurrentHitpoints = newHP;
                }

                return false;
            }
        }
    }
    
}
