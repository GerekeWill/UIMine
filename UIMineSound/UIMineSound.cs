using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using System.Reflection;
using BepInEx.Configuration;

namespace UIMineSound
{
    [BepInPlugin("KurisuBot.UIMineSound", "UIMineSound", "1.0.0")]
    public class UIMineSound : BaseUnityPlugin
    {
        public static UIMineSound instance;
        public static ConfigEntry<bool> silenceMines;
        internal ManualLogSource mls;
        private readonly Harmony harmony = new Harmony("KurisuBot.UIMineSound");
        internal static List<AudioClip> UI;
        internal static AssetBundle Bundle;
        internal static string text;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            text = instance.Info.Location.TrimEnd("UIMineSound.dll".ToCharArray());
            mls = BepInEx.Logging.Logger.CreateLogSource("KurisuBot.UIMineSound");
            UIMineSound.UI = new List<AudioClip>();
            UIMineSound.Bundle = AssetBundle.LoadFromFile(UIMineSound.text + "ui");
            if (UIMineSound.Bundle != null)
            {
                UIMineSound.UI = UIMineSound.Bundle.LoadAllAssets<AudioClip>().ToList();
            }
            mls.LogInfo("UIMineSound Loading. . .");
            UIMineSound.silenceMines = base.Config.Bind<bool>("UIMines", "SilenceMines", false, "Silence the passive mine beeping.");
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);
            mls.LogInfo("UIMineSound Loaded!!");
        }
        public static void Log(string message)
        {
            UIMineSound.instance.Logger.LogInfo(message);
        }
    }
}
