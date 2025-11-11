using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class Enemy : INotifyPropertyChanged
    {
        private string name;
        private BigNumber maxHitpoints;
        private BigNumber currentHitpoints;
        private BigNumber goldReward;
        private bool isDead;
        private IconItem icon;

        //////////////////////////////////////////////////////////////////////////
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        public BigNumber MaxHitpoints
        {
            get { return maxHitpoints;}
            private set
            {
                 if (maxHitpoints != value)
                 {
                 maxHitpoints = value;
                 OnPropertyChanged(nameof(MaxHitpoints));
                 }
            }
        }

        public BigNumber GoldReward
        {
            get { return goldReward;}
            private set { goldReward = value; }
        }

        public BigNumber CurrentHitpoints
        {
            get { return currentHitpoints;}
            private set 
            {
                if (currentHitpoints != value)
                {
                    currentHitpoints = value;
                    OnPropertyChanged(nameof(CurrentHitpoints));
                }
            }
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

            if (IsDead) return false;

            if (dmg.CompareTo(CurrentHitpoints) >= 0)
            {
                // УМИРАЕМ =)))
                CurrentHitpoints = new BigNumber("0");
                IsDead = true;
                goldReward = this.GoldReward;
                return true;
            }
            else
            {
                CurrentHitpoints = CurrentHitpoints.Subtract(dmg);
                return false;
            }
        }

        private void Die()
        {
            //говно
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
