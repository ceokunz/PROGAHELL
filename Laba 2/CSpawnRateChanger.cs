using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace laba_2
{
    public class CSpawnRateChanger : CCollectable
    {
        private double SpeedModifier;
        public CSpawnRateChanger(Point position, double size, double lifetime, double speedModifier)
        {
        }
        public bool onClick (CPlayer player, CCollectable controller, Point mouseposition)
        {
        }
    }
    

}
