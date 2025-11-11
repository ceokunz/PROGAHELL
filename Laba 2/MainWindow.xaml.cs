using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace laba_2
{
    public partial class MainWindow : Window
    {
        private Player player;
        private Enemy currentEnemy;
        private EnemyTemplateManager enemyManager;

        public MainWindow()
        {
            InitializeComponent();

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
                new CEnemyTemplate("Valera", "10", "3", 70, "C:\\Users\\izzzz\\source\\repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\кунзик.png"),
                new CEnemyTemplate("Zlata", "10", "3", 70, "C:\\Users\\izzzz\\source\\repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\zlata.png"),
                new CEnemyTemplate("Sergey Alexeevich", "700", "10000000", 40, "C:\\Users\\izzzz\\source\\repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\alex.png"),
                new CEnemyTemplate("Maxim Urich", "700", "10000000", 40, "C:\\Users\\izzzz\\source\\repos\\ceokunz\\PROGAHELL\\laba 2\\monsters\\max.png")
            };

            enemyManager = new EnemyTemplateManager();
            enemyManager.LoadTemplates(templates);
            enemyManager.NormalizeChances();

            SpawnNewEnemy();
            UpdateUI();

            PlayerGrid.DataContext = player; //
        }

        private void SpawnNewEnemy()
        {
            currentEnemy = enemyManager.CreateRandomEnemy();
            
            EnemyGrid.DataContext = currentEnemy; //
            
            IconGrid.DataContext = currentEnemy.Icon; //

            
            if (currentEnemy == null)
            {
                MessageBox.Show("No enemy templates available!");
                return;
            }

            UpdateUI();
        }

        private void scene_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (currentEnemy == null || currentEnemy.IsDead)
                return;

            bool isDead = currentEnemy.TakeDamage(player.Damage, out BigNumber reward);
            player.AddGold(reward);

            if (isDead)
            {
                SpawnNewEnemy();
            }

            UpdateUI();
        }

        private void UpgradeButton_Click(object sender, RoutedEventArgs e)
        {
            if (player.TryUpgrade())
            {
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Not enough gold to upgrade!");
            }
        }

        private void Repeat_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            SpawnNewEnemy();
        }

        private void UpdateUI()
        {
            //PlayerLvl.Text = player.Lvl.ToString();
            //PlayerGold.Text = player.Gold.ToString();
            //PlayerDamage.Text = player.Damage.ToString();

            //if (currentEnemy != null)
            //{
            //    EnemyNameBlock.Text = currentEnemy.Name;
            //    EnemyHPBlock.Text = currentEnemy.CurrentHitpoints.ToString();
            //    EnemyGoldBlock.Text = currentEnemy.GoldReward.ToString();
            //}
        }

        private void UpgradeButton(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("jopa", "434");
            currentEnemy.TakeDamage(player.Damage, out BigNumber reward);
        }
    }
}