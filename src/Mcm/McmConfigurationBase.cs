using HarmonyLib;
using MGSC;
using ModConfigMenu;
using ModConfigMenu.Objects;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;

namespace QM_SortToTabs.Mcm
{


    /// <summary>
    /// The MCM configuration base class which provides methods to work with ConfigValues.
    /// </summary>
    /// <remarks>See note in code.  This cannot be a generic class.  Mono has a bug with using reflection with generic types 
    /// that have dependencies on assemblies that are not loaded.  This is Mono specific, not .NET</remarks>
    internal abstract class McmConfigurationBase
    {
        //Note - due to a bug with Mono, this class cannot be a generic.  The game uses the assembly to search for types
        //  and Mono will create a VTable setup issue if a used type cannot be located.  This seems to only affect
        //  classes with generics.  Since MCM is optional and the assembly may not be present, all ConfigValue items
        //  will cause the issue.

        public Logger Logger { get; set; }

        public IMcmConfigTarget Config { get; set; }

        /// <summary>
        /// Cache for the type of config.  Used for reflection
        /// </summary>
        private Type ConfigType { get; set; }

        /// <summary>
        /// The defaults for the config.  It is expected that any defaults are set on 
        /// IMcmConfigTarget's construction
        /// </summary>
        private IMcmConfigTarget Defaults { get; set; }

        /// <summary>
        /// Used to make the keys for read only entries unique.  This prevents the MCM's dictionary
        /// from colliding due to entries that are simply notes or read only.
        /// </summary>
        private static int UniqueId = 0;
        /// <param name="config">The object to read and write the settings to.  This must have a new() signature.</param>
        /// <param name="logger">Used to log entries.</param>
        public McmConfigurationBase(IMcmConfigTarget config, Logger logger)
        {
            Logger = logger;
            Config = config;
            ConfigType = config.GetType();
            Defaults = (IMcmConfigTarget)Activator.CreateInstance(ConfigType);    
        }

        /// <summary>
        /// Attempts to configure the MCM, but logs an error and continues if it fails.
        /// </summary>
        public bool TryConfigure()
        {
            try
            {
                Configure();
                return true;
            }
            catch (FileNotFoundException)
            {
                Logger.Log("Bypassing MCM. The 'Mod Configuration Menu' mod is not loaded. ");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred when configuring MCM");
            }

            return false;
        }

        
        /// <summary>
        /// The T specific configuration.  Use the Create* and OnSave helper functions.
        /// </summary>
        public abstract void Configure();


        protected ConfigValue CreateRestartMessage()
        {
            return new ConfigValue("__Restart", "The game must be restarted for any changes to take effect.", "Restart");

        }

        /// <summary>
        /// Creates a setting that is only available in the config file due to lack of MCM support.
        /// Creates a unique ID for the key to avoid the Save from picking it up.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        protected ConfigValue CreateReadOnly(string propertyName, string header = "Only available in config file")
        {
            int key = UniqueId++;

            object value = AccessTools.Property(ConfigType, propertyName)?.GetValue(Config);

            if(value == null)
            {
                //Try field
                value = AccessTools.Field(ConfigType, propertyName)?.GetValue(Config);
            }

            string formattedValue;

            if (value == null)
            {
                value = "Null";
            }
            if (value is IEnumerable enumList)
            {
                List<string> list = new();

                foreach (var item in enumList)
                {
                    list.Add(item.ToString());
                }

                formattedValue = string.Join(",", list);
            }
            else
            {
                formattedValue = value.ToString();
            }

            string formattedPropertyName = FormatUpperCaseSpaces(propertyName);

            return new ConfigValue(key.ToString(), $@"{formattedPropertyName}: <color=#FFFEC1>{formattedValue}</color>", header);

        }

        /// <summary>
        /// Formats a string with no spaces to having spaces before each uppercase letter.
        /// Used to make a property name easier to read.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static string FormatUpperCaseSpaces(string propertyName)
        {
            //Since the UI uppercases the text, add spaces to make it easier to read.
            Regex regex = new Regex(@"([A-Z0-9])");
            string formattedPropertyName = regex.Replace(propertyName.ToString(), " $1").TrimStart();
            return formattedPropertyName;
        }

        /// <summary>
        /// Creates a numeric config entry. Limited to MCM's support of int and float.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="tooltip"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="label">If not set, will use the property name, adding spaced before each capital letter.</param>
        /// <param name="header"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        protected ConfigValue CreateConfigProperty<T>(string propertyName,
            string tooltip, T min, T max, string label = "", string header = "General") where T: struct
        {
            object defaultValue = AccessTools.Property(ConfigType, propertyName).GetValue(Defaults);
            object propertyValue = AccessTools.Property(ConfigType, propertyName).GetValue(Config);

            string formattedLabel = label == "" ? FormatUpperCaseSpaces(propertyName) : label;

            switch (typeof(T))
            {
                case Type floatType when floatType == typeof(float):

                    return new ConfigValue(propertyName, propertyValue, header, defaultValue, 
                        tooltip, formattedLabel, Convert.ToSingle(min), Convert.ToSingle(max));
                case Type intType when intType == typeof(int):
                    return new ConfigValue(propertyName, propertyValue, header, defaultValue,
                        tooltip, formattedLabel, Convert.ToInt32(min), Convert.ToInt32(max));
                default:
                    throw new ApplicationException($"Unexpected numeric type '{typeof(T).Name}'");
            }
        }

        /// <summary>
        /// Creates a configuration value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="tooltip"></param>
        /// <param name="label">If not set, will use the property name, adding spaced before each capital letter.</param>
        /// <param name="header"></param>
        /// <returns></returns>
        protected ConfigValue CreateConfigProperty(string propertyName,
            string tooltip, string label = "", string header = "General")
        {
            object defaultValue = AccessTools.Property(ConfigType, propertyName).GetValue(Defaults);
            object propertyValue = AccessTools.Property(ConfigType, propertyName).GetValue(Config);

            string formattedLabel = label == "" ? FormatUpperCaseSpaces(propertyName) : label;

            return new ConfigValue(propertyName, propertyValue, header, defaultValue, tooltip, formattedLabel);
        }

        /// <summary>
        /// Sets the T's property that matches the ConfigValue key.
        /// Returns false if the property could not be found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        protected bool SetPropertyValue(string key, object value)
        {
            MethodInfo propertyMethod = AccessTools.Property(typeof(IMcmConfigTarget), key)?.GetSetMethod();


            //Try property
            if (propertyMethod != null)
            {
                propertyMethod.Invoke(Config, new object[] { value });
                return true;
            }

            //Try field.
            if (propertyMethod == null)
            {
                //Try field

                var field = AccessTools.Field(typeof(IMcmConfigTarget), key);
                if(field == null)
                {
                    return false;

                }

                field.SetValue(Config, value);
                return true;
            }

            return false;
        }

        protected virtual bool OnSave(Dictionary<string, object> currenIMcmConfigTarget, out string feedbackMessage)
        {
            feedbackMessage = "";

            foreach (var entry in currenIMcmConfigTarget)
            {
                SetPropertyValue(entry.Key, entry.Value);
            }

            Config.Save();

            return true;
        }
    }
}
