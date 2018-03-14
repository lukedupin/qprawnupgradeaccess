using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;

namespace QPrawnUpgradeAccess.Patches
{
    [HarmonyPatch(typeof(Exosuit))]
    [HarmonyPatch("Update")]
    class Exosuit_Update_Patch
    {
        public static void Postfix(Exosuit __instance)
        {
            if ( __instance.GetPilotingMode() )
            {
                if ( GameInput.GetButtonDown(GameInput.Button.Slot1) ||
                     GameInput.GetButtonDown(GameInput.Button.Slot2) )
                {
                    __instance.upgradesInput.OpenFromExternal();
                }

                //Access my storage
                if ( GameInput.GetButtonDown(GameInput.Button.AltTool) )
                {
                    __instance.storageContainer.Open();
                }
            }
        }
    }

}
