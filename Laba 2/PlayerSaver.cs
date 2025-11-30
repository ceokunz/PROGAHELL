using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace laba_2
{
    public class PlayerSaver : ISaveList<Player>
    {
        public Player Load(string path)
        {
            if (!File.Exists(path)) return null;
            string json = File.ReadAllText(path);
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            // Извлекаем поля вручную — Player не сериализуется автоматически из-за BigNumber
            return new Player(
                Lvl: (int)(long)data["Lvl"],
                Gold: new BigNumber(data["Gold"].ToString()),
                Damage: new BigNumber(data["Damage"].ToString()),
                DamageModifier: (long)data["DamageModifier"],
                UpgradeCost: new BigNumber(data["UpgradeCost"].ToString()),
                UpgradeModifier: (double)data["UpgradeModifier"],
                BaseClickCooldown: (double)data["BaseClickCooldown"]
            );
        }

        public void Save(Player player, string path)
        {
            var data = new Dictionary<string, object>
            {
                ["Lvl"] = player.Lvl,
                ["Gold"] = player.Gold.ToString(),
                ["Damage"] = player.Damage.ToString(),
                ["DamageModifier"] = player.DamageModifier,
                ["UpgradeCost"] = player.UpgradeCost.ToString(),
                ["UpgradeModifier"] = player.UpgradeModifier,
                ["BaseClickCooldown"] = player.BaseClickCooldown.ToString(),
            };
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }
    }
}
