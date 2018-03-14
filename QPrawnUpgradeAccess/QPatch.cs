using Harmony;
using System.Reflection;

namespace QPrawnUpgradeAccess
{
    class QPatch
    {
        public static void Patch()
        {
            HarmonyInstance harmony = HarmonyInstance.Create("qprawn_upgrade_access.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
