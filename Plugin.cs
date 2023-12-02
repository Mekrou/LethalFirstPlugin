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
        public static Harmony harmony0;

        private const bool allowDestroy = false;

        private void Awake()
        {
            mls = this.Logger;
            harmony0 = new Harmony(modGUID);

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

            harmony0.PatchAll(typeof(Plugin));

            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        [HarmonyPatch(typeof(HUDManager), "SubmitChat_performed")]
        [HarmonyPrefix]
        static void Commands(HUDManager __instance)
        {
            mls.LogInfo("Submit Chat Performed");

            string text = __instance.chatTextField.text;

            foreach (var kvp in CommandService.Commands)
            {
                if (text == kvp.Key)
                {
                    kvp.Value.Run();
                }
            }

            mls.LogInfo($"User typed: {text}");
        }

        private void OnDestroy()
        {
            if (allowDestroy)
            {
                mls.LogInfo($"Unpatching {PluginInfo.PLUGIN_NAME} right now");
                harmony0.UnpatchSelf();
            }
        }
    }
}
