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
using Partiality;
using MonoMod.ModInterop;
using MonoMod;
using Assets.Nimbatus.GUI.MainMenu.Scripts;
using Assets.Nimbatus.Scripts.Persistence;
using Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.Batteries;
using Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.FuelTanks;
using Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.DefensiveParts;
using Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts;
using Assets.Nimbatus.GUI.Common.Scripts;
using Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.Thruster;
#pragma warning disable CS0626
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
            //InitializeMod(modList);
            InitializeMod(OmegaMood);
        }

        public void InitializeMod(params NimbatusMod[] Modds)
        {
            modList = Modds;
            for (int i = 0; i < modList.Length; i++)
            {
                modInfo2 = modInfo;
                modList[i].Load(this);
                modInfo = modInfo2 + modList[i].ToString() + " ";
            }
            OmegaMood.Load(this);
            ModInfo = " Modded Loaded Mods: " + modInfo;
        }

    }

    public static class FolderStructure
    {
        public static readonly string RootFolder = Path.Combine(Application.dataPath, "..");
        public static readonly string DataFolder = Path.Combine(RootFolder, "OmegaData");
        public static readonly string ModsFolder = Path.Combine(DataFolder, "Mods");
        public static readonly string ConfigFolder = Path.Combine(DataFolder, "Config");
    }
    public class OmegaMod : NimbatusMod
    {
        public override string Name => "OmegaMod";
        public override string Description => "Base Mod";
        public override Version Version => new Version(0, 2, 0, 0);
        public override string Author => "OmegaRogue";
        public override void Load(OmegaModLoader Mods)
        {
            base.Load(Mods);
            typeof(ModExports).ModInterop();
        }
        //public override void Init()
        //{
        //    base.Init();
           
            
        //}
        //public override void OnLoad()
        //{
        //    base.OnLoad();
        //}
        //public override void OnDisable()
        //{
        //    base.OnDisable();
        //}
        //public override void OnEnable()
        //{
        //    base.OnEnable();
        //}
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


    public abstract class NimbatusMod /*: Partiality.Modloader.PartialityMod*/
    {
        public OmegaModLoader Mods;
        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual Version Version => new Version(0, 0, 1);

        public abstract string Author { get; }

        //public JObject Config { get; private set; }
        public virtual void Load(OmegaModLoader Mods)
        {
            this.Mods = Mods;
            string name = Name;
            if (name == null) throw new InvalidOperationException("NimbatusMod.Name returned null");

        }
        public override string ToString() => Name + " " + Version.ToString();



        //public virtual JObject CreateDefaultConfiguration()
        //{
        //    return new JObject();
        //}

    }

    #region Hookers
    [MonoModPatch("global::Assets.Nimbatus.GUI.MainMenu.Scripts.ShowVersionNumber")]
    class patch_ShowVersionNumber : ShowVersionNumber
    {
        
        public int labelSizeAdd;
        public OmegaModLoader Mod;
        public extern void orig_Update();
        public void Update()
        {
            this.labelSizeAdd = 10;
            this.Label.SetDimensions(this.Label.width + this.labelSizeAdd, this.Label.height + this.labelSizeAdd);
            this.Label.text = "Version " + SaveGameManager.CurrentGameVersion + " Closed Alpha " + this.Mod.ModInfo;
        }
        public void Start()
        {
            this.Mod = new OmegaModLoader();
            Debug.Log("Running OmegaMod");
            this.Mod.Startup();
        }
    }
    [MonoModPatch("global::Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.Batteries.Battery")]
    class patch_Battery : Battery
    {
        public extern void orig_Awake();
        public void Awake()
        {
            this.MaxEnergyAmount = 1E+19f;
            this.CurrentEnergyAmount = 1E+19f;
            this.RechargePerSecond = 1E+19f;
            orig_Awake();
        }
    }
    [MonoModPatch("global::Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.FuelTanks.FuelTank")]
    class patch_FuelTank : FuelTank
    {
        public extern void orig_Awake();
        public void Awake()
        {
            this.CurrentFuelAmount = 1E+19f;
            this.MaxFuelAmount = 1E+19f;
            this.RechargePerSecond = 1E+19f;
            orig_Awake();
        }
    }
    [MonoModPatch("global::Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.DefensiveParts.EnergyShield")]
    class patch_EnergyShield : EnergyShield
    {
        [MonoMod.MonoModIgnore]
        private KeyBinding _activateShield;

        public KeyBinding _increaseSize;

        public KeyBinding _decreaseSize;
        public float SizePerSecond;
        public extern void orig_GetKeyBindings();
        public override List<KeyBinding> GetKeyBindings()
        {
            this._activateShield = new KeyBinding("Activate", KeyCode.None);
            this._increaseSize = new KeyBinding("Grow", KeyCode.None);
            this._decreaseSize = new KeyBinding("Shrink", KeyCode.None);
            return new List<KeyBinding>
            {
                this._activateShield,
                this._increaseSize,
                this._decreaseSize
            };
        }
        public extern string orig_GetDetailedToolTip();
        public override string GetDetailedTooltip()
        {
            string text = base.GetDetailedTooltip() + LabelHelper.NewLine;
            string text2 = text;
            text = string.Concat(new object[]
            {
                text2,
                LabelHelper.White,
                "Shield Size: ",
                LabelHelper.Orange,
                this.ShieldSize,
                LabelHelper.NewLine,
                LabelHelper.White,
                "Growth Rate: ",
                LabelHelper.Orange,
                this.SizePerSecond,
                LabelHelper.NewLine
            });
            text2 = text;
            return string.Concat(new object[]
            {
                text2,
                LabelHelper.White,
                "Energy per Second: ",
                LabelHelper.Orange,
                this.EnergyPerSecond
            });
        }
        public extern void orig_Update();
        public override void Update()
        {
            if (this.SizePerSecond == 0f)
            {
                this.SizePerSecond = 1f;
            }
            if (this._increaseSize.IsPressed(this.KeyEventHub))
            {
                this.ShieldSize += this.SizePerSecond;
            }
            if (this._decreaseSize.IsPressed(this.KeyEventHub))
            {
                this.ShieldSize -= this.SizePerSecond;
            }
            orig_Update();
        }
    }
    [MonoModPatch("global::Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.Thruster.Thruster")]
    class patch_Thruster : Thruster
    {
        [MonoMod.MonoModIgnore]
        private KeyBinding _giveThrust;
        private KeyBinding _reverseThrust;

        public float normForce = 100f;

        public override List<KeyBinding> GetKeyBindings()
        {
            this._giveThrust = new KeyBinding("Activate", KeyCode.W);
            this._reverseThrust = new KeyBinding("Reverse", KeyCode.None);
            if (this.ChargeUp)
            {
                return new List<KeyBinding>
                {
                    this._giveThrust,
                 this._reverseThrust
                };
            }
            else
            {
                return new List<KeyBinding>
                {
                    this._giveThrust
                };
            }
        }
        public extern void orig_FixedUpdate();
        public override void FixedUpdate()
        {
            if(this.ChargeUp)
            {
                if(this._reverseThrust.IsPressed(this.KeyEventHub))
                {
                    this.Force = -normForce;
                } else
                {
                    this.Force = normForce;
                }
            }
            orig_FixedUpdate();
        }
    }
    #endregion

    [ModExportName("OmegaMod")] // Defaults to the mod assembly name.
    public static class ModExports
    {
        // Methods are exported.
    }
}
