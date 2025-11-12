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
        private Player player;
        private Enemy currentEnemy;
        private EnemyTemplateManager enemyManager;

        private Viewbox splashViewbox;

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
                UpgradeModifier: 1.2
            );

            var templates = new List<CEnemyTemplate>
            {
                new CEnemyTemplate("Valera", "20", "3", 80, "C:\\Users\\SAPR\\Source\\Repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\val.png"),
                new CEnemyTemplate("Zlata", "60", "3", 60, "C:\\Users\\SAPR\\Source\\Repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\zlata.png"),
                new CEnemyTemplate("Sergey Alexeevich", "666", "10000000", 20, "C:\\Users\\SAPR\\Source\\Repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\alex.png"),
                new CEnemyTemplate("Maxim Urich", "999", "10000000", 10, "C:\\Users\\SAPR\\Source\\Repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\max.png")


                //new CEnemyTemplate("Valera", "20", "3", 80, "C:\\Users\\izzzz\\source\\repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\кунзик.png"),
                //new CEnemyTemplate("Zlata", "20", "3", 80, "C:\\Users\\izzzz\\source\\repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\zlata.png"),
                //new CEnemyTemplate("Sergey Alexeevich", "666", "10000000", 20, "C:\Users\user\Source\Repos\ceokunz\PROGAHELL\laba 2\monsters\alex.png"),
                //new CEnemyTemplate("Maxim Urich", "999", "10000000", 10, "C:\\Users\\izzzz\\source\\repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\max.png")
            };

            enemyManager = new EnemyTemplateManager();
            enemyManager.LoadTemplates(templates);
            enemyManager.NormalizeChances();

            SpawnNewEnemy();


            PlayerGrid.DataContext = player; //
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