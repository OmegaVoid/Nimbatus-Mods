using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace OmegaMod
{

    public class OmegaMod
    {
        public NimbatusMod[] modList;
        public string ModInfo;
        public void InitializeMod()
        {

            ModInfo = "Modded\nLoaded Mods:\n" + modList.ToString();
            return;
        }

    }
    

    public static class FolderStructure
    {
        public static readonly string RootFolder = Path.Combine(UnityEngine.Application.dataPath, "..");
        public static readonly string DataFolder = Path.Combine(RootFolder, "OmegaData");
        public static readonly string ModsFolder = Path.Combine(DataFolder, "Mods");
        public static readonly string ConfigFolder = Path.Combine(DataFolder, "Config");
    }


    //public class Settings
    //{
    //    public static bool EnableEnergyCheat = true;
    //    public static bool EnableFuelCheat = true;
    //    public static bool EnableShieldMod = true;
    //    public static bool EnableResourceMod = true;
    //    public static string FileName = "Settings";
    //    public static string FileExtension => ".json";
    //    public static string Folder => Path.Combine(FolderStructure.DataFolder);
    //    public static string settings;
    //    public static string defaultSettings;
    //    public static string file = Settings.FileName + Settings.FileExtension;
    //    //public static void CreateSettings()
    //    //{

    //    //    if (!File.Exists(Settings.file))
    //    //    {
    //    //        File.CreateText(Settings.file);
    //    //        File.WriteAllText(Settings.file, Settings.defaultSettings);
    //    //    }
    //    //    settings = File.ReadAllText(Settings.file);
    //    //}
        

    //}
    public static class Hooks
    {
        public static class DroneParts
        {
            public static class Bettery
            {
                public static void Strat()
                {

                }
                public static void Unpdate()
                {

                }
            }
            public static class FuelTanc
            {
                public static void Strat()
                {

                }
                public static void Unpdate()
                {

                }
            }

            public static class EnergieShield
            {
                public static void Strat()
                {

                }
                public static void Unpdate()
                {

                }
            }

            public static class Thurster
            {
                public static void Strat()
                {

                }
                public static void Unpdate()
                {

                }
            }
        }
    }
    public abstract class NimbatusMod
    {
        private OmegaMod Modss;
        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual Version Version => new Version(0, 1, 1);
        public JObject Config { get; private set; }

        public virtual void Load(OmegaMod Mods)
        {
            Modss = Mods;
            string name = Name;
            if (name == null) throw new InvalidOperationException("NimbatusMod.Name returned null");
            Mods.modList.Append(this);
        }
        public override string ToString() => Name + Version.ToString();

        public virtual void Unload()
        {
            Modss.modList.
        }

        //public virtual JObject CreateDefaultConfiguration()
        //{
        //    return new JObject();
        //}

    }
}
