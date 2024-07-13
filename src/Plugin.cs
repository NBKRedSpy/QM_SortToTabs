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
        public static string ConfigPath => Path.Combine(Application.persistentDataPath, Assembly.GetExecutingAssembly().GetName().Name) + ".yaml";
        public static string ModPersistenceFolder => Path.Combine(Application.persistentDataPath, Assembly.GetExecutingAssembly().GetName().Name);

        public static ModConfig Config { get; private set; }


        [Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfig(IModContext context)
        {
            Config = ModConfig.LoadConfig(ConfigPath);

            if (Config.ExportItemRecords)
            {
                Directory.CreateDirectory(ModPersistenceFolder);
                ExportRecords();
            }
        }

        private static void ExportRecords()
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(ModPersistenceFolder, "DataExport.csv")))
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

        [Hook(ModHookType.AfterBootstrap)]
        public static void DungeonUpdateAfterGameLoop(IModContext context)
        {
            new Harmony("nbk_redspy.proto").PatchAll();

        }
    }
}
