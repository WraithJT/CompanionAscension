//Mostly taken from Vek17's Tabletop Tweaks: https://github.com/Vek17/TabletopTweaks-Core
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Utility;
using System;
using System.Linq;
using System.Collections.Generic;
using HarmonyLib;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Components;
using System.Data;
using static Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite;
using UnityEngine;
using CompanionAscension.Utilities.TTTCore;
using CompanionAscension.NewContent.Components;

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
