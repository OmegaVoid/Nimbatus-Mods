using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API;
using MonoMod;
using Partiality.Modloader;

namespace OmegaBlocks
{
    [MonoModPatch("global::OmegaBlocks.OmegaBlocks")]
    public class OmegaBlocks : NimbatusMod
    {
        public override string Name => "OmegaBlocks";

        public override string Description => "";

        public override string Author => "OmegaRogue";

        public override Version modVersion => new Version(0, 1, 0, 0);

        public override void OnEnable()
        {

        }
        public override void OnDisable()
        {
            
        }
        
    }
}
