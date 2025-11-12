using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace laba_2
{
    public class Object
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
        public Cobject(Point position, double size, double lifetime)
        {
            
        }
        public bool isMouseOnObject(Point mousePosition)
        {
            
        }
        public Ellipse getSprite()
        { 
        }
        public double getPointsValue()
        {
        }
        public bool updateLifetime(double delta)
        {

        }


    }
    
}
