using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace laba_2
{
    public abstract class CCollectable
    {
        protected Point position;
        protected Size size;
        protected double lifetime;
        protected Ellipse sprite;

        public Ellipse Sprite => sprite;

        public CCollectable(Point position, double size, double lifetime)
        {
            this.position = position;
            this.size = new Size(size, size);
            this.lifetime = lifetime;

            sprite = new Ellipse
            {
                Width = size,
                Height = size,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };

            sprite.Margin = new Thickness(position.X - size / 2, position.Y - size / 2, 0, 0);
        }

        public abstract double GetPointsValue();
        public abstract bool IsMouseOnObject(Point mousePosition);
        public abstract bool OnClick(Player player, Point mousePosition);

        public bool UpdateLifetime(double delta)
        {
            lifetime -= delta;
            return lifetime <= 0;
        }
    }
}
