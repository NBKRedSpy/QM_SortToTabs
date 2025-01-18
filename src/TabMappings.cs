using MGSC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_SortToTabs
{
    public class TabMappings : List<TabMap>
    {
        public void Init()
        {
            foreach (var tab in this)
            {
                tab.ItemMatch.Init();
            }
        }

        public void SetDefaults()
        {
            //Pre-create on item for each tab to simplify the config for users.


            Add(new TabMap(1, new ItemTypeMatch("", "WeaponRecord", "")));
            Add(new TabMap(1, new ItemTypeMatch("", "PlaceableDeviceRecord", "")));
            Add(new TabMap(1, new ItemTypeMatch("", "GrenadeRecord", "")));

            Add(new TabMap(2, new ItemTypeMatch("", "AmmoRecord", "")));

            Add(new TabMap(3, new ItemTypeMatch("", "ArmorRecord", "")));
            Add(new TabMap(3, new ItemTypeMatch("", "BackpackRecord", "")));
            Add(new TabMap(3, new ItemTypeMatch("", "BootsRecord", "")));
            Add(new TabMap(3, new ItemTypeMatch("", "LeggingsRecord", "")));
            Add(new TabMap(3, new ItemTypeMatch("", "HelmetRecord", "")));
            Add(new TabMap(3, new ItemTypeMatch("", "VestRecord", "")));

            Add(new TabMap(4, new ItemTypeMatch("", "FixationMedicineRecord", "")));
            Add(new TabMap(4, new ItemTypeMatch("", "ConsumableRecord", "")));

            Add(new TabMap(5, new ItemTypeMatch("", "RepairRecord", "")));

            Add(new TabMap(6, new ItemTypeMatch("", "DatadiskRecord", "")));
            Add(new TabMap(6, new ItemTypeMatch("", "SkullRecord", "")));
            Add(new TabMap(6, new ItemTypeMatch("", "", "Container")));
            Add(new TabMap(6, new ItemTypeMatch("", "", "Resource")));
            Add(new TabMap(6, new ItemTypeMatch("", "", "BartherResource")));
            Add(new TabMap(6, new ItemTypeMatch("", "", "QuestItem")));
            //The tab number 6 items above this are not needed, but leaving for examples since they should be common items.
            Add(new TabMap(6, new ItemTypeMatch("", "", "")));      

        }
    }
}
