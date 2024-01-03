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
using BepInEx.Configuration;
using System.IO;

namespace LCFriendlyFireSettingsMod
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class LCFriendlyFireSettingsModBase : BaseUnityPlugin
    {
        private const string modGUID = "Snowlance.LCFriendlyFireSettingsMod";
        private const string modName = "Friendly Fire Settings Mod";
        private const string modVersion = "1.0.4";

        private readonly Harmony harmony = new Harmony(modGUID);
        private static LCFriendlyFireSettingsModBase Instance;
        public static ManualLogSource LoggerInstance { get; private set; }

        public static ConfigEntry<bool> configShip;
        public static ConfigEntry<bool> configOutside;
        public static ConfigEntry<bool> configFacility;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            LoggerInstance = this.Logger;
            LoggerInstance.LogInfo($"Plugin {modName} loaded successfully.");

            configShip = Config.Bind("General", "ShipFriendlyFire", false, "Can crewmates damage each other inside the ship?");
            configOutside = Config.Bind("General", "OutsideFriendlyFire", false, "Can crewmates damage each other outside the ship?");
            configFacility = Config.Bind("General", "FacilityFriendlyFire", false, "Can crewmates damage each other inside the facility?");

            harmony.PatchAll(typeof(LCFriendlyFireSettingsModBase));
            harmony.PatchAll(typeof(PlayerControllerBPatch));
        }
    }
}
