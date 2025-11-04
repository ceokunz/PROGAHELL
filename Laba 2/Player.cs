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
            get;
            private set;
        }
        public BigNumber Gold
        {
            get;
            private set;
        }

        public BigNumber Damage
        {
            get;
            private set;
        }
        public double DamageModifier
        {
            get;
            private set;
        }
        public BigNumber UpgradeCost
        {
            get;
            private set;
        }
        public double UpgradeModifier
        {
            get;
            private set;
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
            if (gold.CompareTo(upgradeCost) < 0)
                return false;

            gold = gold.Subtract(upgradeCost);

            damage = damage.Multiply(damageModifier);

            lvl++;

            BigNumber multiplier = new BigNumber(upgradeModifier.ToString("F0"));

            double nextMult = upgradeModifier * lvl;
            upgradeCost = upgradeCost.Multiply(nextMult);

            return true;
        }

        public BigNumber DealDamage(Enemy enemy)
        {
            return null;
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
