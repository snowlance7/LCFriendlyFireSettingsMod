using BepInEx;
using System;
using System.Collections.Generic;
using BepInEx.Logging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using GameNetcodeStuff;
using LCFriendlyFireSettingsMod.Patches;

namespace LCFriendlyFireSettingsMod
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class LCFriendlyFireSettingsModBase : BaseUnityPlugin
    {
        private const string modGUID = "Posiedon.LCFriendlyFireSettingsMod";
        private const string modName = "Friendly Fire Settings Mod";
        private const string modVersion = "1.0.1";

        private readonly Harmony harmony = new Harmony(modGUID);
        private static LCFriendlyFireSettingsModBase Instance;
        public static ManualLogSource LoggerInstance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            LoggerInstance = this.Logger;
            LoggerInstance.LogInfo($"Plugin {modName} loaded successfully.");
            harmony.PatchAll(typeof(LCFriendlyFireSettingsModBase));
            harmony.PatchAll(typeof(PlayerControllerBPatch));
        }
    }
}
