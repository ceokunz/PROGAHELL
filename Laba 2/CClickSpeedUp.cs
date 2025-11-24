using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace laba_2
{
    public class CClickSpeedUp : CCollectable
    {
        private readonly double reduction;

        public CClickSpeedUp(Point position, double size, double lifetime, double reduction = 0.2)
            : base(position, size, lifetime)
        {
            this.reduction = reduction;
            sprite.Fill = Brushes.Red;
        }

        public override bool onClick(CPlayer player, CController controller, Point mousePosition)
        {
            if (!isMouseOnObject(mousePosition)) return false;
            player.IncreaseClickSpeed(reduction);
            return true;
        }
    }
}
