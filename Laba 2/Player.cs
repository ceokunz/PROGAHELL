using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class Player : INotifyPropertyChanged
    {
        int lvl;
        BigNumber gold;
        BigNumber damage;
        long damageModifier;
        BigNumber upgradeCost;
        double upgradeModifier;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Lvl
        {
            get { return lvl; }
            private set 
            {
                if (lvl != value)
                {
                    lvl = value;
                    OnPropertyChanged(nameof(Lvl));
                }
            }    
        }
        public BigNumber Gold
        {
            get { return gold; }
            private set 
            {
                if (gold == null || !gold.Equals(value))
                {
                    gold = value;
                    OnPropertyChanged(nameof(Gold));
                }
            }
        }

        public BigNumber Damage
        {
            get { return damage; }
            private set 
            {
                if (damage == null || !damage.Equals(value))
                {
                    damage = value;
                    OnPropertyChanged(nameof(Damage));
                }
            }
        }
        public long DamageModifier
        {
            get { return damageModifier; }
            private set { damageModifier = value; }
        }
        public BigNumber UpgradeCost
        {
            get {  return upgradeCost; }
            private set 
            {
                if (upgradeCost == null || !upgradeCost.Equals(value))
                {
                    upgradeCost = value;
                    OnPropertyChanged(nameof(UpgradeCost));
                }
            }
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
            Gold = Gold.Add(amount);
            return true;
        }

        public bool TryUpgrade()
        {
            if (Gold.CompareTo(UpgradeCost) < 0)
                return false;

            Gold = Gold.Subtract(UpgradeCost);

            Damage = Damage.Multiply(DamageModifier);

            Lvl++;

            BigNumber multiplier = new BigNumber(UpgradeModifier.ToString("F0"));

            long nextMult = (long)Math.Round(UpgradeModifier * Lvl);
            UpgradeCost = UpgradeCost.Multiply(nextMult);

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
        public void ResetToDefault()
        {
            Lvl = 1;
            Gold = new BigNumber("0");
            Damage = new BigNumber("1");

            UpgradeCost = new BigNumber("10");
        }
    }
}
