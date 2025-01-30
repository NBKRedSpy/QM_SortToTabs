using HarmonyLib;
using MGSC;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_SortToTabs
{

    /// <summary>
    /// Props the user to reset the config for version 0.8.5
    /// </summary>
    [HarmonyPatch(typeof(MainMenuScreen), nameof(MainMenuScreen.Show))]
    public static class MainMenuScreen_Show__ShowConversion_Patch
    {

        public static void Prefix()
        {
            if(Plugin.AskForConfigReset)
            {
                Plugin.AskForConfigReset = false;
                SharedUi.ConfirmDialogWindow.Show(x =>
                {
                    if(x == ConfirmDialogWindow.Option.Yes)
                    {
                        Plugin.Config.ResetRulesToDefault(Plugin.ConfigDirectories.ConfigPath);
                        //The config file monitor will automatically refresh the rules
                    }
                }, "Sort To Tabs: Quasimorph 0.8.5 has changed how the items are categorized.  It is recommended to use the new default rules.  The config has already been backed up.", null, "Reset Rules", "Keep Config");
            }
        }
    }
}
