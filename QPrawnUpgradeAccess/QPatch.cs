using Harmony;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Oculus.Newtonsoft.Json;

namespace QPrawnUpgradeAccess
{
    class QPatch
    {
        public static void Patch()
        {
            HarmonyInstance harmony = HarmonyInstance.Create("qprawn_upgrade_access.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            QPatch.Path = Path ?? "QMods\\QPrawnUpgradeAccess";
            LoadConfig();

            Console.WriteLine("[QPrawnUpgradeAccess] Patched");
        }

        [Serializable]
        public class ModConfig
        {
            public string Id;
            public string DisplayName;
            public string Author;
            public List<string> Requires;
            public bool Enable;
            public string AssemblyName;
            public string EntryMethod;
            public LocalConfig Config;
        }

        [Serializable]
        public class LocalConfig
        {
            public string Upgrade0 = "Slot1";
            public string Upgrade1 = "Slot2";
            public string Storage0 = "Reload";
            public string Storage1 = "";
        }

        public static List<GameInput.Button> UpgradeButtons = new List<GameInput.Button>() { GameInput.Button.Slot1, GameInput.Button.Slot2 };
        public static List<GameInput.Button> StorageButtons = new List<GameInput.Button>() { GameInput.Button.Reload };
        private static string Path = null;

        private static string GetModInfoPath()
        {
            return Environment.CurrentDirectory + "\\" + Path + "\\mod.json";
        }

        private static void LoadConfig()
        {
            string filename = GetModInfoPath();

            if (!File.Exists(filename))
            {
                Console.WriteLine("[QPrawnUpgradeAccess] Couldn't load: " + filename);
                return;
            }

            //Load up the config
            ModConfig config = null;
            try
            {
                config = JsonConvert.DeserializeObject<ModConfig>(File.ReadAllText(filename));
            }
            catch { }

            //Nothing was loaded?
            if (config == null)
            {
                Console.WriteLine("[QPrawnUpgradeAccess] Invalid mod.json format");
                return;
            }

            //Attempt to assign new bindings
            var upgrade = new List<GameInput.Button>();
            var storage = new List<GameInput.Button>();

            //Store my values
            foreach (var button in new string[] { config.Config.Upgrade0, config.Config.Upgrade1 } )
            {
                var val = ToEnum(button);
                if (val != null)
                    upgrade.Add(val.Value);
                else if ( button != "" )
                    Console.WriteLine("[QPrawnUpgradeAccess]: Invalid buton value: " + button);
            }
            foreach (var button in new string[] { config.Config.Storage0, config.Config.Storage1 } )
            {
                var val = ToEnum(button);
                if (val != null)
                    storage.Add(val.Value);
                else if ( button != "" )
                    Console.WriteLine("[QPrawnUpgradeAccess]: Invalid buton value: " + button);
            }

            //Overwrite our default witht the user's data
            QPatch.UpgradeButtons = upgrade;
            QPatch.StorageButtons = storage;
        }

        public static GameInput.Button? ToEnum(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            try
            {
                return (GameInput.Button)Enum.Parse(typeof(GameInput.Button), value, true);
            }
            catch { return null; }
        }
    }
}
