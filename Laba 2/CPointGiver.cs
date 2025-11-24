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
    public class CPointGiver : CCollectable
    {
        private readonly double pointsValue;

        public CPointGiver(Point position, double size, double lifetime)
            : base(position, size, lifetime)
        {
            sprite.Fill = Brushes.BlueViolet;
            pointsValue = (1.0 / size) / lifetime * 1000.0;
        }

        public override bool onClick(CPlayer player, CController controller, Point mousePosition)
        {
            if (!isMouseOnObject(mousePosition)) return false;
            controller.AddPoints(pointsValue);
            return true;
        }
    }
}
