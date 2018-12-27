using System;
using Partiality.Modloader;

// this is an Example Mod, use this to learn how to make a mod or as a template for your mod
namespace ExampleMod
{
	public class ExampleMod : PartialityMod
	{
//        public override string Name => "ExampleMod"; // the name of your Mod
//        public override string Description => "Example Mod"; // A description of your Mod
//        public override Version modVersion => new Version(0, 0, 0); // The version of your Mod in the format [major],[minor],[build]
//        public override string Author => "Example Man";

		public override void Init()
		{
			base.Init();
			ModID = "ExampleMod";
			author = "Example Man";
			Version = new Version(0, 0, 0).ToString();
		}

		public override void OnEnable()
		{
			base.OnEnable();
		}

		public override void OnDisable()
		{
			base.OnDisable();
		}

//        public override void Load(OmegaModLoader Mods) // the Method called when the mod gets loaded
//        {
//            base.Load(Mods); // calls the base load method
//        }
	}
}