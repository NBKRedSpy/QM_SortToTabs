using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_SortToTabs
{
    public class TabMappings
    {

        public List<List<ItemType>> Tabs { get; set;  } = new List<List<ItemType>>();


        public void Init()
        {
            foreach (var tab in Tabs)
            {
                foreach (ItemType rule in tab)
                {
                    rule.Init();
                }
            }
        }

        public TabMappings()
        {
            SetDefaults();
        }
        public void SetDefaults()
        {
            Tabs.Add(new List<ItemType>());
            Tabs.Add(new List<ItemType>()
            {
                new ItemType("","WeaponRecord", ""),
            });

            Tabs.Add(new List<ItemType>()
            {
                //Invalid for testing.
                new ItemType("", "TrashRecord", "")
            });
            Tabs.Add(new List<ItemType>()
            {
                new ItemType("pmc_smg", "",""),
            });
            Tabs.Add(new List<ItemType>()
            {
                new ItemType("", "", "BartherResource")
            });
            Tabs.Add(new List<ItemType>());

            for (int i = 0; i < 7 - Tabs.Count; i++)
            {
                Tabs.Add(new List<ItemType>());
            }

        }
    }
}
