using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace laba_2
{
    public class EnemyTemplateManager
    {
        private List<CEnemyTemplate> enemies = new List<CEnemyTemplate>();
        private Random random = new Random();

       
        public IReadOnlyList<CEnemyTemplate> Enemies => enemies.AsReadOnly();
        public void AddEnemyTemplate(CEnemyTemplate enemyTemplate) 
        {
            if (enemyTemplate == null)
                throw new ArgumentNullException(nameof(enemyTemplate));

            enemies.Add(enemyTemplate);
        }
        public void LoadTemplates(IEnumerable<CEnemyTemplate> templates) 
        {
            if (templates == null)
                throw new ArgumentNullException(nameof(templates));

            enemies.Clear();
            enemies.AddRange(templates);
        }
        public void NormalizeChances() 
        {
            if (enemies.Count == 0)
                return;

            double sum = 0;

            for (int i = 0; i < enemies.Count; i++) 
                sum += enemies[i].SpawnChance;
            
            if (sum == 0) 
            {
                double equalChance = 1.0 / enemies.Count;
                for (int i = 0; i < enemies.Count; i++)
                    enemies[i].SpawnChance = equalChance;
                return;
            }

            
            for (int i = 0; i < enemies.Count; i++)
                enemies[i].SpawnChance /= sum;
        }
        
        public CEnemyTemplate FindByChance(double chance)
        {
            if (enemies.Count == 0)
                return null;

            if (chance < 0 || chance > 1)
                throw new ArgumentException("Chance must be between 0 and 1", nameof(chance));

            double sum = 0;

            for (int i = 0; i < enemies.Count; i++)
            {
                sum += enemies[i].SpawnChance;
                if (sum >= chance)
                    return enemies[i];
            }

            
            return enemies[enemies.Count - 1];
        }
        public CEnemyTemplate GetRandomEnemy()
        {
            if (enemies.Count == 0)
                return null;

            double chance = random.NextDouble();
            return FindByChance(chance);
        }
        public Enemy CreateEnemyFromTemplate(CEnemyTemplate template)
        {
            return (Enemy)EnemyFactory.CreateEnemy(template);
        }

        public Enemy CreateRandomEnemy()
        {
            var template = GetRandomEnemy();
            return CreateEnemyFromTemplate(template);
        }
        public void Clear()
        {
            enemies.Clear();
        }
        public bool AreChancesNormalized()
        {
            if (enemies.Count == 0)
                return true;

            double sum = enemies.Sum(e => e.SpawnChance);
            return Math.Abs(sum - 1.0) < 0.0001;
        }
        public double GetTotalChance()
        {
            return enemies.Sum(e => e.SpawnChance);
        }
    }

    public abstract class CEnemyTemplate
    {
        public string Name { get; set; } = "Name";
        public BigNumber MaxHitpoints { get; set; } = new BigNumber("1");
        public BigNumber GoldReward { get; set; } = new BigNumber("0");
        public double SpawnChance { get; set; } = 1.0;
        public string IconPath { get; set; } = "";

        protected CEnemyTemplate() 
        { 

        }

        protected CEnemyTemplate(string name, string maxHitpoints, string goldReward, double spawnChance, string iconPath)
        {
            Name = name ?? "Unknown";
            MaxHitpoints = new BigNumber(maxHitpoints);
            GoldReward = new BigNumber(goldReward);
            SpawnChance = spawnChance;
            IconPath = iconPath;
        }
        public override string ToString()
        {
            return $"{Name} (HP: {MaxHitpoints}, Gold: {GoldReward}, Chance: {SpawnChance:P2})";
        }
    }

}

