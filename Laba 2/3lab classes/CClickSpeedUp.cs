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

        public override double GetPointsValue() => 0;
        public override bool IsMouseOnObject(Point mousePosition)
        {
            double dx = mousePosition.X - position.X;
            double dy = mousePosition.Y - position.Y;
            double radius = size.Width / 2;
            return dx * dx + dy * dy <= radius * radius;
        }

        public override bool OnClick(CPlayer player, CController controller, Point mousePosition)
        {
            if (!IsMouseOnObject(mousePosition)) return false;
            player.IncreaseClickSpeed(reduction);
            return true;
        }
    }
}
