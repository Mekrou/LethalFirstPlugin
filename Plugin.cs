using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace LethalFirstPlugin
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private const string modGUID = "tk.mekrou.lethalfirstplugin";
        private const string modName = "Mekrou's first plugin";
        private const string modVersion = "1.0.0.0";

        public static ManualLogSource mls;

        private void Awake()
        {
            mls = this.Logger;

            // Plugin startup logic
            //var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Assembly.GetExecutingAssembly().Location, "myassetbundle"));
            string dataFolder = Assembly.GetExecutingAssembly().Location.Replace("LethalFirstPlugin.dll", "data.lethalfirstplugin");
            string assetBundlePath = Path.Combine(dataFolder, "pillbundle");

            var myLoadedAssetBundle = AssetBundle.LoadFromFile(assetBundlePath);
            if (myLoadedAssetBundle == null)
            {
                Logger.LogError("Failed to load AssetBundle!");
                return;
                
            }

            var harmony = new Harmony(modGUID);
            harmony.PatchAll(typeof(Plugin));

            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        [HarmonyPatch(typeof(HUDManager), "SubmitChat_performed")]
        [HarmonyPrefix]
        static void Commands(HUDManager __instance)
        {
            mls.LogInfo("Submit Chat Performed");
        }
    }
}
