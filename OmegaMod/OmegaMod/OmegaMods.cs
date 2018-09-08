using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Mono.Cecil;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
namespace OmegaMods
{

    public class OmegaModLoader
    {
        //FourLogic ForLogic = new FourLogic();
        OmegaMod OmegaMood = new OmegaMod();
        public NimbatusMod[] modList;
        public string ModInfo;
        public string modInfo;
        string modInfo2;
        public void Startup()
        {
            InitializeMod(OmegaMood);
        }

        public void InitializeMod(params NimbatusMod[] Modds)
        {
            modList = Modds;
            for (int i = 0; i < modList.Length; i++)
            {
                modInfo2 = modInfo;
                modList[i].Load(this);
                modInfo = modInfo2 + modList[i].ToString() + "\n";
            }
            // OmegaMood.Load(this);
            ModInfo = "Modded\nLoaded Mods:\n" + modInfo;
        }

    }
    
    public static class FolderStructure
    {
        public static readonly string RootFolder = Path.Combine(Application.dataPath, "..");
        public static readonly string DataFolder = Path.Combine(RootFolder, "OmegaData");
        public static readonly string ModsFolder = Path.Combine(DataFolder, "Mods");
        public static readonly string ConfigFolder = Path.Combine(DataFolder, "Config");
    }
    public class OmegaMod:NimbatusMod
    {
        public override string Name => "OmegaMod";
        public override string Description => "Base Mod";
        public override Version Version => new Version(0, 1, 8, 3);
        public override void Load(OmegaModLoader Mods)
        {
            base.Load(Mods);
        }

    }
    //public class FourLogic:NimbatusMod
    //{
    //    public override string Name => "FourLogic";
    //    public override string Description => "Logic Gates With Up to 4 Input";
    //    public override Version Version => new Version(-1, -1, -1, -1);
    //    public override void Load(OmegaModLoader Mods)
    //    {
    //        base.Load(Mods);
    //    }

    //}
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
                internal static void Starting(Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.Batteries.Battery Bat, float MaxEnergyAmount, float CurrentEnergyAmount, float RechargePerSecond)
                {
                    //do something
                }
                internal static void Awaken(Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.Batteries.Battery Bat, float MaxEnergyAmount, float CurrentEnergyAmount, float RechargePerSecond)
                {
                    Bat.MaxEnergyAmount = 1E+19f;
                    Bat.CurrentEnergyAmount = 1E+19f;
                    Bat.RechargePerSecond = 1E+19f;
                    //do something
                }
                internal static void Updateing(Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.Batteries.Battery Bat, float MaxEnergyAmount, float CurrentEnergyAmount, float RechargePerSecond)
                {
                    //do something
                }
            }
            public static class FeulTank
            {
                public static void Starting(Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.FuelTanks.FuelTank Fuel)
                {
                    //do something
                }
                public static void Awaken(Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.FuelTanks.FuelTank Fuel)
                {
                    Fuel.CurrentFuelAmount = 1E+19f;
                    Fuel.MaxFuelAmount = 1E+19f;
                    Fuel.RechargePerSecond = 1E+19f;
                    //do something
                }
                public static void Updateing(Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.FuelTanks.FuelTank Fuel)
                {
                    //do something
                }
            }

            public static class ResuorceTank
            {
                public static void Starting(Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.DronePartResources.ResourceTank Vsauce)
                {
                    //do something
                }
                public static void Awaken(Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.DronePartResources.ResourceTank Vsauce)
                {
                    //do something
                }
                public static void Updateing(Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.DronePartResources.ResourceTank Vsauce)
                {
                    //do something
                }
            }
            
            public static class EnergieShield
            {
                public static void Starting(Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.DefensiveParts.EnergyShield Shield)
                {
                    Shield.SizePerSecond = 1f;
                    //do something
                }
                public static void Updateing(Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.DefensiveParts.EnergyShield Shield)
                {
                    //do something
                }
            }

        }
    }
    public abstract class NimbatusMod
    {
        private OmegaModLoader Modss;
        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual Version Version => new Version(0, 1, 1);
        //public JObject Config { get; private set; }

        public virtual void Load(OmegaModLoader Mods)
        {
            Modss = Mods;
            string name = Name;
            if (name == null) throw new InvalidOperationException("NimbatusMod.Name returned null");
            
        }
        public override string ToString() => Name + " " + Version.ToString();

        public virtual void Unload()
        { 
           
        }

        //public virtual JObject CreateDefaultConfiguration()
        //{
        //    return new JObject();
        //}

    }
}
