using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Threading;

namespace laba_2
{
    public partial class MainWindow : Window
    {
        private Player player;
        private Enemy currentEnemy;
        private EnemyTemplateManager enemyManager;

        private CController controller;
        private DispatcherTimer gameTimer;
        private bool gameRunning = false;

        private Viewbox splashViewbox;

        private const string ENEMY_SAVE_PATH = "enemies.json";

        public MainWindow()
        {
            InitializeComponent();

            splashViewbox = (Viewbox)SplashGrid.Children[0];
            StartSplashScreen();

            player = new Player(
                Lvl: 1,
                Gold: new BigNumber("0"),
                Damage: new BigNumber("1"),
                DamageModifier: 2,
                UpgradeCost: new BigNumber("10"),
                UpgradeModifier: 1.2,
                BaseClickCooldown: 1
            );

            var saver = new JsonEnemySaver();
            List<CEnemyTemplate> templates;
            if (File.Exists(ENEMY_SAVE_PATH))
            {
                templates = saver.Load(ENEMY_SAVE_PATH);
            }
            else
            {
                templates = CreateDefaultTemplates();
            }

            enemyManager = new EnemyTemplateManager();
            enemyManager.LoadTemplates(templates);
            enemyManager.NormalizeChances();

            SpawnNewEnemy();


            PlayerGrid.DataContext = player; //

            //шиза из 3ей лабы

            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(100);
            gameTimer.Tick += GameTimer_Tick;

            gameRunning = true;
            gameTimer.Start();
            controller = new CController(player: player, spawnRate: 2.0, startTime: 0.0, sceneSize: new Size(scene.Width, scene.Height));

            this.DataContext = controller;


        }

        private List<CEnemyTemplate> CreateDefaultTemplates()
        {
            return new List<CEnemyTemplate>
            {
            new AverageEnemy("Valera", "20", "3", 80, @"monsters\val.png"),
            new YkorachEnemy("Zlata", "20", "3", 60, @"monsters\zlata.png", 10),
            new ArmoredEnemy("Sergey Alexeevich", "666", "10000000", 20, @"monsters\alex.png", armorReduction: 5),
            new HealingEnemy("Maxim Urich", "999", "10000000", 10, @"monsters\max.png", healChancePercent: 30, healAmount: "50")
            };
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            gameTimer?.Stop();


            var saver = new JsonEnemySaver();
            saver.Save(enemyManager.Enemies.ToList(), ENEMY_SAVE_PATH);

            var playerSaver = new PlayerSaver();
            playerSaver.Save(player, "player.json");
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (!gameRunning) return;

            controller.Update(0.1);

            if (controller.Time >= 20)
            {
                EndGame();
                return;
            }

            

            UpdateUI();
        }

        private void EndGame()
        {
            gameRunning = false;
            gameTimer.Stop();

            MessageBox.Show($"Игра окончена.\nВаш счёт: {player.Gold}", "Конец игры");

            scene.Children.Clear();
        }

        private void UpdateUI()
        {
            var currentObjects = controller.GetObjects();

            var toRemove = scene.Children.OfType<Ellipse>().Where(el => !currentObjects.Any(obj => obj.Sprite == el)).ToList();

            foreach (var el in toRemove)
            {
                scene.Children.Remove(el);
                el.MouseDown -= OnCollectableMouseDown;
            }    

                

            foreach (var obj in currentObjects)
            {
                var sprite = obj.Sprite;
                if (!scene.Children.Contains(sprite))
                {
                    sprite.IsHitTestVisible = true;
                    sprite.MouseDown += OnCollectableMouseDown;
                    scene.Children.Add(sprite);
                }
            }
        }

        private void OnCollectableMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!gameRunning || controller == null) return;

            var ellipse = (Ellipse)sender;
            var obj = controller.GetObjects().FirstOrDefault(o => o.Sprite == ellipse);
            if (obj == null) return;

            Point mousePos = e.GetPosition(scene);
            if (obj.OnClick(player, controller, mousePos))
            {
                controller.RemoveObject(obj);
            }

            e.Handled = true;
        }

        private void SpawnNewEnemy()
        {
            currentEnemy = enemyManager.CreateRandomEnemy();

            EnemyGrid.DataContext = currentEnemy; //

            IconGrid.DataContext = currentEnemy.Icon; //

            AnimateJump();
            if (currentEnemy == null)
            {
                MessageBox.Show("No enemy templates available!");
                return;
            }

        }


        private void UpgradeButton(object sender, RoutedEventArgs e)
        {
            if (player.TryUpgrade())
            {

            }
            else
            {
                MessageBox.Show("Not enough gold to upgrade!");
            }
        }

        private void Repeat_Click(object sender, RoutedEventArgs e)
        {
            player.ResetToDefault();
            SpawnNewEnemy();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            SpawnNewEnemy();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (currentEnemy == null || currentEnemy.IsDead)
                return;

            bool isDead = currentEnemy.TakeDamage(player.Damage, out BigNumber reward);
            player.AddGold(reward);

            if (isDead)
            {
                AnimateJump();
                SpawnNewEnemy();
            }
        }

        private void GameCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!gameRunning) return;
            Point mousePos = e.GetPosition(scene);
            controller.MouseClick(mousePos);
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

            // Применяем к RenderTransform у Image
            var transformGroup = new TransformGroup();
            var translateTransform = new TranslateTransform();
            transformGroup.Children.Add(translateTransform);

            EnemyImage.RenderTransformOrigin = new Point(0.5, 0.5);
            EnemyImage.RenderTransform = transformGroup;

            // Сначала подпрыгивает, потом возвращается
            bounceY.Completed += (s, e) =>
            {
                translateTransform.BeginAnimation(TranslateTransform.YProperty, fallY);
            };

            translateTransform.BeginAnimation(TranslateTransform.YProperty, bounceY);
        }
    }
}