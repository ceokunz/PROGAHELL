using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace laba_2
{
    public class CController : INotifyPropertyChanged
    {
        private List<CCollectable> objects;
        private double spawnRate;
        private double gameTime;
        private double spawnTimer;
        private Random rng;
        private double minLifetime = 1.0;
        private double maxLifetime = 5.0;
        private double minSpriteSize = 10.0;
        private double maxSpriteSize = 30.0;
        private Size sceneSize;
        private double points;
        public CPlayer Player { get; private set; }

        public double Time { get => gameTime; private set { gameTime = value; OnPropertyChanged(); } }
        public double Points { get => points; private set { points = value; } }
        public double SpawnRate { get => spawnRate; private set { spawnRate = value; } }
        public Size SceneSize { get => sceneSize; }
        public double ClickCooldown => Player.RemainingCooldown;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CController(double spawnRate, double startTime, Size sceneSize)
        {
            this.spawnRate = spawnRate;
            this.gameTime = startTime;
            this.spawnTimer = spawnRate;
            this.sceneSize = sceneSize;
            this.objects = new List<CCollectable>();
            this.rng = new Random();
            this.Player = new CPlayer(baseCooldown: 0.5);
        }

        public void AddPoints(double value) => Points += value;

        public void IncreaseLifetimeRange(double bonus)
        {
            minLifetime += bonus;
            maxLifetime += bonus;
        }

        public void DecreaseSpawnRate(double reduction)
        {
            spawnRate = Math.Max(0.2, spawnRate - reduction);
        }

        public void SpawnObject()
        {
            double x = rng.NextDouble() * (sceneSize.Width - maxSpriteSize) + maxSpriteSize / 2;
            double y = rng.NextDouble() * (sceneSize.Height - maxSpriteSize) + maxSpriteSize / 2;
            Point pos = new Point(x, y);
            double size = rng.NextDouble() * (maxSpriteSize - minSpriteSize) + minSpriteSize;
            double lifetime = rng.NextDouble() * (maxLifetime - minLifetime) + minLifetime;

            // Спавним разные типы с вероятностями
            double r = rng.NextDouble();
            CCollectable obj = null;

            if (r < 0.6) obj = new CPointGiver(pos, size, lifetime);
            else if (r < 0.75) obj = new CClickSpeedUp(pos, size, lifetime);
            else if (r < 0.9) obj = new CSpawnRateChanger(pos, size, lifetime);
            else obj = new CLifetimeChanger(pos, size, lifetime);

            objects.Add(obj);
        }

        public void Update(double delta)
        {
            gameTime += delta;
            Time = gameTime;
            Player.Update(delta);

            spawnTimer -= delta;
            if (spawnTimer <= 0)
            {
                SpawnObject();
                spawnTimer = spawnRate;
            }

            for (int i = objects.Count - 1; i >= 0; i--)
            {
                if (objects[i].UpdateLifetime(delta))
                {
                    objects.RemoveAt(i);
                }
            }
        }

        public void MouseClick(Point mousePos)
        {
            if (!Player.CanClick()) return;
            Player.PerformClick();

            for (int i = objects.Count - 1; i >= 0; i--)
            {
                if (objects[i].OnClick(Player, this, mousePos))
                {
                    objects.RemoveAt(i);
                    break;
                }
            }
        }

        public List<CCollectable> GetObjects() => new List<CCollectable>(objects);
    }
}
