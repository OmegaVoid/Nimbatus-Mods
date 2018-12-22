using System;
using System.Collections.Generic;
using Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts;
using Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.DefensiveParts;
using Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.Thruster;
using MonoMod;
using MonoMod.ModInterop;
using Partiality.Modloader;
using UnityEngine;

#pragma warning disable CS0626
namespace OmegaMods
{
	public class OmegaMod : PartialityMod
	{
		public override void Init()
		{
			base.Init();
			ModID = "OmegaMod";
			author = "OmegaRogue";
			Version = new Version(1, 0, 0, 0).ToString();
		}

		public override void OnLoad()
		{
			base.OnLoad();
		}

		public override void OnEnable()
		{
			base.OnEnable();
			// += your hooks
		}

		public override void OnDisable()
		{
			base.OnDisable();
			// -= your hooks (a future Partiality update will do this automatically)
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


	#region Hookers

	[MonoModPatch("global::Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.DefensiveParts.EnergyShield")]
	class patch_EnergyShield : EnergyShield
	{
		[MonoMod.MonoModIgnore] private KeyBinding _activateShield;

		public KeyBinding _decreaseSize;

		public KeyBinding _increaseSize;
		public float SizePerSecond;
		public extern void orig_Start();

		public override void Start()
		{
			this.SizePerSecond = 1f;
			orig_Start();
			AddKeyBindings();
		}

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

		//public override string GetDetailedTooltip()
		//{
		//    string text = base.GetDetailedTooltip() + LabelHelper.NewLine;
		//    string text2 = text;
		//    text = string.Concat(new object[]
		//    {
		//        text2,
		//        LabelHelper.White,
		//        "Shield Size: ",
		//        LabelHelper.Orange,
		//        this.ShieldSize,
		//        LabelHelper.NewLine
		//    });
		//    text2 = text;
		//    text = string.Concat(new object[]
		//    {
		//        text2,
		//        LabelHelper.White,
		//        "Growth Rate: ",
		//        LabelHelper.Orange,
		//        this.SizePerSecond,
		//        LabelHelper.NewLine
		//    });
		//    text2 = text;
		//    return string.Concat(new object[]
		//    {
		//        text2,
		//        LabelHelper.White,
		//        "Energy per Second: ",
		//        LabelHelper.Orange,
		//        this.EnergyPerSecond
		//    });
		//}
		public extern void orig_Update();

		public override void Update()
		{
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
		[MonoMod.MonoModIgnore] private KeyBinding _giveThrust;

		private KeyBinding _reverseThrust;


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
			if (this.ChargeUp)
			{
				if (this._reverseThrust.IsPressed(this.KeyEventHub))
				{
					this.Force = -100f;
				}
				else
				{
					this.Force = 100f;
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