using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BepInEx;
using BepInEx.Configuration;

namespace LCFriendlyFireSettingsMod.Patches
{
    [HarmonyPatch]
    internal class PlayerControllerBPatch
    {
        [HarmonyPatch(typeof(PlayerControllerB), "DamagePlayerFromOtherClientServerRpc")]
        [HarmonyPrefix]
        public static void DamagePlayerFromOtherClientServerRpc(PlayerControllerB __instance, ref int damageAmount, Vector3 hitDirection, int playerWhoHit)
        {
            if (__instance.isInHangarShipRoom == true && LCFriendlyFireSettingsModBase.configShip.Value == false)
            {
                LCFriendlyFireSettingsModBase.LoggerInstance.LogDebug($"Player{playerWhoHit} attempted to friendly fire in the ship, setting damage amount to 0.");
                damageAmount = 0;
            }
            if (__instance.isInHangarShipRoom == false && __instance.isInsideFactory == false && LCFriendlyFireSettingsModBase.configOutside.Value == false)
            {
                LCFriendlyFireSettingsModBase.LoggerInstance.LogDebug($"Player{playerWhoHit} attempted to friendly fire outside the ship, setting damage amount to 0.");
                damageAmount = 0;
            }
            if (__instance.isInsideFactory == true && LCFriendlyFireSettingsModBase.configFacility.Value == false)
            {
                LCFriendlyFireSettingsModBase.LoggerInstance.LogDebug($"Player{playerWhoHit} attempted to friendly fire in facility, setting damage amount to 0.");
                damageAmount = 0;
            }
            
        }
    }
}
