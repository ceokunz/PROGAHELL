using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class CPlayer
    {
        private double baseClickCooldown = 0.5;
        private double currentCooldown;
        private double cooldownTimer;

        public CPlayer(double baseCooldown = 0.5)
        {
            this.baseClickCooldown = baseCooldown;
            this.currentCooldown = baseCooldown;
            this.cooldownTimer = 0;
        }

        public void Update(double delta)
        {
            if (cooldownTimer > 0)
                cooldownTimer -= delta;
        }

        public bool CanClick() => cooldownTimer <= 0;

        public void PerformClick()
        {
            if (CanClick())
            {
                cooldownTimer = currentCooldown;
            }
        }

        public void IncreaseClickSpeed(double reduction)
        {
            currentCooldown = Math.Max(0.1, currentCooldown - reduction);
        }
        public double RemainingCooldown => Math.Max(0, cooldownTimer);
    }
}
