using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace test.CT_Hacks
{
    public class H_Main : Menu
    {
        public H_Main() : base(new Rect(10, 10, 200, 200), "ModMenu un peu nul", 0, true) { }

        public override void runWin(int id)
        {
            if (GUILayout.Button("Player Menu"))
            {
                CManager.hk_Player.isOpen = !CManager.hk_Player.isOpen;
            }
            if (GUILayout.Button("Misc Menu"))
            {
                CManager.hk_Misc.isOpen = !CManager.hk_Misc.isOpen;
            }
            GUILayout.Label("Created by Ariedam");
            base.runWin(id);
        }


    }
}