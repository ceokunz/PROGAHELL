using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace laba_2
{
    public class CLifetimeChanger : CCollectable
    {
        private readonly double bonus;

        public CLifetimeChanger(Point position, double size, double lifetime, double bonus = 1.0)
            : base(position, size, lifetime)
        {
            this.bonus = bonus;
            sprite.Fill = Brushes.Yellow;
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
