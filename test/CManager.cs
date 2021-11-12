using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using test.CT_Hacks;
using test.CT_System;


namespace test
{
    public class CManager
    {
        //SYSTEM MODS
        public static LB_Menu lib_Menu;

        //HACK MODS
        public static H_Main hk_Main;
        public static H_Player hk_Player;
        public static H_ItemSpawner hk_itemSpawner;
        public static H_Misc hk_misc;
        public static H_DayCycle hk_daycycle;
        public static H_PowerUp hk_powerUp;
        public static H_Server hk_server;
        public static H_MobSpawner hk_mobspawner;
        public static H_GUIColors hk_guicolors;
        public static H_Waypoints hk_waypoints;

        public static void injHacks(GameObject go)
        {
            hk_Main = go.AddComponent<H_Main>();
            hk_Player = go.AddComponent<H_Player>();
            hk_itemSpawner = go.AddComponent<H_ItemSpawner>();
            hk_misc = go.AddComponent<H_Misc>();
            hk_daycycle = go.AddComponent<H_DayCycle>();
            hk_powerUp = go.AddComponent<H_PowerUp>();
            hk_server = go.AddComponent<H_Server>();
            hk_mobspawner = go.AddComponent<H_MobSpawner>();
            hk_guicolors = go.AddComponent<H_GUIColors>();
            hk_waypoints = go.AddComponent<H_Waypoints>();
        }

        public static void injSystem(GameObject go)
        {
            lib_Menu = go.AddComponent<LB_Menu>();
        }
    }
}