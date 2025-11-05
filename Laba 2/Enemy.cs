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
            get { return name; }
            private set { name = value; }
        }

        public BigNumber MaxHitpoints
        {
            get { return maxHitpoints;}
            private set { maxHitpoints = value; }
        }

        public BigNumber GoldReward
        {
            get { return goldReward;}
            private set { goldReward = value; }
        }

        public BigNumber CurrentHitpoints
        {
            get { return currentHitpoints;}
            private set { currentHitpoints = value; }
        }

        public bool IsDead
        {
            get { return isDead;}
            private set { isDead = value;}
        }

        public IconItem Icon
        {
            get{ return icon;}
            private set { icon = value;}
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
