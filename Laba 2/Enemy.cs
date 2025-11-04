using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class Enemy
    {
        private string name;
        private BigNumber maxHitpoints;
        private BigNumber currentHitpoints;
        private BigNumber goldReward;
        private bool isDead;
        private IconItem icon;

        public string Name
        {
            get;
            private set;
        }

        public BigNumber MaxHitpoints
        {
            get;
            private set;
        }

        public BigNumber GoldReward
        {
            get;
            private set;
        }

        public BigNumber CurrentHitpoints
        {
            get;
            private set;
        }

        public bool IsDead
        {
            get;
            private set;
        }

        public IconItem Icon
        {
            get;
            private set;
        }


        public Enemy(string Name, BigNumber MaxHitpoints, BigNumber GoldReward,
            BigNumber CurrentHitpoints, bool IsDead, IconItem Icon)
        {
            name = Name;
            maxHitpoints = MaxHitpoints;
            goldReward = GoldReward;
            currentHitpoints = CurrentHitpoints;
            isDead = IsDead;
            icon = Icon;
        }

        public bool TakeDamage(BigNumber dmg, out BigNumber goldReward)
        {
            goldReward = new BigNumber("0");

            if (isDead) return false;

            currentHitpoints = currentHitpoints.Subtract(dmg);

            if (currentHitpoints.CompareTo(new BigNumber("0")) <= 0)
            {
                isDead = true;
                currentHitpoints = new BigNumber("0");
                goldReward = this.goldReward;
                return true;
            }

            return false;
        }

        private void Die()
        {
            //говно
        }
    }
}
