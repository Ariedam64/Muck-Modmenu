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

        public static Vector2 mobListScrollPositition { get; set; } = Vector2.zero;
        public static Vector2 mobListinListScrollPosition { get; set; } = Vector2.zero;
        public int ItemSpawnerAmount = 1;
        public int powerMultiplierAmount = 1;
        public int multiBossAmount = 1;
        public int speedAmount = 1;
        public int yScroll = 0;
        public bool isList = false;
        public List<mobList> mobListTab = new List<mobList>();

        public string[] toolbarStringsAdvMenuCurrent;
        public string[] toolbarStringsSelectionMode = {"Normal", "Adv."};
        public string[] toolbarStringsSpawnMode = { "Normal", "List" };
        public GUIStyle centeredStyle;
        public int tolbarIntMenu = 0;
        public int tolbarIntAdvancedMode = 0;
        public int tolbarIntPosition = 0;
        public int tolbarIntList = 0;
        public string prevTooltip = "";
        public string mobStats;
        public string prevMobSpeed, mobSpeed;
        public string prevMobMaxAttackDistance ,maxAttackDistance;
        public string prevMobBoss, mobBoss;
        public string prevMobDefense, mobDefense;

        public H_MobSpawner() : base(new Rect(1000, 420, 430, 320), "Mob Spawner menu", 7, false) { }

        public void Update()
        {
            centeredStyle.alignment = TextAnchor.UpperCenter;
            centeredStyle.normal.textColor = Color.white;
            centeredStyle.normal.background = Texture2D.grayTexture;

            if (tolbarIntAdvancedMode == 1 && tolbarIntPosition == 0 && tolbarIntList == 0)
            {
                toolbarStringsAdvMenuCurrent = new string[2];
                Array.Clear(toolbarStringsAdvMenuCurrent, 0, toolbarStringsAdvMenuCurrent.Length);
                toolbarStringsAdvMenuCurrent[0] = "Mobs";
                toolbarStringsAdvMenuCurrent[1] = "Stats";
            }
            if(tolbarIntAdvancedMode == 1 && tolbarIntPosition == 1 && tolbarIntList == 0)
            {
                toolbarStringsAdvMenuCurrent = new string[3];
                Array.Clear(toolbarStringsAdvMenuCurrent, 0, toolbarStringsAdvMenuCurrent.Length);
                toolbarStringsAdvMenuCurrent[0] = "Mobs";
                toolbarStringsAdvMenuCurrent[1] = "Stats";
                toolbarStringsAdvMenuCurrent[2] = "Postion";
            }
            if (tolbarIntAdvancedMode == 1 && tolbarIntPosition == 1 && tolbarIntList == 1)
            {
                toolbarStringsAdvMenuCurrent = new string[4];
                Array.Clear(toolbarStringsAdvMenuCurrent, 0, toolbarStringsAdvMenuCurrent.Length);
                toolbarStringsAdvMenuCurrent[0] = "Mobs";
                toolbarStringsAdvMenuCurrent[1] = "Stats";
                toolbarStringsAdvMenuCurrent[2] = "Postion";
                toolbarStringsAdvMenuCurrent[3] = "List";
            }
            if (tolbarIntAdvancedMode == 1 && tolbarIntPosition == 0 && tolbarIntList == 1)
            {
                toolbarStringsAdvMenuCurrent = new string[3];
                Array.Clear(toolbarStringsAdvMenuCurrent, 0, toolbarStringsAdvMenuCurrent.Length);
                toolbarStringsAdvMenuCurrent[0] = "Mobs";
                toolbarStringsAdvMenuCurrent[1] = "Stats";
                toolbarStringsAdvMenuCurrent[2] = "List";
            }

        }
        public override void runWin(int id)
        {
            switch (tolbarIntAdvancedMode)
            {
                //Normal mode
                case 0:
                    GUI.Box(new Rect(10, 25, 135, 55), "Mode");
                    tolbarIntAdvancedMode = GUI.Toolbar(new Rect(15, 50, 125, 23), tolbarIntAdvancedMode, toolbarStringsSelectionMode);
                    GUI.Box(new Rect(150, 25, 250, 255), "Mobs list");
                    mobListScrollPositition = GUI.BeginScrollView(new Rect(160, 50, 260, 230), mobListScrollPositition, new Rect(140, 25, 240, 290), false, true);
                    int x = 140;
                    int y = 25;
                    foreach (MobType mob in MobSpawner.Instance.allMobs)
                    {
                        if (GUI.Button(new Rect(x, y, 110, 22), new GUIContent(mob.name, mob.maxAttackDistance + "$1" + mob.speed)))
                        {
                            for (int j = 0; j < ItemSpawnerAmount; j++)
                            {
                                 MobSpawner.Instance.ServerSpawnNewMob(MobManager.Instance.GetNextId(), mob.id, PlayerMovement.Instance.GetRb().position, powerMultiplierAmount, multiBossAmount, Mob.BossType.None, -1);
                            } 
                        }
                        if (x == 260)
                        {
                            x = 140; y += 32;
                        }
                        else
                            x += 120;
                    }
                    GUI.EndScrollView();
                    GUI.Label(new Rect(10, 85, 95, 20), "Quantity : x" + ItemSpawnerAmount);
                    ItemSpawnerAmount = (int)GUI.HorizontalSlider(new Rect(10, 105, 130, 20), ItemSpawnerAmount, 1, 100);
                    GUI.Label(new Rect(10, 115, 140, 20), "Power + health : x" + powerMultiplierAmount);
                    powerMultiplierAmount = (int)GUI.HorizontalSlider(new Rect(10, 135, 130, 20), powerMultiplierAmount, 1, 50);
                    break;

                //Advanced mode
               case 1:
                    switch (tolbarIntMenu)
                    {
                        case 0:
                           tolbarIntMenu = GUI.Toolbar(new Rect(10, 22, 410, 23), tolbarIntMenu, toolbarStringsAdvMenuCurrent);
                            GUI.Box(new Rect(10, 50, 135, 55), "Mode");
                            tolbarIntAdvancedMode = GUI.Toolbar(new Rect(15, 75, 125, 23), tolbarIntAdvancedMode, toolbarStringsSelectionMode);
                            GUI.Box(new Rect(10, 110, 135, 55), "Spawn position");
                            tolbarIntPosition = GUI.Toolbar(new Rect(15, 135, 125, 23), tolbarIntPosition, toolbarStringsSelectionMode);
                            GUI.Box(new Rect(10, 170, 135, 55), "Spawn");
                            tolbarIntList = GUI.Toolbar(new Rect(15, 195, 125, 23), tolbarIntList, toolbarStringsSpawnMode);
                            GUI.Label(new Rect(10, 225, 95, 20), "Quantity : x" + ItemSpawnerAmount);
                            ItemSpawnerAmount = (int)GUI.HorizontalSlider(new Rect(10, 245, 130, 20), ItemSpawnerAmount, 1, 100);
                            GUI.Label(new Rect(10, 255, 140, 20), "Power + health : x" + powerMultiplierAmount);
                            powerMultiplierAmount = (int)GUI.HorizontalSlider(new Rect(10, 275, 130, 20), powerMultiplierAmount, 1, 50);
                            GUI.Box(new Rect(150, 50, 250, 230), "Mobs list");
                            mobListScrollPositition = GUI.BeginScrollView(new Rect(160, 80, 260, 200), mobListScrollPositition, new Rect(140, 50, 240, 290), false, true);
                            int x2 = 140;
                            int y2 = 50;
                            foreach (MobType mob in MobSpawner.Instance.allMobs)
                            {
                                if (GUI.Button(new Rect(x2, y2, 110, 22), new GUIContent(mob.name, mob.maxAttackDistance + "$1" + mob.speed)))
                                {
                                    if (tolbarIntList == 1)
                                    {
                                        mobListTab.Add(new mobList() { name = mob.name, id = mob.id, multiplier = powerMultiplierAmount, quantity = ItemSpawnerAmount });
                                        yScroll += 20;
                                    }
                                    else
                                    {
                                        for (int j = 0; j < ItemSpawnerAmount; j++)
                                        {
                                            MobSpawner.Instance.ServerSpawnNewMob(MobManager.Instance.GetNextId(), mob.id, PlayerMovement.Instance.GetRb().position, powerMultiplierAmount, multiBossAmount, Mob.BossType.None, -1);
                                        }
                                    }                             
                                }
                                if (x2 == 260)
                                {
                                    x2 = 140; y2 += 32;
                                }
                                else
                                    x2 += 120;
                            }
                            GUI.EndScrollView();

                            break;

                        case 1:
                            tolbarIntMenu = GUI.Toolbar(new Rect(10, 22, 410, 23), tolbarIntMenu, toolbarStringsAdvMenuCurrent);
                            GUI.Box(new Rect(150, 50, 250, 230), "Mobs list");
                            mobListScrollPositition = GUI.BeginScrollView(new Rect(160, 80, 260, 200), mobListScrollPositition, new Rect(140, 50, 240, 290), false, true);
                            int x3 = 140;
                            int y3 = 50;
                            foreach (MobType mob in MobSpawner.Instance.allMobs)
                            {
                                if (GUI.Button(new Rect(x3, y3, 110, 22), new GUIContent(mob.name, mob.maxAttackDistance + "$1" + mob.speed)))
                                {
                                    if (tolbarIntList == 1)
                                    {
                                        mobListTab.Add(new mobList() { name = mob.name, id = mob.id, multiplier = powerMultiplierAmount, quantity = ItemSpawnerAmount });
                                        yScroll += 20;
                                    }
                                    else
                                    {
                                        for (int j = 0; j < ItemSpawnerAmount; j++)
                                        {
                                            MobSpawner.Instance.ServerSpawnNewMob(MobManager.Instance.GetNextId(), mob.id, PlayerMovement.Instance.GetRb().position, powerMultiplierAmount, multiBossAmount, Mob.BossType.None, -1);
                                        }
                                    }
                                }
                                if (x3 == 260)
                                {
                                    x3 = 140; y3 += 32;
                                }
                                else
                                    x3 += 120;
                            }
                            GUI.EndScrollView();
                            break;

                        case 2:
                            if(tolbarIntPosition == 1)
                            {
                                tolbarIntMenu = GUI.Toolbar(new Rect(10, 22, 410, 23), tolbarIntMenu, toolbarStringsAdvMenuCurrent);
                                break;
                            }
                            else
                            {
                                tolbarIntMenu = GUI.Toolbar(new Rect(10, 22, 410, 23), tolbarIntMenu, toolbarStringsAdvMenuCurrent);
                                
                                showMobsList();
                                break;
                            }
                            

                        case 3:
                            tolbarIntMenu = GUI.Toolbar(new Rect(10, 22, 410, 23), tolbarIntMenu, toolbarStringsAdvMenuCurrent);
                            GUI.Box(new Rect(130, 50, 290, 200), "Mobs added");
                            showMobsList();
                            break;
                    }
                    break;
            }

            if (GUI.Button(new Rect(10, 290, 410, 20), "Exit"))
            {
                this.isOpen = false;
            }

            base.runWin(id);
        }

        void showMobsList()
        {
            GUI.Box(new Rect(130, 50, 270, 200), "Mobs added");
            mobListinListScrollPosition = GUI.BeginScrollView(new Rect(135, 80, 285, 160), mobListinListScrollPosition, new Rect(135, 80, 200, yScroll), false, true);

            int y = 80;
            foreach (mobList mob in mobListTab)
            {
                GUI.Label(new Rect(140, y, 250, 17), mob.name + " | Quantity x" + mob.quantity, centeredStyle);
                y += 20;
            }
            GUI.EndScrollView();

        }
    }

    public class mobList
    {
        public string name { get; set; }
        public int id { get; set; }
        public Vector3 position { get; set; }
        public float multiplier { get; set; }
        public float quantity { get; set; }
    }
}