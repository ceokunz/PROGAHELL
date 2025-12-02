using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public abstract class Enemy : INotifyPropertyChanged
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
            protected set { name = value; }
        }

        public BigNumber MaxHitpoints
        {
            get { return maxHitpoints;}
            protected set
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
            protected set { goldReward = value; }
        }

        public BigNumber CurrentHitpoints
        {
            get { return currentHitpoints;}
            protected set 
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
            protected set { isDead = value;}
        }

        public IconItem Icon
        {
            get{ return icon;}
            protected set { icon = value;}
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

        public abstract bool TakeDamage(BigNumber dmg, out BigNumber goldReward);
    }
}
