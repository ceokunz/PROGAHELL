using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace laba_2
{
    public class CObject
    {
        private Point position;
        private Size size;
        private double lifetime;
        private double pointsValue;
        private Ellipse sprite;

        public double Lifetime
        {
            get { return lifetime; }
            private set { lifetime = value; }
        }
        public double PointsValue
        {
            get { return pointsValue; }
            private set { pointsValue = value; }
        }
        public CObject(Point position, double size, double lifetime)
        {

        }
        public bool isMouseOnObject(Point mousePosition)
        {
            return null;
        }
        public Ellipse getSprite()
        {
            return null;
        }
        public double getPointsValue()
        {
            return null;
        }
        public bool updateLifetime(double delta)
        {
            return null;
        }


    }

}
