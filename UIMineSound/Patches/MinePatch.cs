using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine;


namespace UIMineSound.Patches
{
    [HarmonyPatch(typeof(Landmine))]
    internal class MinePatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        static void ReplaceSound(Landmine __instance, ref AudioSource ___mineAudio, ref AudioSource ___mineFarAudio, ref Animator ___mineAnimator)
        {
            if (UIMineSound.silenceMines.Value.Equals(true))
            {
                ___mineAudio.enabled = false;
                ___mineFarAudio.enabled = false;
                ___mineAnimator.speed = 0f;
            }
            __instance.mineTrigger = UIMineSound.UI[0];
        }
        [HarmonyPatch("PressMineServerRpc")]
        [HarmonyPrefix]
        static void TurnBackOn(ref AudioSource ___mineAudio, ref AudioSource ___mineFarAudio, ref Animator ___mineAnimator)
        {
            ___mineAnimator.speed = 1f;
            ___mineAudio.enabled = true;
            ___mineAudio.pitch = 1f;
            ___mineFarAudio.enabled = true;
        }
    }
}