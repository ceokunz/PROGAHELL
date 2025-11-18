using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class CLifetimeChanger : CCollectable
    {
        private double lifetimeModifier;

        public CCollectable(Point position, double size, double lifetime, double speedModifier)
        {

        }

        public bool onClick(CPlayer player, CController controller, Point mousePosition)
        {
            return false;
        }
    }
}
