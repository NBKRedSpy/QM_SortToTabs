using MGSC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QM_SortToTabs
{
    public class ItemTypeMatch
    {

        public string Id { get; set; } = "";
        public string RecordType { get; set; } = "";

        /// <summary>
        /// The trash sub type.  Trash records only.
        /// </summary>
        public string SubType { get; set; } = "";

        /// <summary>
        /// Match any record that contains the category in the ItemRecord.Categories list.
        /// </summary>
        public string Category { get; set; } = "";

        public string ItemClass { get; set; } = "";

        public ItemTypeMatch()
        {
        }

        public ItemTypeMatch(string recordType = "", string id  = "", string subtype = "", string category = "",
            string itemClass = "")
        {
            Id = id?.Trim() ?? "";
            RecordType = recordType?.Trim() ?? "";
            SubType = subtype?.Trim() ?? "";
            Category = category?.Trim() ?? "";
            ItemClass = itemClass?.Trim() ?? "";
        }

        public bool Matches(BasePickupItem pickupItem)
        {
            BasePickupItemRecord pickupRecord = pickupItem.Record<BasePickupItemRecord>();

            if (pickupRecord is CompositeItemRecord)
            {
                pickupRecord = ((CompositeItemRecord)pickupRecord).PrimaryRecord;
            }

            ItemRecord record = pickupRecord as ItemRecord;
            if (record is null) return false;       //This should always be true, but just in case.

            if (Id != "" && Id != record.Id) return false;
            if (RecordType != "" && RecordType != record.GetType().Name) return false;
            if (Category != "" && !record.Categories.Contains(Category, StringComparer.OrdinalIgnoreCase)) return false;
            if (ItemClass != "" && ItemClass != record.ItemClass.ToString()) return false;

            //If subtype is set on a non trash item, just fail the compare
            if (SubType != "" && record.GetType() != typeof(TrashRecord)) return false;

            if (record is TrashRecord)
            {
                string subType = ((TrashRecord)record).SubType.ToString();
                if (SubType != "" && SubType != subType) return false;
            }

            return true;
        }

        public override string ToString()
        {
            return $"Id: '{Id}' RecordType: '{RecordType}' Category: '{Category}' SubType: '{SubType}'";
        }

        public override bool Equals(object obj)
        {
            return obj is ItemTypeMatch match &&
                   Id == match.Id &&
                   RecordType == match.RecordType &&
                   SubType == match.SubType &&
                   Category == match.Category &&
                   ItemClass == match.ItemClass;
        }

        public override int GetHashCode()
        {
            int hashCode = 51521904;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RecordType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SubType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Category);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ItemClass);
            return hashCode;
        }
    }
}
