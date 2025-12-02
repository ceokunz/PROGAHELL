using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public abstract class CEnemy : IEnemy
    {
        public string Name { get; protected set; }
        public BigNumber BaseLife { get; protected set; }
        public BigNumber CurrentLife { get; protected set; }
        public BigNumber Gold { get; protected set; }

        protected CEnemy(string name, BigNumber baseLife, BigNumber gold)
        {
            Name = name;
            BaseLife = baseLife;
            CurrentLife = baseLife.Clone();
            Gold = gold;
        }

        public virtual void TakeDamage(BigNumber damage)
        {
            if (damage.CompareTo(CurrentLife) >= 0)
            {
                CurrentLife = new BigNumber("0");
            }
            else
            {
                CurrentLife = CurrentLife.Subtract(damage);
            }
        }
    }
}
