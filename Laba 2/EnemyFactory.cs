using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class EnemyFactory // ЭТО ИЩЗДЕВАТЕЛЬСТВО НАД ЛЮДЬМИ =)
    {
        public static Enemy CreateEnemy(CEnemyTemplate template, BigNumber finalLife, BigNumber finalGold)
        {
            if (template.Type == ETypes.Armored)
            {
                int armor = 50;
                return new ArmoredEnemy(template.Name, finalLife, finalGold, template.MaxHitpoints, false, template.Icon, armor);
            }

            if (template.Type == ETypes.Ykorach)
            {
                double ykorMod = 0.5;
                return new YkorachEnemy(template.Name, finalLife, finalGold, template.MaxHitpoints, false, template.Icon, ykorMod);
            }

            if (template.Type == ETypes.Healing)
            {
                double healChan = 0.2;
                double healPerc = 0.3;
                
                return new HealingEnemy(template.Name, finalLife, finalGold, template.MaxHitpoints, false, template.Icon, healChan, healPerc);
            }

            return new NormalEnemy(template.Name, finalLife, finalGold, template.MaxHitpoints, false, template.Icon);
        }
    }
}
