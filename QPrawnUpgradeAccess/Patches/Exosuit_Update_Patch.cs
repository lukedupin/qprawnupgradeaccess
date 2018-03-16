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
                var pda = PDA_Awake_Patch.Pda;

                //Access the ugprade
                foreach ( var button in QPatch.UpgradeButtons )
                    if ( GameInput.GetButtonDown(button ) )
                    {
                        if (pda != null && pda.isOpen)
                            pda.Close();
                        else
                            __instance.upgradesInput.OpenFromExternal();
                        return;
                    }

                //Access my storage
                foreach ( var button in QPatch.StorageButtons )
                    if ( GameInput.GetButtonDown(button) )
                    {
                        if (pda != null && pda.isOpen)
                            pda.Close();
                        else
                            __instance.storageContainer.Open();
                        return;
                    }
            }
        }
    }

}
