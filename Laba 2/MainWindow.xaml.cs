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
                new CEnemyTemplate("Slime", "10", "5", 70, ""),
                new CEnemyTemplate("Orc", "50", "20", 25, ""),
                new CEnemyTemplate("Dragon", "200", "100", 5, "")
            };

            enemyManager = new EnemyTemplateManager();
            enemyManager.LoadTemplates(templates);
            enemyManager.NormalizeChances();

            SpawnNewEnemy();
            UpdateUI();
        }

        private void SpawnNewEnemy()
        {
            currentEnemy = enemyManager.CreateRandomEnemy();
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
            PlayerLvl.Text = player.Lvl.ToString();
            PlayerGold.Text = player.Gold.ToString();
            PlayerDamage.Text = player.Damage.ToString();

            if (currentEnemy != null)
            {
                EnemyNameBlock.Text = currentEnemy.Name;
                EnemyHPBlock.Text = currentEnemy.CurrentHitpoints.ToString();
                EnemyGoldBlock.Text = currentEnemy.GoldReward.ToString();
            }
        }
    }
}