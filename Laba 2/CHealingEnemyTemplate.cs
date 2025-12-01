using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    internal class CHealingEnemyTemplate: CEnemyTemplate
    {
        private int healChancePercent; // от 0 до 100
        private BigNumber healAmount;  // фиксированное восстановление

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

        public  bool TakeDamage(BigNumber dmg, out BigNumber goldReward)
        {
            goldReward = new BigNumber("0");
            if (IsDead) return false;

            // Сначала получаем урон
            if (dmg.CompareTo(CurrentHitpoints) >= 0)
            {
                // Умираем — самолечение не срабатывает при смерти
                CurrentHitpoints = new BigNumber("0");
                IsDead = true;
                goldReward = GoldReward;
                return true;
            }
            else
            {
                CurrentHitpoints = CurrentHitpoints.Subtract(dmg);

                // Попытка самолечения
                if (Random.Shared.Next(100) < HealChancePercent)
                {
                    // Восстанавливаем HP, но не больше максимума
                    var newHP = CurrentHitpoints.Add(HealAmount);
                    if (newHP.CompareTo(MaxHitpoints) > 0)
                        CurrentHitpoints = MaxHitpoints;
                    else
                        CurrentHitpoints = newHP;

                    // Обязательно вызвать OnPropertyChanged (он вызывается через сеттер)
                    // Т.к. мы присваиваем CurrentHitpoints = ..., сеттер уже вызывает OnPropertyChanged
                }

                return false;
            }
        }
    }
}
}
