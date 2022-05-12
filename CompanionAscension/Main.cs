using HarmonyLib;
using UnityEngine;
using UnityModManagerNet;
using CompanionAscension.Utilities;
using CompanionAscension.Utilities.TTTCore;

namespace CompanionAscension
{
    static class Main
    {
        internal static UnityModManager.ModEntry.ModLogger logger;
        public static Settings Settings;
        public static bool Enabled;

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            var harmony = new Harmony(modEntry.Info.Id);
            logger = modEntry.Logger;
            Settings = Settings.Load<Settings>(modEntry);
            modEntry.OnToggle = OnToggle;
            modEntry.OnGUI = OnGUI;
            modEntry.OnSaveGUI = OnSaveGUI;
            harmony.PatchAll();
            return true;
        }

        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            Enabled = value;
            return true;
        }

        static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("GAME MUST BE RESTARTED FOR CHANGES TO TAKE EFFECT");
            GUILayout.EndHorizontal();

            Tools.AddGUIOption("Companion Ascension",
                "Enables the Ascension of Companions",
                ref Settings.useCompanionAscension);
        }

        static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            Settings.Save(modEntry);
        }
    }
}
