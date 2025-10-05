using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace UnfairTails;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
        
    private void Awake()
    {
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

        HarmonyLib.Harmony harmony = new (MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll();
    }

    [HarmonyLib.HarmonyPatch(typeof(MessageManager), "GetVisibleMessages")]
    public class MessageManager_GetVisibleMessages_Patch
    {
        private static void Postfix(ref string[] __result)
        {
            for (int j = 0; j < __result.Length; j++)
            {
                __result[j] = __result[j].Replace("tails", "temp").Replace("Tails", "Temp").Replace("TAILS", "TEMP");
                __result[j] = __result[j].Replace("heads", "tails").Replace("Heads", "Tails").Replace("HEADS", "TAILS");
                __result[j] = __result[j].Replace("temp", "heads").Replace("Temp", "Heads").Replace("TEMP", "HEADS");
            }
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(GenericMessage), "SetMessage")]
    public class GenericMessage_SetMessage_Patch
    {
        private static void Prefix(ref string message)
        {
            message = message.Replace("tails", "temp").Replace("Tails", "Temp").Replace("TAILS", "TEMP");
            message = message.Replace("heads", "tails").Replace("Heads", "Tails").Replace("HEADS", "TAILS");
            message = message.Replace("temp", "heads").Replace("Temp", "Heads").Replace("TEMP", "HEADS");
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(HeadsChanceCounter), "Update")]
    public class HeadsChanceCounter_Update_Patch
    {
        private static void Postfix(HeadsChanceCounter __instance)
        {
            var textField = (TMPro.TMP_Text)AccessTools.Field(typeof(HeadsChanceCounter), "text").GetValue(__instance);
            textField.text = textField.text.Replace("TAILS", "TEMP").Replace("HEADS", "TAILS").Replace("TEMP", "HEADS");
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(FlipResultMessage), "ShowResult")]
    public class FlipResultMessagePatch
    {
        private static void Postfix(FlipResultMessage __instance)
        {
            var textField = (TMPro.TMP_Text)AccessTools.Field(typeof(FlipResultMessage), "text").GetValue(__instance);
            textField.text = textField.text.Replace("TAILS", "TEMP").Replace("HEADS", "TAILS").Replace("TEMP", "HEADS");
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(ShopButton), "Update")]
    public class ShopButtonPatch
    {
        private static void Postfix(ShopButton __instance)
        {
            var textField = (TMPro.TMP_Text)AccessTools.Field(typeof(ShopButton), "text").GetValue(__instance);
            textField.text = textField.text.Replace("TAILS", "TEMP").Replace("HEADS", "TAILS").Replace("TEMP", "HEADS");
        }
    }
}
