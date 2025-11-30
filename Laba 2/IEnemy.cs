using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public interface IEnemy
    {
        string Name { get; }
        BigNumber MaxHitpoints { get; }
        BigNumber CurrentHitpoints { get; }
        BigNumber GoldReward { get; }
        bool IsDead { get; }
        IconItem Icon { get; }

        bool TakeDamage(BigNumber dmg, out BigNumber reward);
    }
}
