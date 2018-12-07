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
    public class OmegaBlocks : PartialityMod
    {
        public override void Init()
        {
            base.Init();
            ModID = "OmegaBlocks";
            author = "OmegaRogue";
            Version = new Version(0, 0, 0, 0).ToString();
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
}
