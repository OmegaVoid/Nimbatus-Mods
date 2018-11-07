using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmegaMods;
using API;
// this is an Example Mod, use this to learn how to make a mod or as a template for your mod
namespace ExampleMod
{
    public class ExampleMod : NimbatusMod
    {
        public override string Name => "ExampleMod"; // the name of your Mod
        public override string Description => "Example Mod"; // A description of your Mod
        public override Version Version => new Version(0, 0, 0); // The version of your Mod in the format [major],[minor],[build]
        public override void Load(OmegaModLoader Mods) // the Method called when the mod gets loaded
        {
            base.Load(Mods); // calls the base load method
        }
    }
}
