using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MGSC;
using UnityEngine;

namespace QM_SortToTabs
{
    public static class CargoScreenUtil
    {

        private static void Sort(ScreenWithShipCargo __instance)
        {

            //------------ recycling testing

            ItemTab selectedTab = __instance._tabsView.FirstSelectedTab();
            bool isRecyclerTab = __instance._recyclingTab == selectedTab;
            bool isRecycling = __instance._magnumCargo.RecyclingInProgress;

            //The tab will be hidden if it is not valid for the screen or the player
            //  does not gave the technology researched.
            bool includeRecyclerStorage = __instance._recyclingTab != null && !isRecycling;

            if (isRecyclerTab && isRecycling)
            {
                //Do not sort the recycling tab when recycling.
                return;
            }

            //The UI's selected tab.
            //The UI includes the recycling tab just another tab. 
            //  However, the game's storage is split between regular storage and the recycler storage.
            int currentUiTabIndex = __instance._tabsView._idsToTabs.Values.ToList().FindIndex(x => x.IsSelected);


            //The Recycler is included in the UI's tab list; however, 
            //  the storage is split between two properties.
            if (currentUiTabIndex == -1)
            {
                //Not sure how this would happen, but just in case.
                Debug.Log("did not find selected tab");
                return;
            }

            ItemStorage sourceStorage = isRecyclerTab ? __instance._magnumCargo.RecyclingStorage : __instance._magnumCargo.ShipCargo[currentUiTabIndex];

            if (sourceStorage == null)
            {
                throw new ApplicationException($"Did not find matching storage index or active recycling tab. {currentUiTabIndex}");
            }

            //Reload the mappings if the config file has changed.
            //Helps users create their  maps.
            Plugin.ReloadChangedConfig();
            TabMappings tabMappings = Plugin.Config.TabMappings;

            bool logMatch = Plugin.Config.DebugLogMatches;

            List<ItemStorage> shipStorages = new List<ItemStorage>(__instance._magnumCargo.ShipCargo);

            //Add the recycling storage if it exists and is not busy.
            if(includeRecyclerStorage)
            {
                shipStorages.Add(__instance._magnumCargo.RecyclingStorage);
            }

            //Find all items on this page.
            //Reverse due to item removal
            for (int itemIndex = sourceStorage.Items.Count - 1; itemIndex >= 0; itemIndex--)
            {

                BasePickupItem item = sourceStorage.Items[itemIndex];

                TabMap matchedRule = tabMappings.FirstOrDefault(x => x.ItemMatch.Matches(item));


                if (logMatch)
                {
                    Debug.Log($"Match: '{item.Id}' {(matchedRule == null ? "None found" : matchedRule.ToString())}");
                }

                //No match, invalid tab number, or targeting the current tab.
                //Yes,this downstream validation is hacky.  Its a mod. =)
                if (matchedRule == null ||  matchedRule.TabNumber <= 0 || matchedRule.TabNumber == currentUiTabIndex + 1)
                {
                    continue;
                }

                int targetTab;

                if(matchedRule.TabNumber <= shipStorages.Count)
                {
                    targetTab = matchedRule.TabNumber;
                }
                else if (matchedRule.AltTabNumber > 0 && matchedRule.AltTabNumber <= shipStorages.Count)
                {
                    targetTab = matchedRule.AltTabNumber;

                    if (logMatch)
                    {
                        Debug.Log($"Match: '{item.Id}'. Target tab is not available.  Using alt {targetTab}");
                    }

                    if(targetTab == currentUiTabIndex + 1)
                    {
                        continue;
                    }
                }
                else
                {
                    //Tab not available.  No fallback.
                    continue;
                }


                sourceStorage.Remove(item);
                shipStorages[targetTab -1].ExpandHeightAndPutItem(item);
            }

            __instance._tabsView.RefreshAllTabs();
        }


        public static void ProcessSortLoop(ScreenWithShipCargo __instance)
        {
            if (!__instance.gameObject.activeSelf || SharedUi.ManageSkullWindow.IsViewActive || SharedUi.NarrativeTextScreen.IsViewActive)
            {
                return;
            }

            if (Input.GetKeyUp(Plugin.Config.SortToTabsKey))
            {
                Sort(__instance);
            }

            if (Input.GetKeyUp(Plugin.Config.TabSortKey))
            {
                __instance.SortArsenalButtonOnOnClick(null,1);


            }
        }
    }
}
