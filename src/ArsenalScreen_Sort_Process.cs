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


            if (!Input.GetKeyUp(KeyCode.F5))
            {
                return;
            }


            //Find currently selected tab?
            int currentTabIndex = __instance._tabsView._idsToTabs.Values.ToList().FindIndex(x => x.IsSelected);

            if (currentTabIndex == -1)
            {
                //Not sure how this would happen....
                Debug.Log("did not find selected tab");
                return;
            }

            //Get only the tabs within the amount of tabs available.
            List<List<ItemType>> tabSorts = Plugin.Config.DestinationTabs.Tabs.Take(__instance._tabsView._idsToTabs.Values.Count).ToList();

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

            //Find all items on this page.
            for (int i = storage.Items.Count - 1; i >= 0; i--)
            {
                BasePickupItem item = storage.Items[i];

                for (int sortInfoIndex = 0; sortInfoIndex < tabSorts.Count; sortInfoIndex++)
                {
                    if (sortInfoIndex == currentTabIndex) continue;

                    List<ItemType> currentSort = tabSorts[sortInfoIndex];

                    BasePickupItemRecord itemRecord = item.Record<BasePickupItemRecord>();

                    if (currentSort.Any(x => x.Matches(item)))
                    {
                        //the sort index will match the destination tab index.
                        shipCargo[sortInfoIndex].ExpandHeightAndPutItem(item);

                        storage.Remove(item);
                        //remove from this storage
                    }
                }
            }
            __instance._tabsView.RefreshAllTabs();

        }
    }
}
