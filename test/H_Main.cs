using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

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
            if (GUILayout.Button("Item spawner Menu"))
            {
                CManager.hk_itemSpawner.isOpen = !CManager.hk_itemSpawner.isOpen;
            }
            GUILayout.Label("Created by Romann");
            base.runWin(id);
        }


    }
}