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

        private Assembly GameAssembly { get; set; }
        public string Id {  get; set; } 
        public string RecordType { get; set; }
        public string SubType { get; set; }

        [JsonIgnore]
        public Type ObjectRecordType { get; set; }

        public ItemTypeMatch()
        {
        }

        public ItemTypeMatch(string id , string recordType, string subType)
        {
            Id = id?.Trim() ?? "";
            RecordType = recordType?.Trim() ?? "";
            SubType = subType?.Trim() ?? "";

            Init();
            
        }

        public bool Matches(BasePickupItem item)
        {
            BasePickupItemRecord record = item.Record<BasePickupItemRecord>();

            if(record is CompositeItemRecord)
            {
                record = ((CompositeItemRecord)record).PrimaryRecord;
            }

            if (Id != "" && Id != item.Id) return false;
            if (ObjectRecordType != null && ObjectRecordType != record.GetType()) return false;
            if (SubType != "" && record.GetType() != typeof(TrashRecord)) return false;

            if(record is TrashRecord)
            {
                string subType = ((TrashRecord)record).SubType.ToString();
                if (SubType != "" && SubType != subType) return false;
            }

            return true;
        }

        public void Init()
        {
            if (string.IsNullOrWhiteSpace(RecordType)) return;

            if(GameAssembly == null)
            {

                //Just need a type from the game's assembly.  
                //There doesn't seem to be a good way around this as the entry assembly, etc.
                //don't work.  At least with BepInEx running.
                GameAssembly = Assembly.GetAssembly(typeof(Data));
            }

            ObjectRecordType = GameAssembly.GetType("MGSC." + RecordType);
        }

        public override string ToString()
        {
            return $"Id: '{Id}' RecordType: '{RecordType}' SubType: '{SubType}'";
        }
    }
}
