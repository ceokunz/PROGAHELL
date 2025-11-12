using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace laba_2
{
    public class CController
    {
        public class CObject
        {

        }

        private List<CObject> objects;

        private double spawnRate;
        private int startTime;
        private double spawnTime;
        private Random rgn;
        private double maxLifetime;
        private double minLifetime;
        private double maxSpritesize;
        private double minSpritesize;
        private Size sceneSize;
        private double points;



        public double SpawnRate
        {
            get { return spawnRate; }
            private set { spawnRate = value; }
        }

        public double SpawnTime
        {
            get { return spawnTime; }
            private set { spawnTime = value; }
        }

        public int StartTime
        {
            get { return startTime; }
            private set { startTime = value; }
        }

        public double MaxLifetime
        {
            get { return maxLifetime; }
            private set { maxLifetime = value; }
        }

        public double MinLifetime
        {
            get { return minLifetime; }
            private set { minLifetime = value; }
        }

        public double MaxSpritesize
        {
            get { return maxSpritesize; }
            private set { maxSpritesize = value; }
        }

        public double MinSpritesize
        {
            get { return minSpritesize; }
            private set { minSpritesize = value; }
        }

        public double Points
        {
            get { return points; }
            private set { points = value; }
        }

        public CObject(double spawnRate, int startTime, Size sceneSize)
        {
            spawnRate = SpawnRate;
            startTime = StartTime;

        }

        public void spawnObject()
        {
            
        }

        public void destroyObject(CObject obj) 
        {
            
        }

        public void update(double delta)
        {

        }

        public void mouseClick(Point mousePosition)
        {

        }
    }
}
