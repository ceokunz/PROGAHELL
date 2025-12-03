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

       
        public IReadOnlyList<CEnemyTemplate> Enemies => enemies.AsReadOnly(); //свойство для доступа к списку шаблонов (только для чтения)
        public void AddEnemyTemplate(CEnemyTemplate enemyTemplate) //добавляет шаблон противника 
        {
            if (enemyTemplate == null)
                throw new ArgumentNullException(nameof(enemyTemplate));

            enemies.Add(enemyTemplate);
        }
        public void LoadTemplates(IEnumerable<CEnemyTemplate> templates) //загружает коллекцию шаблонов противников
        {
            if (templates == null)
                throw new ArgumentNullException(nameof(templates));

            enemies.Clear();
            enemies.AddRange(templates);
        }
        public void NormalizeChances() //нормализация шансов появления противников (сумма шансов = 1)
        {
            if (enemies.Count == 0)
                return;

            double sum = 0;

            for (int i = 0; i < enemies.Count; i++) //сумму всех шансов
                sum += enemies[i].SpawnChance;
            
            if (sum == 0) //если сумма равна 0, шансы распределяются равномерно
            {
                double equalChance = 1.0 / enemies.Count;
                for (int i = 0; i < enemies.Count; i++)
                    enemies[i].SpawnChance = equalChance;
                return;
            }

            //нормализуем шансы
            for (int i = 0; i < enemies.Count; i++)
                enemies[i].SpawnChance /= sum;
        }
        //поиск шаблона противника 
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

            //если не найден, возвращаем последнего
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

            if (template == null)
                return null;

            return EnemyFactory.CreateEnemy(template, template.MaxHitpoints, template.GoldReward);
        }
        
        //создает случайного противника 
        public Enemy CreateRandomEnemy()
        {
            CEnemyTemplate template = GetRandomEnemy();
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
            return Math.Abs(sum - 1.0) < 0.0001; // Учитываем погрешность вычислений
        }
        //получает общую сумму шансов всех противников
        public double GetTotalChance()
        {
            return enemies.Sum(e => e.SpawnChance);
        }
    }

}

