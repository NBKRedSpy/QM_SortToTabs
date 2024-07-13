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
    [HarmonyPatch(typeof(MainMenuScreen), nameof(MainMenuScreen.Awake))]
    internal class Patch_Test
    {

        public static void Postfix()
        {
            Debug.Log("----- Main Menu Patch");
        }
    }
}
