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

        private double baseClickCooldown = 0.5;
        private double currentCooldown;
        private double cooldownTimer;

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

        public double RemainingCooldown => Math.Max(0, cooldownTimer);


        public Player(int Lvl, BigNumber Gold, BigNumber Damage, long DamageModifier, BigNumber UpgradeCost, double UpgradeModifier, double BaseClickCooldown)
        {
            lvl = 1;
            gold = Gold;
            damage = Damage;
            damageModifier = DamageModifier;
            upgradeCost = UpgradeCost;
            upgradeModifier = UpgradeModifier;
            baseClickCooldown = BaseClickCooldown;
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


            long nextMult = (long)Math.Round(UpgradeModifier * Lvl);
            UpgradeCost = UpgradeCost.Multiply(nextMult);

            return true;
        }

        public BigNumber DealDamage(Enemy enemy)
        {
            return Damage;
        }

        public void ResetToDefault()
        {
            Lvl = 1;
            Gold = new BigNumber("0");
            Damage = new BigNumber("1");
            UpgradeCost = new BigNumber("10");

            // Сброс кулдауна
            currentCooldown = baseClickCooldown;
            cooldownTimer = 0;
        }
        public void Update(double delta)
        {
            if (cooldownTimer > 0)
                cooldownTimer -= delta;
        }

        public bool CanClick() => cooldownTimer <= 0;

        public void PerformClick()
        {
            if (CanClick())
            {
                cooldownTimer = currentCooldown;
            }
        }

        public void IncreaseClickSpeed(double reduction)
        {
            currentCooldown = Math.Max(0.1, currentCooldown - reduction);
        }
        
    }
}
