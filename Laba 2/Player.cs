using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class Player
    {
        int lvl;
        BigNumber gold;
        BigNumber damage;
        double damageModifier;
        BigNumber upgradeCost;
        double upgradeModifier;

        public int Lvl
        {
            get { return lvl; }
            set { lvl = value; }
        }
        public BigNumber Gold
        {
            get { return gold; }
            set { gold = value; }
        }

        public BigNumber Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        public double DamageModifier
        {
            get { return damageModifier; }
            set { damageModifier = value; }
        }
        public BigNumber UpgradeCost
        {
            get { return upgradeCost; }
            set { upgradeCost = value; }
        }
        public double UpgradeModifier
        {
            get { return upgradeModifier; }
            set { upgradeModifier = value; }
        }

        public Player(int Lvl, BigNumber Gold, BigNumber Damage, double DamageModifier, BigNumber UpgradeCost, double UpgradeModifier)
        {
            lvl = 1;
            gold = Gold;
            damage = Damage;
            damageModifier = DamageModifier;
            upgradeCost = UpgradeCost;
            upgradeModifier = UpgradeModifier;
        }

        public bool AddGold(BigNumber amount)
        {
            gold.Add(amount);
            return true;
        }

        public bool TryUpgrade()
        {
            if ((gold > upgradeCost))
            {
                gold.Subtract(upgradeCost);
                lvl++;

                damage.Multiply(damageModifier);
                upgradeCost.Multiply(upgradeModifier);

                return true;
            }
            return false;
        }

        public BigNumber DealDamage(Enemy enemy)
        {
            return null;
            //enemy.TakeDamage(damage);
        }

        private void RecalculateStats()
        {

        }

        private BigNumber CalculateNextUpgradeCost()
        {
            return null;

        }

        private BigNumber CalculateTotalDamage()
        {
            throw new NotImplementedException();
        }

        private bool TrySpendGold(BigNumber amount)
        {
            throw new NotImplementedException();

        }
    }
}
