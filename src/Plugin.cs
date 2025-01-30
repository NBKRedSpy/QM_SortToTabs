using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_SortToTabs
{
    public static class Plugin
    {
        public static ConfigDirectories ConfigDirectories = new ConfigDirectories();
        public static ModConfig Config { get; private set; }

        /// <summary>
        /// If true, the config file was upgraded and the user needs to be asked if they want to just reset the rules to the new defaults
        /// since the "upgraded" rules don't work like the old rules.
        /// </summary>
        public static bool AskForConfigReset { get; set; } = false;

        [Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfig(IModContext context)
        {

            string configFileName = ConfigDirectories.ModAssemblyName + ".json";

            Directory.CreateDirectory(ConfigDirectories.AllModsConfigFolder);

            ConfigDirectories = new ConfigDirectories(configFileName);
            ConfigDirectories.MoveLegacyConfigLocation(configFileName);

            Directory.CreateDirectory(ConfigDirectories.ModPersistenceFolder);

            Config = ModConfig.LoadConfig(ConfigDirectories.ConfigPath);
            ExportRecords();

            //------ Patching
            Harmony harmony = new Harmony("nbk_redspy.SortToTabs");

            //Have to patch object since the Process function is overridden.

            harmony.Patch(
                AccessTools.Method(typeof(AfterRaidScreen), nameof(AfterRaidScreen.Process)),
                new HarmonyMethod(typeof(CargoScreenUtil), nameof(CargoScreenUtil.ProcessSortLoop))
                );

            harmony.Patch(
                AccessTools.Method(typeof(ArsenalScreen), nameof(ArsenalScreen.Process)),
                new HarmonyMethod(typeof(CargoScreenUtil), nameof(CargoScreenUtil.ProcessSortLoop))
                );

            harmony.Patch(
                AccessTools.Method(typeof(FastTradeScreen), nameof(FastTradeScreen.Process)),
                new HarmonyMethod(typeof(CargoScreenUtil), nameof(CargoScreenUtil.ProcessSortLoop))
                );

            harmony.PatchAll();
        }


        /// <summary>
        /// Invoked when the config file has been changed on disk.
        /// </summary>
        public static void ReloadChangedConfig()
        {
            ModConfig config = ModConfig.ReloadChangedConfig(ConfigDirectories.ConfigPath);

            if(config != null)
            {
                Debug.Log("Config Reloaded");
                Config = config;
            }
        }

        /// <summary>
        /// Writes out the items' data to DataExport.csv.  This is used by the user to create new rules.
        /// </summary>
        private static void ExportRecords()
        {
            string path = Path.Combine(ConfigDirectories.ModPersistenceFolder, "DataExport.csv");

            Directory.CreateDirectory(ConfigDirectories.ModPersistenceFolder);

            StringBuilder sb = new StringBuilder();


            sb.AppendLine("ItemName,Id,Type,SubType,ItemClass,Categories");
            foreach (var item in Data.Items.Records)
            {

                CompositeItemRecord composite = item as CompositeItemRecord;

                BasePickupItemRecord target;
                if (composite != null)
                {
                    target = composite.PrimaryRecord;
                }
                else
                {
                    target = item;
                }

                ItemRecord itemRecord = target as ItemRecord;
                TrashRecord trash = target as TrashRecord;

                string itemName = Localization.Get($"item.{item.Id}.name");

                sb.AppendLine(String.Join(",", itemName, target.Id, target.GetType().Name, 
                    trash?.SubType,itemRecord.ItemClass,
                    string.Join(" ", itemRecord?.Categories)));
            }

            string output = sb.ToString();

            //Do not write if the data is the same as the existing file.
            if (File.Exists(path))
            {
                if (File.ReadAllText(path) == output)
                {
                    return;
                }
            }

            File.WriteAllText(path, output);
        }
    }
}
