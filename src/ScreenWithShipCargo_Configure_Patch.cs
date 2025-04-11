using HarmonyLib;
using MGSC;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_SortToTabs
{

    [HarmonyPatch(typeof(ScreenWithShipCargo), nameof(ScreenWithShipCargo.Configure))]
    public static class ScreenWithShipCargo_Configure_Patch
    {
        public static void Prefix(ArsenalScreen __instance)
        {
            const string GameObjectName = "SortToTabsUpdateObject";

            if (__instance.GetComponent<SortToTabsHook>() != null) return;

            SortToTabsHook update = __instance.gameObject.AddComponent<SortToTabsHook>();
            update.name = GameObjectName;
            update.CargoScreen = __instance;
        }
    }
}
