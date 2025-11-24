using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class CCountdownTimer
    {
        private double currentTime;
        private double targetTime;

        public CCountdownTimer(double initialTime)
        {
            this.currentTime = 0;
            this.targetTime = initialTime;
        }

        public double getTime()
        {
            return currentTime;
        }

        public void update(double delta)
        {
            if (currentTime > 0)
            {
                currentTime = Math.Max(0, currentTime - delta);
            }
        }

        public void Start(double time)
        {
            currentTime = time;
            targetTime = time;
        }

        public bool IsFinished()
        {
            return currentTime <= 0;
        }
    }
}
