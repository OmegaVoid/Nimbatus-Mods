using System;
using System.Collections.Generic;
using System.IO;
using Assets.Nimbatus.GUI.MainMenu.Scripts;
using Assets.Nimbatus.Scripts.Persistence;
using Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts;
using Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.SensorParts;
using MonoMod;
using Newtonsoft.Json.Linq;
using Partiality.Modloader;
using UnityEngine;

#pragma warning disable CS0626
#pragma warning disable CS0649
#pragma warning disable CS0108
namespace API
{
	//public class OmegaModLoader
	//{
	//    //FourLogic ForLogic = new FourLogic();
	//    public NimbatusMod[] modList;
	//    public string ModInfo;
	//    public string modInfo;
	//    string modInfo2;
	//    public void Startup()
	//    {
	//        InitializeMod(modList);
	//    }

	//    public void InitializeMod(params NimbatusMod[] Modds)
	//    {
	//        for (int i = 0; i < Modds.Length; i++)
	//        {
	//            modInfo2 = modInfo;
	//            modList[i].Load(this);
	//            modInfo = modInfo2 + modList[i].ToString() + " ";
	//        }
	//        ModInfo = " Modded Loaded Mods: " + modInfo;
	//    }

	//}
	public class ModAPI : PartialityMod
	{
		public override void Init()
		{
			base.Init();
			ModID = "API";
			author = "OmegaRogue";
			Version = new Version(1, 0, 0, 0).ToString();
			loadPriority = 0;
		}
	}

	[MonoModPatch("global::Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.BindableDronePart")]
	internal abstract class patch_BindableDronePart : BindableDronePart
	{
		[MonoModIgnore] internal List<KeyBinding> KeyBindings;

		// Adds KeyBindings to a BindableDronePart
		/// <summary>
		/// Adds any number of KeyBindings to a BindableDronePart
		/// </summary>
		/// <param name="keys">The KeyBindings to Add</param>
		public virtual void AddKeyBindings(params KeyBinding[] keys)
		{
			KeyBindings.AddRange(keys);
		}

		// Removes KeyBindings from a BindableDronePart
		/// <summary>
		/// Removes any number of KeyBindings from a BindableDronePart
		/// </summary>
		/// <param name="keys">The KeyBindings to Remove</param>
		public virtual void RemoveKeyBindings(params KeyBinding[] keys)
		{
			foreach (var key in keys) KeyBindings.Remove(key);
		}
	}

	[MonoModPatch("global::Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.SensorParts.SensorPart")]
	internal abstract class patch_SensorPart : SensorPart
	{
		[MonoModIgnore] internal List<KeyBinding> EventBindings;

		// Adds EventBindings to a SensorPart
		/// <summary>
		/// Adds any number of EventBindings to a SensorPart
		/// </summary>
		/// <param name="keys">The EventBindings to Add</param>
		public virtual void AddEventBindings(params KeyBinding[] keys)
		{
			EventBindings.AddRange(keys);
		}

		// Removes EventBindings from a SensorPart
		/// <summary>
		/// Removes any number of EventBindings from a SensorPart
		/// </summary>
		/// <param name="keys">The EventBindings to Remove</param>
		public virtual void RemoveEventBindings(params KeyBinding[] keys)
		{
			foreach (var key in keys) EventBindings.Remove(key);
		}
	}

	public static class AssetBundleModule
	{
		public static Dictionary<String, AssetBundle> AssetBundles;

		// Loads a new AssetBundle
		/// <summary>
		/// Loads an AssetBundle into the AssetBundles Dictionary
		/// </summary>
		/// <param name="name">The Name of the AssetBundle to Load</param>
		/// <exception cref="FileLoadException">Thrown when the AssetBundle cannot be found</exception>
		public static void Load(String name)
		{
			var myLoadedAssetBundle =
				AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, name));
			if (myLoadedAssetBundle == null)
			{
				Debug.Log("Failed to load AssetBundle!");
				throw new FileLoadException("Failed to load AssetBundle");
			}

			AssetBundles.Add(name, myLoadedAssetBundle);
		}

		// Unloads an AssetBundle
		/// <summary>
		/// Unloads an AssetBundle from The AssetBundles Dictionary
		/// </summary>
		/// <param name="name">The Name of the AssetBundle to Unload</param>
		/// <param name="unloadAllLoadedObjects"></param>
		/// <exception cref="NullReferenceException">Thrown when there is no AssetBundle with the Specified Name in the AssetBundles Dictionary</exception>
		public static void Unload(String name, bool unloadAllLoadedObjects)
		{
			if (AssetBundles[name] == null)
			{
				Debug.Log("Failed to unload AssetBundle!");
				throw new NullReferenceException("Failed to unload AssetBundle");
			}

			AssetBundles[name].Unload(unloadAllLoadedObjects);
		}

		// Loads a Prefab from a loaded AssetBundle
		/// <summary>
		/// Loads a Prefab from a loaded AssetBundle in the AssetBundles Dictionary
		/// </summary>
		/// <param name="name">The Name of the AssetBundle to Load from</param>
		/// <param name="assetName">The Name of the Prefab to Load</param>
		/// <returns>The Prefab</returns>
		/// <exception cref="TypeLoadException">Thrown when the Prefab doesnt exist</exception>
		public static GameObject LoadPrefabFrom(String name, String assetName)
		{
			var prefab = AssetBundles[name].LoadAsset<GameObject>(assetName);
			if (prefab == null)
			{
				throw new TypeLoadException("Failed to load Prefab");
			}

			return AssetBundles[name].LoadAsset<GameObject>(assetName);
		}
	}

	public class ConfigManager
	{
		private readonly string _folder;

		public ConfigManager(string folder)
		{
			_folder = folder;
		}

		public string Folder => Path.Combine(FolderStructure.ConfigFolder, _folder);
		public string FileExtension => ".json";

		public JObject LoadModConfig(PartialityMod mod)
		{
			if (mod == null) throw new ArgumentNullException(nameof(mod));
			var path = GetModConfigFilePath(mod);
			var json = File.ReadAllText(path);
			return JObject.Parse(json);
		}

		private string GetModConfigFilePath(PartialityMod mod)
		{
			var configName = mod.ModID;
			if (configName == null) throw new ArgumentException("NimbatusMod.Name is null", nameof(mod));
			configName = configName.ToLower();
			var path = Path.Combine(Folder, configName) + FileExtension;
			return path;
		}
	}


	public static class FolderStructure
	{
		public static readonly string RootFolder = Application.dataPath;
		public static readonly string DataFolder = Path.Combine(RootFolder, "OmegaData");
		public static readonly string ModsFolder = Path.Combine(DataFolder, "Mods");
		public static readonly string ConfigFolder = Path.Combine(DataFolder, "Config");
		public static readonly string AssetsFolder = Path.Combine(DataFolder, "Assets");
	}
	//public abstract class NimbatusMod : PartialityMod
	//{
	//    public OmegaModLoader Mods;
	//    public abstract string Name { get; }
	//    public abstract string Description { get; }
	//    public virtual Version modVersion => new Version(0, 0, 1);

	//    public abstract string Author { get; }

	//    //public JObject Config { get; private set; }
	//    public virtual void Load(OmegaModLoader Mods)
	//    {
	//        this.Mods = Mods;
	//        string name = Name;
	//        if (name == null) throw new InvalidOperationException("NimbatusMod.Name returned null");

	//    }
	//    public override string ToString() => Name + " " + Version;

	//    public override void Init()
	//    {
	//        base.Init();
	//        Version = modVersion.ToString();
	//        author = Author;
	//        ModID = Name;
	//    }


	//    //public virtual JObject CreateDefaultConfiguration()
	//    //{
	//    //    return new JObject();
	//    //}

	//}
	[MonoModPatch("global::Assets.Nimbatus.GUI.MainMenu.Scripts.ShowVersionNumber")]
	internal class patch_ShowVersionNumber : ShowVersionNumber
	{
		public int labelSizeAdd;

		// public OmegaModLoader Mod;
		public extern void orig_Update();

		public void Update()
		{
			labelSizeAdd = 0;
			Label.SetDimensions(Label.width + labelSizeAdd, Label.height + labelSizeAdd);
			Label.text = "Version " + SaveGameManager.CurrentGameVersion + " Closed Alpha " +
						 "Modded using OmegaMod"; //+ this.Mod.ModInfo;
		}

		public void Start()
		{
			new Color(10, 10, 10);
			//this.Mod = new OmegaModLoader();
			Debug.Log("Running OmegaMod");
			// this.Mod.Startup();
			//Label.gameObject.AddComponent<ModConfigurator>();

			Debug.Log(Application.streamingAssetsPath);
		}
	}
}