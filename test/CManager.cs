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
        public static LB_Info lib_Info;

        //HACK MODS
        public static H_Main hk_Main;
        public static H_Player hk_Player;
        public static H_Misc hk_Misc;

        public static void injHacks(GameObject go)
        {
            hk_Main = go.AddComponent<H_Main>();
            hk_Player = go.AddComponent<H_Player>();
            hk_Misc = go.AddComponent<H_Misc>();
        }

        public static void injSystem(GameObject go)
        {
            lib_Menu = go.AddComponent<LB_Menu>();
            lib_Info = go.AddComponent<LB_Info>();
        }
    }
}