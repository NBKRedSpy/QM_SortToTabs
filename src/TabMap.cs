using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
