using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace laba_2
{
    public class CGoldGiver : CCollectable
    {
        private BigNumber goldValue;

        public CGoldGiver(Point position, double size, double lifetime)
            : base(position, size, lifetime)
        {
            sprite.Fill = Brushes.BlueViolet;
            goldValue = new BigNumber(Math.Max(1.0, (1.0 / size) / lifetime * 1000.0).ToString("F0"));
        }

        //public override double GetPointsValue() => goldValue.ToDouble();

        public override bool IsMouseOnObject(Point mousePosition)
        {
            double dx = mousePosition.X - (position.X);
            double dy = mousePosition.Y - (position.Y);
            double radius = size.Width / 2;
            return dx * dx + dy * dy <= radius * radius;
        }

        public override bool OnClick(Player player, CController controller, Point mousePosition)
        {
            if (!IsMouseOnObject(mousePosition)) 
                return false;

            player.AddGold(goldValue);
            return true;
        }
    }
}
