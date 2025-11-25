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

        public override double GetPointsValue() => 0;
        public override bool IsMouseOnObject(Point mousePosition)
        {
            double dx = mousePosition.X - position.X;
            double dy = mousePosition.Y - position.Y;
            double radius = size.Width / 2;
            return dx * dx + dy * dy <= radius * radius;
        }

        public override bool OnClick(Player player, Point mousePosition)
        {
            if (!IsMouseOnObject(mousePosition)) return false;
            return true;
        }
    }

}
