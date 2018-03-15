using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QPrawnUpgradeAccess.Patches
{
    [HarmonyPatch(typeof(PDA))]
    [HarmonyPatch("Update")]
    class PDA_Awake_Patch
    {
        public static PDA Pda;
        public static void Postfix(PDA __instance)
        {
            Pda = __instance;
        }
    }
}
