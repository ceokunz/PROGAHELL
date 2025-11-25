using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace laba_2
{
    public partial class MainWindow : Window
    {
        private Player new_player;
        private CPlayer player;
        private Enemy currentEnemy;
        private EnemyTemplateManager enemyManager;
        private CController controller;

        private Viewbox splashViewbox;

        //бонусы абобусы

        private List<CCollectable> bonusObjects = new List<CCollectable>();
        private DispatcherTimer bonusTimer;
        private Random rng = new Random();
        private double bonusSpawnRate = 3.0;
        private double bonusSpawnAccum = 0;

        public MainWindow()
        {
            InitializeComponent();

            splashViewbox = (Viewbox)SplashGrid.Children[0];
            StartSplashScreen();

            new_player = new Player(
                Lvl: 1,
                Gold: new BigNumber("0"),
                Damage: new BigNumber("1"),
                DamageModifier: 2,
                UpgradeCost: new BigNumber("10"),
                UpgradeModifier: 1.2
            );

            player = new CPlayer(1);

            var templates = new List<CEnemyTemplate>
            {
                //new CEnemyTemplate("Valera", "20", "3", 80, "C:\\Users\\SAPR\\Source\\Repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\val.png"),
                //new CEnemyTemplate("Zlata", "60", "3", 60, "C:\\Users\\SAPR\\Source\\Repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\zlata.png"),
                //new CEnemyTemplate("Sergey Alexeevich", "666", "10000000", 20, "C:\\Users\\SAPR\\Source\\Repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\alex.png"),
                //new CEnemyTemplate("Maxim Urich", "999", "10000000", 10, "C:\\Users\\SAPR\\Source\\Repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\max.png")


                new CEnemyTemplate("Valera", "20", "3", 80, "C:\\Users\\izzzz\\source\\repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\кунзик.png"),
                new CEnemyTemplate("Zlata", "20", "3", 80, "C:\\Users\\izzzz\\source\\repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\zlata.png"),
                new CEnemyTemplate("Sergey Alexeevich", "666", "10000000", 20, "C:\\Users\\user\\Source\\Repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\alex.png"),
                new CEnemyTemplate("Maxim Urich", "999", "10000000", 10, "C:\\Users\\izzzz\\source\\repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\max.png")
            };

            enemyManager = new EnemyTemplateManager();
            enemyManager.LoadTemplates(templates);
            enemyManager.NormalizeChances();

            SpawnNewEnemy();

            var sceneSize = new Size(scene.ActualWidth, scene.ActualHeight);
            controller = new CController(spawnRate: 3.0, startTime: 0.0, sceneSize: sceneSize);

            bonusTimer = new DispatcherTimer();
            bonusTimer.Interval = TimeSpan.FromMilliseconds(100);
            bonusTimer.Tick += BonusTimer_Tick;
            bonusTimer.Start();

            PlayerGrid.DataContext = player; //
        }

        private void BonusTimer_Tick(object sender, EventArgs e)
        {
            const double delta = 0.1;
            new_player.UpdateClickCooldown(delta);

            bonusSpawnAccum -= delta;
            if (bonusSpawnAccum <= 0)
            {
                SpawnBonusObject();
                bonusSpawnAccum = bonusSpawnRate;
            }

            for (int i = bonusObjects.Count - 1; i >= 0; i--)
            {
                var obj = bonusObjects[i];
                if (obj.UpdateLifetime(delta))
                {
                    if (BonusCanvas.Children.Contains(obj.Sprite))
                        BonusCanvas.Children.Remove(obj.Sprite);
                    bonusObjects.RemoveAt(i);
                }
            }

            if (CooldownBlock != null)
                CooldownBlock.Text = new_player.GetRemainingCooldown().ToString("F2");
        }

        private void SpawnBonusObject()
        {
            double sceneWidth = scene.ActualWidth;
            double sceneHeight = scene.ActualHeight;

            if (sceneWidth <= 0 || sceneHeight <= 0) return;

            double maxSize = 30;
            double x = rng.NextDouble() * (sceneWidth - maxSize) + maxSize / 2;
            double y = rng.NextDouble() * (sceneHeight - maxSize) + maxSize / 2;
            Point pos = new Point(x, y);

            double size = rng.NextDouble() * 20 + 10; 
            double lifetime = rng.NextDouble() * 4 + 1; 

            CCollectable obj = null;
            double r = rng.NextDouble();

            if (r < 0.6) obj = new CPointGiver(pos, size, lifetime);
            else if (r < 0.8) obj = new CClickSpeedUp(pos, size, lifetime);
            else obj = new CSpawnRateChanger(pos, size, lifetime);

            if (obj != null)
            {
                bonusObjects.Add(obj);
                BonusCanvas.Children.Add(obj.Sprite);
            }
        }

        private void SpawnNewEnemy()
        {
            currentEnemy = enemyManager.CreateRandomEnemy();

            EnemyGrid.DataContext = currentEnemy; 

            IconGrid.DataContext = currentEnemy.Icon; 

            AnimateJump();
            if (currentEnemy == null)
            {
                MessageBox.Show("No enemy templates available!");
                return;
            }

        }


        private void UpgradeButton(object sender, RoutedEventArgs e)
        {
            if (new_player.TryUpgrade())
            {

            }
            else
            {
                MessageBox.Show("Not enough gold to upgrade!");
            }
        }

        private void Repeat_Click(object sender, RoutedEventArgs e)
        {
            new_player.ResetToDefault();
            SpawnNewEnemy();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            SpawnNewEnemy();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!player.CanClick()) return;

            Point mousePos = Mouse.GetPosition(BonusCanvas);

            if (controller.MouseClick(mousePos, player))
            {
                return;
            }

            // Иначе — бьём врага
            player.PerformClick();
            if (currentEnemy != null && !currentEnemy.IsDead)
            {
                bool isDead = currentEnemy.TakeDamage(new_player.Damage, out BigNumber reward);
                new_player.AddGold(reward);
                if (isDead)
                {
                    AnimateJump();
                    SpawnNewEnemy();
                }
            }
        }

        //анимации! ---------------------------------------------------
        private void StartSplashScreen()
        {
            var scaleTransform = new ScaleTransform(0.8, 0.8);
            splashViewbox.RenderTransformOrigin = new Point(0.5, 0.5);
            splashViewbox.RenderTransform = scaleTransform;

            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1));

            var scaleUp = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1));

            SplashGrid.BeginAnimation(Grid.OpacityProperty, fadeIn);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleUp);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleUp);

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) =>
            {
                timer.Stop();

                var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1));

                fadeOut.Completed += (s2, e2) => SplashGrid.Visibility = Visibility.Collapsed;

                SplashGrid.BeginAnimation(Grid.OpacityProperty, fadeOut);
            };
            timer.Start();
        }

        private void AnimateJump()
        {
            var bounceY = new DoubleAnimation(0, -50, TimeSpan.FromMilliseconds(100));
            var fallY = new DoubleAnimation(-50, 0, TimeSpan.FromMilliseconds(100));

            var transformGroup = new TransformGroup();
            var translateTransform = new TranslateTransform();
            transformGroup.Children.Add(translateTransform);

            EnemyImage.RenderTransformOrigin = new Point(0.5, 0.5);
            EnemyImage.RenderTransform = transformGroup;

            bounceY.Completed += (s, e) =>
            {
                translateTransform.BeginAnimation(TranslateTransform.YProperty, fallY);
            };

            translateTransform.BeginAnimation(TranslateTransform.YProperty, bounceY);
        }
    }
}