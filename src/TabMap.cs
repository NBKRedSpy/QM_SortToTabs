using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace QM_SortToTabs
{
    
    /// <summary>
    /// The list of items to put into a specific tab
    /// </summary>
    public class TabMap
    {
        /// <summary>
        /// The one based tab number that this list of item matches are for.
        /// </summary>
        //Note - Added this only because I thought it might be less confusing for non technical users.
        public int TabNumber { get; set; }

        /// <summary>
        /// Used if the first tab is unavailable.  Such as the recycler tab being busy.
        /// </summary>
        public int AltTabNumber { get; set; }


        public ItemTypeMatch ItemMatch { get; set; }

        public TabMap()
        {
                
        }

        public TabMap(int tabNumber, ItemTypeMatch itemMatch)
        {
            TabNumber = tabNumber;
            ItemMatch = itemMatch;
        }

        public override string ToString()
        {
            return $"To Tab {TabNumber} {ItemMatch.ToString()}";
        }

    }
}
