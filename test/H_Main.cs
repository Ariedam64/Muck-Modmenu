using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace test.CT_Hacks
{
    public class H_Main : Menu
    {
        
        public H_Main() : base(new Rect(10, 10, 200, 100), "MAH v0.2", 0, true) { }


        public override void runWin(int id)
        {
            GUI.backgroundColor = H_GUIColors.GUIBackgroundColor;
            GUI.contentColor = H_GUIColors.GUIFrontColor;
            if (GUILayout.Button("Player"))
            {
                CManager.hk_Player.isOpen = !CManager.hk_Player.isOpen;
            }
            if (GUILayout.Button("Server"))
            {
                CManager.hk_server.isOpen = !CManager.hk_server.isOpen;
            }

            if (GUILayout.Button("Mob Spawner [HOST]"))
            {
                CManager.hk_mobspawner.isOpen = !CManager.hk_mobspawner.isOpen;
            }
            if (GUILayout.Button("Item spawner"))
            {
                CManager.hk_itemSpawner.isOpen = !CManager.hk_itemSpawner.isOpen;
            }
            if (GUILayout.Button("PowerUp Spawner"))
            {
                CManager.hk_powerUp.isOpen = !CManager.hk_powerUp.isOpen;
            }
            if (GUILayout.Button("Day cycle"))
            {
                CManager.hk_daycycle.isOpen = !CManager.hk_daycycle.isOpen;
            }
            if (GUILayout.Button("Waypoints"))
            {
                CManager.hk_waypoints.isOpen = !CManager.hk_waypoints.isOpen;
            }
            if (GUILayout.Button("Misc"))
            {
                CManager.hk_misc.isOpen = !CManager.hk_misc.isOpen;
            }
            if (GUILayout.Button("GUI Colors"))
            {
                CManager.hk_guicolors.isOpen = !CManager.hk_guicolors.isOpen;
            }
            GUILayout.Label("Created by Ariedam");

            if(GUILayout.Button("Join discord"))
            {
                Application.OpenURL("https://discord.gg/ccysRb2dgt");
            }
            base.runWin(id); 
        }


    }
}