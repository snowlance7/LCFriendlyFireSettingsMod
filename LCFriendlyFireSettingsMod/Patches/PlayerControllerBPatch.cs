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

namespace LCFriendlyFireSettingsMod.Patches
{
    [HarmonyPatch]
    internal class PlayerControllerBPatch
    {
        [HarmonyPatch(typeof(PlayerControllerB), "DamagePlayerFromOtherClientServerRpc")]
        [HarmonyPrefix]
        public static void DamagePlayerFromOtherClientServerRpc(PlayerControllerB __instance, ref int damageAmount, Vector3 hitDirection, int playerWhoHit)
        {
            if (__instance.isInsideFactory != true)
            {
                LCFriendlyFireSettingsModBase.LoggerInstance.LogDebug($"Player{playerWhoHit} attempted to friendly fire, setting damage amount to 0.");
                damageAmount = 0;
            }
        }
    }
}
