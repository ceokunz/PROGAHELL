using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public interface IEnemy
    {
        public interface IEnemy
        {
            string Name { get; }
            BigNumber BaseLife { get; }
            BigNumber Gold { get; }
            void TakeDamage(BigNumber damage);
        }
    }
}
