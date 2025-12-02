using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class EnemyFactory // ЭТО ИЩЗДЕВАТЕЛЬСТВО НАД ЛЮДЬМИ =)
    {
        public static IEnemy CreateEnemy(CEnemyTemplate template, BigNumber finalLife, BigNumber finalGold)
        {
            if (template is CArmoredEnemyTemplate)
            {
                var armoredTemplate = template as CArmoredEnemyTemplate;
                BigNumber armor = new BigNumber(armoredTemplate.Armor.ToString());
                return new ArmoredEnemy(template.Name, finalLife, finalGold, false, template.Icon, armor);
            }

            if (template is CYkorachEnemyTemplate)
            {
                var ykorTemplate = template as CYkorachEnemyTemplate;
                return new YkorachEnemy(template.Name, finalLife, finalGold, ykorTemplate.Ykor);
            }

            if (template is CHealingEnemyTemplate)
            {
                var healingTemplate = template as CHealingEnemyTemplate;
                return new HealingEnemy(template.Name, finalLife, finalGold, healingTemplate.HealChance, healingTemplate.HealPercentage);
            }

            return new NormalEnemy(template.Name, finalLife, finalGold);
        }
    }
}
