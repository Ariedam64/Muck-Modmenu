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
        public static Vector2 playerListScrollPosition { get; set; } = Vector2.zero;
        public static Vector2 waypointListScrollView { get; set; } = Vector2.zero;
        public int ItemSpawnerAmount = 1;
        public int powerMultiplierAmount = 1;
        public int multiBossAmount = 1;
        public int speedAmount = 1;
        public int yScroll = 20;
        public bool isList = false;
        public List<mobList> mobListTab = new List<mobList>();
        public string[] playerList;

        public string[] toolbarStringsAdvMenuCurrent;
        public string[] toolbarStringsSelectionMode = {"Normal", "Adv."};
        public string[] toolbarStringsOthersMode = { "Look at"};
        public string[] toolbarStringsSpawnMode = { "Normal", "List" };
        public string[] toolbarStringsListMode = { "Show", "Delete" };
        public string[] toolbarStringsPositionMode = { "Players", "Waypoints", "Others" };
        public string[] waypointsListString;
        public string[] playersListString;
        public GUIStyle centeredStyle, centeredStyle2;
        public Vector3 spawnPosition;
        public int tolbarIntMenu = 0;
        public int yWaypoints;
        public int yPlayer;
        public int tolbarPlayerSelected = 0;
        public int tolbarOthersSelected = 0;
        public int tolbarWaypointSelected = 0;
        public int tolbarListMode = 0;
        public int tolbarIntAdvancedMode = 0;
        public int tolbarIntPosition = 0;
        public int tolbarIntList = 0;
        public int tolbarIntPositionMode = 0;
        public string prevTooltip = "";
        public string mobStats;
        public string prevMobSpeed, mobSpeed;
        public string prevMobMaxAttackDistance ,maxAttackDistance;
        public string prevMobBoss, mobBoss;
        public string prevMobDefense, mobDefense;

        public H_MobSpawner() : base(new Rect(1000, 420, 430, 320), "Mob Spawner menu", 7, false) { }

        public void Update()
        {





            //Position
            waypointsListString = H_Waypoints.waypointsList.Select(x => x.name).ToArray();
            playersListString = LB_Menu.listeJoueur.Select(x => x.username).ToArray();

            yPlayer = (15 * playersListString.Length) + 10 * playersListString.Length - 1;
            yWaypoints = (15 * waypointsListString.Length) + 10 * waypointsListString.Length - 1;

            if (tolbarIntPosition == 0)
            {
                spawnPosition = PlayerMovement.Instance.transform.position;
            }
            else
            {
                switch (tolbarIntPositionMode)
                {
                    //Players
                    case 0:
                        spawnPosition = LB_Menu.listeJoueur[tolbarPlayerSelected].transform.position;
                        break;
                    //Waypoints
                    case 1:
                        spawnPosition = H_Waypoints.waypointsList[tolbarWaypointSelected].position;
                        break;
                    //Other
                    case 2:
                        spawnPosition = Variables.FindTpPos();
                        break;
                }
            }

            //-------

            //List
            centeredStyle.alignment = TextAnchor.UpperCenter;
            centeredStyle.normal.textColor = Color.white;
            centeredStyle.normal.background = Texture2D.grayTexture;

            centeredStyle2.alignment = TextAnchor.UpperCenter;
            centeredStyle2.normal.textColor = Color.white;
            centeredStyle2.fontStyle = FontStyle.Bold;
            centeredStyle2.normal.background = Texture2D.grayTexture;

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
            ///-------
        }
        public override void runWin(int id)
        {
            switch (tolbarIntAdvancedMode)
            {
                //Normal mode
                case 0:
                    GUI.Box(new Rect(10, 25, 135, 55), "Mode");
                    tolbarIntAdvancedMode = GUI.Toolbar(new Rect(15, 50, 125, 23), tolbarIntAdvancedMode, toolbarStringsSelectionMode);
                    
                    GUI.Label(new Rect(10, 85, 95, 20), "Quantity : x" + ItemSpawnerAmount);
                    ItemSpawnerAmount = (int)GUI.HorizontalSlider(new Rect(10, 105, 130, 20), ItemSpawnerAmount, 1, 100);
                    GUI.Label(new Rect(10, 115, 140, 20), "Power + health : x" + powerMultiplierAmount);
                    powerMultiplierAmount = (int)GUI.HorizontalSlider(new Rect(10, 135, 130, 20), powerMultiplierAmount, 1, 50);
                    showMobNormal();
                    break;

                //Advanced mode
               case 1:
                    switch (tolbarIntMenu)
                    {
                        //Mobs menu
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
                            showMobsAdvanced();
                            break;

                        //Stats menu
                        case 1:
                            tolbarIntMenu = GUI.Toolbar(new Rect(10, 22, 410, 23), tolbarIntMenu, toolbarStringsAdvMenuCurrent);
                            showMobsAdvanced();
                            break;

                         
                        case 2:
                            //Position menu
                            if (tolbarIntPosition == 1)
                            {
                                tolbarIntMenu = GUI.Toolbar(new Rect(10, 22, 410, 23), tolbarIntMenu, toolbarStringsAdvMenuCurrent);
                                GUI.Box(new Rect(10, 50, 134, 230), "");
                                GUI.Box(new Rect(148, 50, 134, 230), "");
                                GUI.Box(new Rect(286, 50, 134, 230), "");
                                tolbarIntPositionMode = GUI.Toolbar(new Rect(10, 50, 410, 23), tolbarIntPositionMode, toolbarStringsPositionMode);
                                playerListScrollPosition = GUI.BeginScrollView(new Rect(10, 80, 129, 190), playerListScrollPosition, new Rect(10, 80, 50, yPlayer), false, true);
                                    tolbarPlayerSelected = GUI.SelectionGrid(new Rect(15, 80, 105, yPlayer), tolbarPlayerSelected, playersListString, 1);
                                GUI.EndScrollView();
                                waypointListScrollView = GUI.BeginScrollView(new Rect(148, 80, 129, 190), waypointListScrollView, new Rect(148, 80, 50, yWaypoints), false, true);
                                    tolbarWaypointSelected = GUI.SelectionGrid(new Rect(153, 80, 105, yWaypoints), tolbarWaypointSelected, waypointsListString, 1);
                                GUI.EndScrollView();
                                    tolbarOthersSelected = GUI.SelectionGrid(new Rect(291, 80, 125, 25), tolbarOthersSelected, toolbarStringsOthersMode, 1);
                                break;
                            }
                            // List menu
                            else
                            {
                                showLIST();
                                break;
                            }
                            
                        //List menu
                        case 3:
                            showLIST();
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

        void showLIST()
        {
            showListMenu();
            switch (tolbarListMode)
            {
                //Show
                case 0:
                    showMobsListLabel();
                    break;

                //Delete
                case 1:
                    showMobsListButton();
                    break;
            }
        }
        void showListMenu()
        {
            tolbarIntMenu = GUI.Toolbar(new Rect(10, 22, 410, 23), tolbarIntMenu, toolbarStringsAdvMenuCurrent);
            GUI.Box(new Rect(10, 50, 135, 55), "Mode");
            tolbarListMode = GUI.Toolbar(new Rect(15, 75, 125, 23), tolbarListMode, toolbarStringsListMode);
            GUI.Box(new Rect(10, 110, 135, 90), "Actions");
            if (GUI.Button(new Rect(15, 135, 125, 25), "Spawn all"))
            {
                spawnAllMobsInList();
                yScroll = 20;
            }
            if (GUI.Button(new Rect(15, 165, 125, 25), "Clear list"))
            {
                mobListTab.Clear();
                yScroll = 20;
            }
        }

        void showMobsListLabel()
        {
            GUI.Box(new Rect(150, 50, 250, 230), "Mobs added");
            mobListinListScrollPosition = GUI.BeginScrollView(new Rect(155, 80, 265, 190), mobListinListScrollPosition, new Rect(155, 80, 180, yScroll), false, true);
            int y = 80;
            GUI.Label(new Rect(160, y, 230, 17), "  Mob name  | Multiplier | Quantity   ", centeredStyle2);
            y += 20;
            foreach (mobList mob in mobListTab)
            {
                GUI.Label(new Rect(160, y, 230, 17), mob.name + " | x" + mob.multiplier + " | x" + mob.quantity, centeredStyle);
                y += 20;
            }
            GUI.EndScrollView();
        }


        void showMobsListButton()
        {
            GUI.Box(new Rect(150, 50, 250, 230), "Mobs added");
            mobListinListScrollPosition = GUI.BeginScrollView(new Rect(155, 80, 265, 190), mobListinListScrollPosition, new Rect(155, 80, 180, yScroll), false, true);
            int y = 80;
            GUI.Label(new Rect(160, y, 230, 17), "  Mob name  | Multiplier | Quantity   ", centeredStyle2);
            y += 20;
            foreach (mobList mob in mobListTab)
            {
                if(GUI.Button(new Rect(160, y, 230, 20), mob.name + " | x" + mob.multiplier + " |  x" + mob.quantity))
                {
                    mobListTab.Remove(mob);
                    yScroll -= 20;
                }
                y += 20;
            }
            GUI.EndScrollView();
        }

        void showMobsAdvanced()
        {
            GUI.Box(new Rect(150, 50, 250, 230), "Mobs list");
            mobListScrollPositition = GUI.BeginScrollView(new Rect(160, 80, 260, 200), mobListScrollPositition, new Rect(140, 50, 240, 290), false, true);
            int x2 = 140;
            int y2 = 50;
            foreach (MobType mob in MobSpawner.Instance.allMobs)
            {
                if (GUI.Button(new Rect(x2, y2, 110, 22), new GUIContent(mob.name, mob.maxAttackDistance + "$1" + mob.speed)))
                {
                    //If list
                    if (tolbarIntList == 1)
                    {
                        if(mobListTab.Exists(x => x.id == mob.id) && mobListTab.Exists(x => x.multiplier == powerMultiplierAmount))
                        {
                            var sameMob = mobListTab.Find(x => (x.id == mob.id) && (x.multiplier == powerMultiplierAmount)); 
                            sameMob.quantity += ItemSpawnerAmount;
                        }
                        else
                        {
                            mobListTab.Add(new mobList() { name = mob.name, id = mob.id, multiplier = powerMultiplierAmount, quantity = ItemSpawnerAmount });
                            yScroll += 20;
                        }                  
                    }
                    //If normal
                    else
                    {
                        for (int j = 0; j < ItemSpawnerAmount; j++)
                        {
                            MobSpawner.Instance.ServerSpawnNewMob(MobManager.Instance.GetNextId(), mob.id, spawnPosition, powerMultiplierAmount, multiBossAmount, Mob.BossType.None, -1);
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
        }

        void showMobNormal()
        {
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
                        MobSpawner.Instance.ServerSpawnNewMob(MobManager.Instance.GetNextId(), mob.id, spawnPosition, powerMultiplierAmount, multiBossAmount, Mob.BossType.None, -1);
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
        }

        void spawnAllMobsInList()
        {
            foreach (mobList mob in mobListTab)
            {
                for (int j = 0; j < mob.quantity; j++)
                {
                    MobSpawner.Instance.ServerSpawnNewMob(MobManager.Instance.GetNextId(), mob.id, spawnPosition, mob.multiplier, 1, Mob.BossType.None, -1);
                }
            }
            mobListTab.Clear();
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