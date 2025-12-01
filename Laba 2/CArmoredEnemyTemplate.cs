using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class CArmoredEnemyTemplate : CEnemyTemplate
    {
        double armor;
        public double Armor
        {
            get { return armor; }
            set { if (value > 0) armor = value; else armor = 25; }
        }
    }
    
}
