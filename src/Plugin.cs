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
        public static string ConfigPath => Path.Combine(Application.persistentDataPath, Assembly.GetExecutingAssembly().GetName().Name) + ".json";
        public static string ModPersistenceFolder => Path.Combine(Application.persistentDataPath, Assembly.GetExecutingAssembly().GetName().Name);

        public static ModConfig Config { get; private set; }


        [Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfig(IModContext context)
        {
            Config = ModConfig.LoadConfig(ConfigPath);
            ExportRecords();


            //------ Patching
            Harmony harmony = new Harmony("nbk_redspy.proto");

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

            ////Unable to find this screen invoked.  Maybe unused now?
            ////  Tried the on planet cargo and trade screens, but no invoke.
            ////While it should be fine, it's not being included since I cannot test it.
            //harmony.Patch(
            //    AccessTools.Method(typeof(TradeShuttleScreen), nameof(TradeShuttleScreen.Process)),
            //    new HarmonyMethod(typeof(CargoScreenUtil), nameof(CargoScreenUtil.ProcessSortLoop))
            //    );
        }


        public static void ReloadChangedConfig()
        {
            ModConfig config = ModConfig.ReloadChangedConfig(ConfigPath);

            if(config != null)
            {
                Debug.Log("Config Reloaded");
                Config = config;
            }
        }


        private static void ExportRecords()
        {
            string path = Path.Combine(ModPersistenceFolder, "DataExport.csv");

            //Only write if the file does not already exist.
            if (File.Exists(path)) return;

            Directory.CreateDirectory(ModPersistenceFolder);

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("Id,Type,SubType");

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

                    TrashRecord trash = target as TrashRecord;

                    writer.WriteLine(String.Join(",", target.Id, target.GetType().Name, trash?.SubType));
                }
            }
        }

     
    }
}
