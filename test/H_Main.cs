using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace test.CT_Hacks
{
    public class H_Main : Menu
    {
        public H_Main() : base(new Rect(10, 10, 200, 200), "MAH 0.1", 0, true) { }

        public override void runWin(int id)
        {

            if (GUILayout.Button("Player"))
            {
                CManager.hk_Player.isOpen = !CManager.hk_Player.isOpen;
            }
            if (GUILayout.Button("Server"))
            {
                CManager.hk_server.isOpen = !CManager.hk_server.isOpen;
            }
            if (GUILayout.Button("ESP (soon)"))
            {
                CManager.hk_esp.isOpen = !CManager.hk_esp.isOpen;
            }
            if (GUILayout.Button("Mob Spawner"))
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


            GUILayout.Label("Created by Ariedam");
            base.runWin(id); 
        }


    }
}