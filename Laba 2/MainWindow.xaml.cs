using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
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


        public MainWindow()
        {
            InitializeComponent();

            splashViewbox = (Viewbox)SplashGrid.Children[0];
            StartSplashScreen();

            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(100);
            gameTimer.Tick += GameTimer_Tick;

            player = new Player(
                Lvl: 1,
                Gold: new BigNumber("0"),
                Damage: new BigNumber("1"),
                DamageModifier: 2,
                UpgradeCost: new BigNumber("10"),
                UpgradeModifier: 1.2,
                BaseClickCooldown: 1
            );

            var templates = new List<CEnemyTemplate>
            {


                new CEnemyTemplate("Valera", "20", "3", 80, "C:\\Users\\user\\Source\\Repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\val.png"),
                new CEnemyTemplate("Zlata", "20", "3", 60, "C:\\Users\\user\\Source\\Repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\zlata.png", ETypes.Ykorach),
                new CEnemyTemplate("Sergey Alexeevich", "666", "100", 20, "C:\\Users\\user\\Source\\Repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\alex.png", ETypes.Armored),
                new CEnemyTemplate("Maxim Urich", "999", "100000", 10, "C:\\Users\\user\\Source\\Repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\max.png",ETypes.Healing)

            };

            string g = JsonSerializer.Serialize(templates);

            enemyManager = new EnemyTemplateManager();
            enemyManager.LoadTemplates(templates);
            enemyManager.NormalizeChances();

            InitializeGame();

            SpawnNewEnemy();
            
            PlayerGrid.DataContext = player;
        }

        private void OnEnemyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Enemy.CurrentHitpoints))
            {
                if (sender is YkorachEnemy)
                {
                    UpdateEnemySize();
                }
            }
        }

        private void InitializeGame()
        {
            double width = scene.ActualWidth > 0 ? scene.ActualWidth : 125;
            double height = scene.ActualHeight > 0 ? scene.ActualHeight : 250;

            controller = new CController(
                player: player,
                spawnRate: 2.0,
                startTime: 0.0,
                sceneSize: new Size(width, height)
            );

            controller.addObject += OnSphereAdded;
            controller.removeObject += OnSphereRemoved;

            this.DataContext = controller;

            scene.Children.Clear();
            EventLogBox.Items.Clear();

            gameRunning = true;
            gameTimer.Start();
        }

        private void OnSphereAdded(object sender, GameEventArgs e)
        {
            if (e.Target is Ellipse sprite)
            {
                scene.Children.Add(sprite);
            }
        }

        private void OnSphereRemoved(object sender, GameEventArgs e)
        {
            if (e.Target is Ellipse sprite)
            {
                scene.Children.Remove(sprite);
            }

            if (!string.IsNullOrEmpty(e.Message))
            {
                EventLogBox.Items.Insert(0, e.Message);
                if (EventLogBox.Items.Count > 30)
                    EventLogBox.Items.RemoveAt(EventLogBox.Items.Count - 1);
            }
        }

        private void OnEnemyEvent(object sender, GameEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Message))
            {
                EventLogBox.Items.Insert(0, e.Message);
                if (EventLogBox.Items.Count > 50)
                    EventLogBox.Items.RemoveAt(EventLogBox.Items.Count - 1);
            }
        }

        private void UpdateEnemySize()
        {
            if (currentEnemy is YkorachEnemy ykorEnemy)
            {
                double scale = ykorEnemy.ScaleFactor;
                const double baseSize = 125.0;
                image.Width = baseSize * scale;
            }
            else
            {
                image.Height = 250;
                image.Width = 125;
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (!gameRunning) return;

            controller.Update(0.1);

            //if (controller.Time >= 20)
            //{
            //    EndGame();
            //    return;
            //}

            //CooldownBlock.Text = controller.ClickCooldown.ToString("F2");

            //UpdateUI();
        }

        private void EndGame()
        {
            gameRunning = false;
            gameTimer.Stop();

            MessageBox.Show($"Игра окончена.\nВаш счёт: {player.Gold}", "Конец игры");

            scene.Children.Clear();
        }

        //private void UpdateUI()
        //{
        //    var currentObjects = controller.GetObjects();

        //    var toRemove = scene.Children.OfType<Ellipse>().Where(el => !currentObjects.Any(obj => obj.Sprite == el)).ToList();

        //    foreach (var el in toRemove)
        //    {
        //        scene.Children.Remove(el);
        //        el.MouseDown -= OnCollectableMouseDown;
        //    }    

                

        //    foreach (var obj in currentObjects)
        //    {
        //        var sprite = obj.Sprite;
        //        if (!scene.Children.Contains(sprite))
        //        {
        //            sprite.IsHitTestVisible = true;
        //            sprite.MouseDown += OnCollectableMouseDown;
        //            scene.Children.Add(sprite);
        //        }
        //    }
        //}

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
            if (currentEnemy != null)
            {
                currentEnemy.EnemyDamaged -= OnEnemyEvent;
                currentEnemy.EnemyDefeated -= OnEnemyEvent;
                if (currentEnemy is INotifyPropertyChanged npc1)
                    npc1.PropertyChanged -= OnEnemyPropertyChanged;
            }

            currentEnemy = enemyManager.CreateRandomEnemy();

            currentEnemy.EnemyDamaged += OnEnemyEvent;
            currentEnemy.EnemyDefeated += OnEnemyEvent;
            if (currentEnemy is INotifyPropertyChanged npc2)
                npc2.PropertyChanged += OnEnemyPropertyChanged;

            EnemyGrid.DataContext = currentEnemy;
            IconGrid.DataContext = currentEnemy?.Icon;
            UpdateEnemySize();  
            AnimateJump();      

            OnEnemyEvent(this, new GameEventArgs(currentEnemy, $"{currentEnemy.Name} явился..."));

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

        private void SubscribeToEnemyEvents(Enemy enemy)
        {
            if (enemy == null) return;

            enemy.EnemySpawned += OnEnemyEvent;
            enemy.EnemyDamaged += OnEnemyEvent;
            enemy.EnemyDefeated += OnEnemyEvent;
        }

        private void UnsubscribeFromEnemyEvents(Enemy enemy)
        {
            if (enemy == null) return;

            enemy.EnemySpawned -= OnEnemyEvent;
            enemy.EnemyDamaged -= OnEnemyEvent;
            enemy.EnemyDefeated -= OnEnemyEvent;
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