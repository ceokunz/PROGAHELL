using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class CEnemyTemplateList
    {
        public List<CEnemyTemplate> enemies { get; set; }
        private readonly ISaveList<List<CEnemyTemplate>> _serializer = new JsonEnemySaver();
        public CEnemyTemplateList()
        {
            enemies = new List<CEnemyTemplate>();
            _serializer = new JsonEnemySaver();
        }
        public void LoadEnemies(string path)
        {
            this.enemies = _serializer.Load(path);
        }
        public void addEnemy(string name, string maxHitpoints, string goldReward,double spawnChance, string iconPath)
        {
            enemies.Add(new CEnemyTemplate(name, maxHitpoints, goldReward, spawnChance, iconPath)); //да чо почемуууу кака попа жопа
        }

        public CEnemyTemplate getEnemyByName(string name)
        {
            foreach (CEnemyTemplate a in enemies)
            {
                if (a.Name == name) { return a; }

            }
            return null;
        }

        public CEnemyTemplate getEnemyByIndex(int id)
        {
            return enemies[id];
        }

        public List<string> getListOfEnemyNames()
        {
            List<string> names = new List<string>();
            foreach (var a in enemies)
            {
                names.Add(a.Name);
            }
            return names;
        }

        public void normalizeChances()
        {
            double sum = 0;

            for (int i = 0; i < enemies.Count; i++) { sum += enemies[i].SpawnChance; }
            for (int i = 0; i < enemies.Count; i++) { enemies[i].SpawnChance /= sum; }
        }
        public CEnemyTemplate findByChance(double chance)
        {
            double sum = 0;
            for (int i = 0; i < enemies.Count; i++)
            {
                sum += enemies[i].SpawnChance;
                if (sum >= chance) return enemies[i];
            }
            return null;
        }

    }
}
