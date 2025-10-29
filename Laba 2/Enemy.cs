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
            set { name = value; }
        }

        public BigNumber MaxHitpoints
        {
            get { return maxHitpoints; }
            set { maxHitpoints = value; }
        }

        public BigNumber GoldReward
        {
            get { return goldReward; }
            set { goldReward = value; }
        }

        public BigNumber Hitpoints
        {
            get { return currentHitpoints; }
            set { currentHitpoints = value; }
        }

        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }

        public IconItem Icon
        {
            get { return icon; }
            set { icon = value; }
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

        public bool TakeDamage(BigNumber dmg, out BigNumber GoldReward)
        {
            //if (Player.Damage)
            //{

            //}

            GoldReward = new BigNumber("222");
            return true;
        }

        private void Die()
        {

        }
    }
}
