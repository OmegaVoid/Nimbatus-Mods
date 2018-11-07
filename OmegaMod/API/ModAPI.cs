using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Nimbatus.GUI.MainMenu.Scripts;
using Assets.Nimbatus.Scripts.Persistence;
using Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts;
using Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.SensorParts;
using MonoMod;
using MonoMod.ModInterop;
using OmegaMods;
using UnityEngine;
#pragma warning disable CS0626
namespace API
{
    public class OmegaModLoader
    {
        //FourLogic ForLogic = new FourLogic();
        public NimbatusMod[] modList;
        public string ModInfo;
        public string modInfo;
        string modInfo2;
        public void Startup()
        {
            InitializeMod(modList);
        }

        public void InitializeMod(params NimbatusMod[] Modds)
        {
            for (int i = 0; i < Modds.Length; i++)
            {
                modInfo2 = modInfo;
                modList[i].Load(this);
                modInfo = modInfo2 + modList[i].ToString() + " ";
            }
            ModInfo = " Modded Loaded Mods: " + modInfo;
        }

    }
    public class ModAPI : NimbatusMod
    {
        public override string Name => "ModAPI";

        public override string Description => "API for Nimbatus Modding";

        public override string Author => "OmegaRogue";
        public override void Load(OmegaModLoader Mods)
        {
            base.Load(Mods);
            typeof(ModExports).ModInterop();
        }

    }
    [MonoModPatch("global::Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.BindableDronePart")]
    abstract class patch_BindableDronePart : BindableDronePart
    {
        [MonoMod.MonoModIgnore]
        internal List<KeyBinding> KeyBindings;
        public virtual void AddKeyBindings(params KeyBinding[] keys)
        {
            this.KeyBindings.AddRange(keys);
        }
        public virtual void RemoveKeyBindings(params KeyBinding[] keys)
        {
            foreach (KeyBinding key in keys)
            {
                this.KeyBindings.Remove(key);
            }
        }
    }
    [MonoModPatch("global::Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.SensorParts.SensorPart")]
    abstract class patch_SensorPart : SensorPart
    {
        [MonoMod.MonoModIgnore]
        internal List<KeyBinding> EventBindings;
        public virtual void AddEventBindings(params KeyBinding[] keys)
        {
            this.EventBindings.AddRange(keys);
        }
        public virtual void RemoveEventBindings(params KeyBinding[] keys)
        {
            foreach (KeyBinding key in keys)
            {
                this.EventBindings.Remove(key);
            }
        }

    }
    public class LoadFromFileExample : MonoBehaviour
    {
        public void Start()
        {
            var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "myassetBundle"));
            if (myLoadedAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                return;
            }

            var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("MyObject");
            Instantiate(prefab);

            myLoadedAssetBundle.Unload(false);
        }
    }
    public static class FolderStructure
    {
        public static readonly string RootFolder = Path.Combine(Application.dataPath, "..");
        public static readonly string DataFolder = Path.Combine(RootFolder, "OmegaData");
        public static readonly string ModsFolder = Path.Combine(DataFolder, "Mods");
        public static readonly string ConfigFolder = Path.Combine(DataFolder, "Config");
    }
    public abstract class NimbatusMod : Partiality.Modloader.PartialityMod
    {
        public OmegaModLoader Mods;
        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual Version modVersion => new Version(0, 0, 1);

        public abstract string Author { get; }

        //public JObject Config { get; private set; }
        public virtual void Load(OmegaModLoader Mods)
        {
            this.Mods = Mods;
            string name = Name;
            if (name == null) throw new InvalidOperationException("NimbatusMod.Name returned null");

        }
        public override string ToString() => Name + " " + Version.ToString();

        public override void Init()
        {
            base.Init();
            Version = modVersion.ToString();
            author = Author;
            ModID = Name;
        }


        //public virtual JObject CreateDefaultConfiguration()
        //{
        //    return new JObject();
        //}

    }
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
}
