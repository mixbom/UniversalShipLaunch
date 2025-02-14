using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace UniversalShipLaunch
{
    [BepInPlugin("com.mixbom.universalshiplaunch", "UniversalShipLaunch", "1.0.0")]
    public class UniversalShipLaunchPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            var harmony = new Harmony("com.mixbom.universalshiplaunch");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(StartMatchLever))]
    [HarmonyPatch("Start", MethodType.Normal)]
    public class StartMatchLeverPatch
    {
        static void Postfix(StartMatchLever __instance)
        {
            __instance.triggerScript.hoverTip = "[ Click to launch ship ]";
            __instance.triggerScript.interactable = true;
        }
    }

    [HarmonyPatch(typeof(StartMatchLever))]
    [HarmonyPatch("PullLever", MethodType.Normal)]
    public class PullLeverPatch
    {
        static void Prefix(StartMatchLever __instance)
        {
            if (__instance.leverHasBeenPulled)
            {
                __instance.StartGame();
            }
            else
            {
                __instance.EndGame();
            }
        }
    }
}