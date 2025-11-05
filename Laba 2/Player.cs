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
        long damageModifier;
        BigNumber upgradeCost;
        double upgradeModifier;

        public int Lvl
        {
            get { return lvl; }
            private set { lvl = value; }    
        }
        public BigNumber Gold
        {
            get { return gold; }
            private set { gold = value; }
        }

        public BigNumber Damage
        {
            get { return damage; }
            private set {  damage = value; }
        }
        public long DamageModifier
        {
            get { return damageModifier; }
            private set { damageModifier = value; }
        }
        public BigNumber UpgradeCost
        {
            get {  return upgradeCost; }
            private set { upgradeCost = value; }
        }
        public double UpgradeModifier
        {
            get {  return upgradeModifier; }
            private set { upgradeModifier = value; }    
        }

        public Player(int Lvl, BigNumber Gold, BigNumber Damage, long DamageModifier, BigNumber UpgradeCost, double UpgradeModifier)
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
            gold = gold.Add(amount);
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

            long nextMult = (long)Math.Round(upgradeModifier * lvl);
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
