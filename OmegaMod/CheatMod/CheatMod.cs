using System;
using Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.DronePartResources;
using Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.FuelTanks;
using Partiality.Modloader;

namespace CheatMod
{
	public class CheatMod : PartialityMod
	{
		public override void Init()
		{
			base.Init();
			ModID = "CheatMod";
			author = "OmegaRogue";
			Version = new Version(1, 0, 0, 0).ToString();
		}

		public override void OnEnable()
		{
			base.OnEnable();
			// += your hooks
			On.Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.Batteries.Battery.Awake += Battery_Awake;
			On.Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.FuelTanks.FuelTank.Awake += FuelTank_Awake;
			On.Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.DronePartResources.ResourceTank.Update +=
				ResourceTank_Update;
		}

		private void ResourceTank_Update(
			On.Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.DronePartResources.ResourceTank.orig_Update orig,
			ResourceTank self)
		{
			self.SetResourceAmount(self.ResourceCapacity);
			orig(self);
		}

		private void FuelTank_Awake(
			On.Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.FuelTanks.FuelTank.orig_Awake orig, FuelTank self)
		{
			self.CurrentFuelAmount = 1E+19f;
			self.MaxFuelAmount = 1E+19f;
			self.RechargePerSecond = 1E+19f;
			orig(self);
		}

		private void Battery_Awake(
			On.Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.Batteries.Battery.orig_Awake orig,
			Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.Batteries.Battery self)
		{
			self.MaxEnergyAmount = 1E+19f;
			self.CurrentEnergyAmount = 1E+19f;
			self.RechargePerSecond = 1E+19f;
			orig(self);
		}

		public override void OnDisable()
		{
			// -= your hooks (a future Partiality update will do this automatically)
			On.Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.Batteries.Battery.Awake -= Battery_Awake;
			On.Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.FuelTanks.FuelTank.Awake -= FuelTank_Awake;
			On.Assets.Nimbatus.Scripts.WorldObjects.Items.DroneParts.DronePartResources.ResourceTank.Update -=
				ResourceTank_Update;
		}
	}
}