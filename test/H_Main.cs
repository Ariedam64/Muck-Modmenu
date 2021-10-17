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
            if (GUILayout.Button("Player"))
            {
                CManager.hk_Player.isOpen = !CManager.hk_Player.isOpen;
            }
            if (GUILayout.Button("Item spawner"))
            {
                CManager.hk_itemSpawner.isOpen = !CManager.hk_itemSpawner.isOpen;
            }
            if (GUILayout.Button("Misc"))
            {
                CManager.hk_misc.isOpen = !CManager.hk_misc.isOpen;
            }
            if (GUILayout.Button("Day cycle"))
            {
                CManager.hk_daycycle.isOpen = !CManager.hk_daycycle.isOpen;
            }
            if (GUILayout.Button("PowerUp"))
            {
                CManager.hk_powerUp.isOpen = !CManager.hk_powerUp.isOpen;
            }
            if (GUILayout.Button("Other player"))
            {
                CManager.hk_otherplayer.isOpen = !CManager.hk_otherplayer.isOpen;
            }
            GUILayout.Label("Created by Romann");
            base.runWin(id);
        }


    }
}