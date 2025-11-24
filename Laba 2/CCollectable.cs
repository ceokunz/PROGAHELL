using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

// Файл: CCollectable.cs
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

        public abstract bool onClick(CPlayer player, CController controller, Point mousePosition);

        protected bool isMouseOnObject(Point mousePosition)
        {
            double dx = mousePosition.X - position.X;
            double dy = mousePosition.Y - position.Y;
            double radius = size.Width / 2;
            return dx * dx + dy * dy <= radius * radius;
        }

        public bool UpdateLifetime(double delta)
        {
            lifetime -= delta;
            return lifetime <= 0;
        }
    }
}
