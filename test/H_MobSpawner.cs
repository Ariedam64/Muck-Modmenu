using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Reflection;
using System.Linq;
using System.Runtime.InteropServices;

namespace test.CT_Hacks
{
    public class H_MobSpawner : Menu
    {
        public static Vector2 ScrollPosition2 { get; set; } = Vector2.zero;
        public static readonly Rect ItemSpawnerSliderPosition = new Rect(10, 40, 140, 20);
        public static readonly Rect ItemSpawnerlabel = new Rect(10, 20, 100, 20);
        public static readonly Rect powerMultiplieSliderPosition = new Rect(10, 70, 140, 20);
        public static readonly Rect powerMultiplierlabel = new Rect(10, 50, 140, 20);
        public static readonly Rect multiBoss = new Rect(10, 100, 140, 20);
        public static readonly Rect multiBosslabel = new Rect(10, 80, 100, 20);
        public int ItemSpawnerAmount = 1;
        public int powerMultiplierAmount = 1;
        public int multiBossAmount = 1;
        public int mobStat;

        public H_MobSpawner() : base(new Rect(1460, 420, 430, 200), "Mob Spawner menu", 7, false) { }

        public void Update()
        {
			
		}
        public override void runWin(int id)
        {
            ScrollPosition2 = GUILayout.BeginScrollView(ScrollPosition2, false, true, GUILayout.Width(430), GUILayout.Height(200));

            int x = 160;
            int y = 0;
            int buttonWidth = 32;

            for (int i = 0; i < MobSpawner.Instance.allMobs.Length; i++)
            {
                MobType mob = MobSpawner.Instance.allMobs[i];
                if (GUI.Button(new Rect(x, y, 120, 22), mob.name))
                {
                    for (int j = 0; j < ItemSpawnerAmount; j++){
                        MobSpawner.Instance.ServerSpawnNewMob(MobManager.Instance.GetNextId(), mob.id, PlayerMovement.Instance.GetRb().position, powerMultiplierAmount, multiBossAmount, Mob.BossType.None, -1);
                    }
                }

                if (x == 290)
                {
                    x = 160; y += 32;
                    GUILayout.Space(buttonWidth);
                }
                else
                    x += 130;

            }
            GUILayout.Space(buttonWidth);

            GUILayout.EndScrollView();

            GUI.Label(ItemSpawnerlabel, "Quantity : x" + ItemSpawnerAmount);
            ItemSpawnerAmount = (int)GUI.HorizontalSlider(ItemSpawnerSliderPosition, ItemSpawnerAmount, 1, 100);
            GUI.Label(powerMultiplierlabel, "Power multiplier : x" + powerMultiplierAmount);
            powerMultiplierAmount = (int)GUI.HorizontalSlider(powerMultiplieSliderPosition, powerMultiplierAmount, 1, 50);
            GUI.Label(multiBosslabel, "Boss multiplier : x" + multiBossAmount);
            multiBossAmount = (int)GUI.HorizontalSlider(multiBoss, multiBossAmount, 1, 50);

            GUI.Box(new Rect(10, 120, 140, 100), "Stats");


            base.runWin(id);
        }

    }
}