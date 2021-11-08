using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Reflection;
using System.Linq;
using System.Runtime.InteropServices;
using test.CT_System;

namespace test.CT_Hacks
{
    public class H_MobSpawner : Menu
    {

        public static Vector2 ScrollPosition2 { get; set; } = Vector2.zero;
        public int ItemSpawnerAmount = 1;
        public int powerMultiplierAmount = 1;
        public int multiBossAmount = 1;
        public int speedAmount = 1;
        public bool isList = false;
        public List<mobList> mobListTab = new List<mobList>();

        public string[] toolbarStringsMenu = { "Mobs", "Stats", "Spawn position", "List" };
        public string[] toolbarStringsSelectionMode = { "Selection", "Spawn"};
        public int tolbarIntMenu = 0;
        public int tolbarIntSelectionMode = 0;
        public string prevTooltip = "";
        public string mobStats;
        public string prevMobSpeed, mobSpeed;
        public string prevMobMaxAttackDistance ,maxAttackDistance;
        public string prevMobBoss, mobBoss;
        public string prevMobDefense, mobDefense;

        public H_MobSpawner() : base(new Rect(1000, 420, 430, 320), "Mob Spawner menu", 7, false) { }

        public void Update()
        {

        }
        public override void runWin(int id)
        {
            tolbarIntMenu = GUI.Toolbar(new Rect(10, 20, 410, 23), tolbarIntMenu, toolbarStringsMenu);
            GUI.Box(new Rect(150, 50, 250, 230), "Mobs list");
            ScrollPosition2 = GUI.BeginScrollView(new Rect(160, 80, 260, 200), ScrollPosition2, new Rect(140, 50, 240, 290), false, true);

            int x = 140;
            int y = 50;
            int buttonWidth = 32;

            foreach (MobType mob in MobSpawner.Instance.allMobs)
            {
                if (GUI.Button(new Rect(x, y, 110, 22), new GUIContent(mob.name, mob.maxAttackDistance + "$1" + mob.speed)))
                {
                    if (isList)
                    {
                        new mobList() { id = mob.id, multiplier = powerMultiplierAmount, quantity = ItemSpawnerAmount };
                    }
                    else
                    {
                        for (int j = 0; j < ItemSpawnerAmount; j++)
                        {
                            MobSpawner.Instance.ServerSpawnNewMob(MobManager.Instance.GetNextId(), mob.id, PlayerMovement.Instance.GetRb().position, powerMultiplierAmount, multiBossAmount, Mob.BossType.None, -1);
                        }
                    }
                }
                if (x == 260)
                {
                    x = 140; y += 32;
                    GUILayout.Space(buttonWidth);
                }
                else
                    x += 120;
            }

            GUILayout.Space(buttonWidth);
            GUILayout.EndScrollView();
            switch (tolbarIntMenu)
            {
                case 0:

                    GUI.Box(new Rect(10, 50, 115, 60), "Mode");
                    GUI.Label(new Rect(15, 100, 95, 20), "Quantity : x" + ItemSpawnerAmount);
                    ItemSpawnerAmount = (int)GUI.HorizontalSlider(new Rect(15, 65, 130, 20), ItemSpawnerAmount, 1, 100);
                    GUI.Label(new Rect(15, 125, 140, 20), "Power + health : x" + powerMultiplierAmount);
                    powerMultiplierAmount = (int)GUI.HorizontalSlider(new Rect(15, 95, 130, 20), powerMultiplierAmount, 1, 50);

                    break;

                case 1:

                    break;

                case 2:

                    break;

                case 3:

                    break;
            }
            if (GUI.Button(new Rect(10, 290, 400, 20), "Exit"))
            {
                this.isOpen = false;
            }

            base.runWin(id);
        }
    }

    public class mobList
    {
        public int id { get; set; }
        public Vector3 position { get; set; }
        public float multiplier { get; set; }
        public float quantity { get; set; }
    }
}