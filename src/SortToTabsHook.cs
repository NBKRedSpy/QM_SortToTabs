using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_SortToTabs
{
    public class SortToTabsHook : MonoBehaviour
    {

        public ScreenWithShipCargo CargoScreen { get; set; }

        private SortToTabsHook()
        {
        }

        public void Update()
        {
            CargoScreenUtil.ProcessSortLoop(CargoScreen);

        }
    }
}
