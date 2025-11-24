using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace laba_2
{
    public class CSpawnRateChanger : CCollectable
    {
        private readonly double reduction;

        public CSpawnRateChanger(Point position, double size, double lifetime, double reduction = 0.3)
            : base(position, size, lifetime)
        {
            this.reduction = reduction;
            sprite.Fill = Brushes.HotPink;
        }

        public override bool onClick(CPlayer player, CController controller, Point mousePosition)
        {
            if (!isMouseOnObject(mousePosition)) return false;
            controller.DecreaseSpawnRate(reduction);
            return true;
        }
    }
}
