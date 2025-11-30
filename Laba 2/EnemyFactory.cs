using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public static class EnemyFactory
    {
        public static IEnemy CreateEnemy(CEnemyTemplate template)
        {
            IconItem icon = new IconItem(template.IconPath);
            object[] args;
            Type enemyType;

            if (template is ArmoredEnemy arm)
            {
                enemyType = typeof(ArmoredEnemy);
                args = new object[] { template.Name, template.MaxHitpoints, template.GoldReward, template.MaxHitpoints.Clone(), false, icon, arm.ArmorReduction };
            }
            else if (template is HealingEnemy heal)
            {
                enemyType = typeof(HealingEnemy);
                args = new object[] { template.Name, template.MaxHitpoints, template.GoldReward, template.MaxHitpoints.Clone(), false, icon, heal.HealChancePercent, heal.HealAmount };
            }
            else if (template is YkorachEnemy ykor)
            {
                enemyType = typeof(YkorachEnemy);
                args = new object[] { template.Name, template.MaxHitpoints, template.GoldReward, template.MaxHitpoints.Clone(), false, icon, ykor.YkorachSize };
            }
            else
            {
                enemyType = typeof(AverageEnemy);
                args = new object[] { template.Name, template.MaxHitpoints, template.GoldReward, template.MaxHitpoints.Clone(), false, icon };
            }

            return (IEnemy)Activator.CreateInstance(enemyType, args);
        }
    }
}
