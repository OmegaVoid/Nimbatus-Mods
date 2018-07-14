using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace OmegaMod
{
    public class StartMod
    {
        public StartMod()
        {
            Settings.CreateSettings();
        }
    }
    public static class FolderStructure
    {
        public static readonly string RootFolder = Path.Combine(Application.dataPath, "..");
        public static readonly string DataFolder = Path.Combine(RootFolder, "OmegaData");
        public static readonly string ModsFolder = Path.Combine(DataFolder, "Mods");
        public static readonly string ConfigFolder = Path.Combine(DataFolder, "Config");
    }

    internal class Application
    {
        public static string dataPath { get; internal set; }
    }

    public class Settings
    {
        public static bool EnableEnergyCheat = true;
        public static bool EnableFuelCheat = true;
        public static bool EnableShieldMod = true;
        public static bool EnableResourceMod = true;
        public static string FileName = "Settings";
        public static string FileExtension => ".cfg";
        public static string Folder => Path.Combine(FolderStructure.DataFolder);
        public static string settings;
        public static string defaultSettings;
        public static string file = Settings.FileName + Settings.FileExtension;
        public static void CreateSettings()
        {
            
            if (!File.Exists(Settings.file))
            {
                File.CreateText(Settings.file);
                File.WriteAllText(Settings.file, Settings.defaultSettings);
            }
            settings = File.ReadAllText(Settings.file);
        }
        //public JObject LoadModConfig(TerraTechMod mod)
        //{
        //    if (mod == null) throw new ArgumentNullException(nameof(mod));
        //    string path = GetModConfigFilePath(mod);
        //    if (File.Exists(path))
        //    {
        //        string json = File.ReadAllText(path);
        //        return JObject.Parse(json);
        //    }
        //    else
        //    {
        //        JObject fallback = mod.CreateDefaultConfiguration();
        //        File.WriteAllText(path, fallback.ToString(Formatting.Indented));
        //        return fallback;
        //    }
        //}

        //private string GetModConfigFilePath(NimbatusMod mod)
        //{
        //    string configName = mod.Name;
        //    if (configName == null) throw new ArgumentException("NimbatusMod.Name is null", nameof(mod));
        //    configName = configName.ToLower();
        //    string path = Path.Combine(Folder, configName) + FileExtension;
        //    return path;
        //}

    }
    public class BaseClass
	{
		public int SomeInt()
		{
			return 0;
		}
	}
    public class test
    {

        public int testint()
        {
            return 0;
        }
    }
    public static class Hooks
    {
        public static class DroneParts
        {
            public static class Bettery
            {
            }
            public static class FuelTanc
            {
            }

            public static class EnergieShield
            {
            }

            public static class Thurster
            {
            }
        }
    }
    //public abstract class NimbatusMod
    //{
    //    public abstract string Name { get; }
    //    public abstract string Description { get; }
    //    public virtual Version Version => new Version(0, 0, 0);
    //    public JObject Config { get; private set; }

    //    public virtual void Load()
    //    {
    //        string name = Name;
    //        if (name == null) throw new InvalidOperationException("NimbatusMod.Name returned null");
    //        Config = NuterraApi.Configuration.LoadModConfig(this);
    //    }

    //    public virtual void Unload()
    //    {
    //    }

    //    public virtual JObject CreateDefaultConfiguration()
    //    {
    //        return new JObject();
    //    }
        
    //}
}
