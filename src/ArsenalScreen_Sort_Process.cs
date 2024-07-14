using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MGSC;
using UnityEngine;

namespace QM_SortToTabs
{
    [HarmonyPatch(typeof(ArsenalScreen), nameof(ArsenalScreen.Process))]
    internal class ArsenalScreen_Sort_Process
    {

        public static void Postfix(ArsenalScreen __instance)
        {

            //Check from original method
            if (!__instance.gameObject.activeSelf || SharedUi.ManageSkullWindow.IsViewActive || SharedUi.NarrativeTextScreen.IsViewActive)
            {
                return;
            }

            if (Input.GetKeyUp(Plugin.Config.SortToTabsKey))
            {
                Sort(__instance);
            }

            if(Input.GetKeyUp(KeyCode.S))
            {
                __instance.SortArsenalButtonOnOnClick(null);


            }

        }

        public static void Sort(ArsenalScreen __instance)
        {
            //The UI's selected tab.
            int currentTabIndex = __instance._tabsView._idsToTabs.Values.ToList().FindIndex(x => x.IsSelected);

            if (currentTabIndex == -1)
            {
                //Not sure how this would happen, but just incase.
                Debug.Log("did not find selected tab");
                return;
            }

            List<ItemStorage> shipCargo = __instance._magnumCargo.ShipCargo;

            //Storage for the current tab

            if (shipCargo.Count < currentTabIndex + 1)
            {
                //A non cargo tab.  Currently there is a recycler tab, which is not cargo.
                return;
            }

            ItemStorage storage = shipCargo[currentTabIndex];

            if (storage == null)
            {
                throw new ApplicationException($"Did not find matching storage index {currentTabIndex}");
            }

            //Reload the mappings if the config file has changed.
            //Helps users create their  maps.

            Plugin.ReloadChangedConfig();
            TabMappings tabMappings = Plugin.Config.TabMappings;

            bool logMatch = Plugin.Config.DebugLogMatches;

            //Find all items on this page.
            //Reverse due to item removal
            for (int itemIndex = storage.Items.Count - 1; itemIndex >= 0; itemIndex--)
            {
                BasePickupItem item = storage.Items[itemIndex];


                TabMap matchedRule = tabMappings.FirstOrDefault(x => x.ItemMatch.Matches(item));


                if (logMatch)
                {
                    Debug.Log($"Match: '{item.Id}' {(matchedRule == null ? "None found" : matchedRule.ToString())}");
                }

                if (matchedRule == null || matchedRule.TabNumber == currentTabIndex + 1) continue;

                storage.Remove(item);
                shipCargo[matchedRule.TabNumber - 1].ExpandHeightAndPutItem(item);
            }
            __instance._tabsView.RefreshAllTabs();
        }
    }
}
