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
        public void SetDefaults()
        {
            //Pre-create on item for each tab to simplify the config for users.

            
            Add(new TabMap(1, new ItemTypeMatch(id: "itemsBox")));
            Add(new TabMap(6, new ItemTypeMatch(itemClass: "ScienceBarter")));
            Add(new TabMap(6, new ItemTypeMatch(itemClass: "Cyborg")));


            Add(new TabMap(1, new ItemTypeMatch("WeaponRecord")));
            Add(new TabMap(1, new ItemTypeMatch(itemClass: "Turret")));
            Add(new TabMap(1, new ItemTypeMatch("PlaceableDeviceRecord")));
            Add(new TabMap(1, new ItemTypeMatch("GrenadeRecord")));

            Add(new TabMap(2, new ItemTypeMatch("AmmoRecord")));

            Add(new TabMap(3, new ItemTypeMatch("ArmorRecord")));
            Add(new TabMap(3, new ItemTypeMatch("BackpackRecord")));
            Add(new TabMap(3, new ItemTypeMatch("BootsRecord")));
            Add(new TabMap(3, new ItemTypeMatch("LeggingsRecord")));
            Add(new TabMap(3, new ItemTypeMatch("HelmetRecord")));
            Add(new TabMap(3, new ItemTypeMatch("VestRecord")));

            Add(new TabMap(4, new ItemTypeMatch("FixationMedicineRecord")));
            Add(new TabMap(4, new ItemTypeMatch("ConsumableRecord")));

            Add(new TabMap(5, new ItemTypeMatch("RepairRecord")));

            Add(new TabMap(6, new ItemTypeMatch("DeviceRecord")));
            Add(new TabMap(6, new ItemTypeMatch("DatadiskRecord")));
            Add(new TabMap(6, new ItemTypeMatch("SkullRecord")));
            Add(new TabMap(6, new ItemTypeMatch(subtype: "Container")));
            Add(new TabMap(6, new ItemTypeMatch(subtype: "Resource")));
            Add(new TabMap(6, new ItemTypeMatch(subtype: "BartherResource")));
            Add(new TabMap(6, new ItemTypeMatch(subtype: "QuestItem")));

            // Default anything else to the first tab.
            Add(new TabMap(1, new ItemTypeMatch()));      

        }
    }
}
