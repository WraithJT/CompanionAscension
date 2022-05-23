//Mostly taken from Vek17's Tabletop Tweaks: https://github.com/Vek17/TabletopTweaks-Core
using UnityEngine;

namespace CompanionAscension.Utilities
{
    public static class Tools
    {
        public static void LogMessage(string msg)
        {
            Main.logger.Log(msg);
        }

        public static void AddGUIOption(string name, string description, ref bool setting)
        {
            GUILayout.BeginVertical();
            //GUILayout.Space(5);
            GUILayout.EndVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Space(15);
            name = " " + name;
            int len = name.Length;
            do
            {
                name += "\t";
                if (name.Length >= 50) { break; }
                len += 10;
            } while (len < 49);
            name += description;
            setting = GUILayout.Toggle(setting, name, GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
        }
    }
}
