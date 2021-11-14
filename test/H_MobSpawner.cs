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
        public static Vector2 mobStatsListScrollPosition { get; set; } = Vector2.zero;
        
        public List<mobList> mobListTab = new List<mobList>();

        public readonly Color GUIOriginalColor = GUI.color;
        public GUIStyle centeredStyle, centeredStyle2, styleForStats;
        public Vector3 spawnPosition;

        public int ItemSpawnerAmount = 1;
        public int powerMultiplierAmount = 1;
        public int multiBossAmount = 1;
        public int speedAmount = 1;
        public int yScroll = 20;
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

        public string[] playerList;
        public string[] toolbarStringsAdvMenuCurrent;
        public string[] toolbarStringsSelectionMode = {"Normal", "Adv."};
        public string[] toolbarStringsOthersMode = { "Look at"};
        public string[] toolbarStringsSpawnMode = { "Normal", "List" };
        public string[] toolbarStringsListMode = { "Show", "Delete" };
        public string[] toolbarStringsPositionMode = { "Players", "Waypoints", "Others" };
        public string[] waypointsListString;
        public string[] playersListString;

        //Stats
        public string prevRangedString, rangedString;
        public string prevMobMaxAttackDistanceString, maxAttackDistanceString;
        public string prevRangedCooldownString, rangedCooldownString;
        public string prevStartAttackDistanceString, startAttackDistanceString;
        public string prevStartRangedAttackDistanceString, startRangedAttackDistanceString;
        public string prevSpeedString, speedString;
        public string prevMinAttackAngleString, minAttackAngleString;
        public string prevSharpDefenseString, sharpDefenseString;
        public string prevDefenseString, defenseString;
        public string prevKnockbackThresholdString, knockbackThresholdString;
        public string prevIgnoreBuildsString, ignoreBuildsString;
        public string prevFollowPlayerDistanceString, followPlayerDistanceString;
        public string prevFollowPlayerAccuracyString, followPlayerAccuracyString;
        public string prevOnlyRangedInRangedPatternString, onlyRangedInRangedPatternString;
        public string prevBossString, bossString;

        public bool prevRanged, ranged;
        public float prevRangedCooldown, rangedCooldown;
        public float prevStartAttackDistance, startAttackDistance;
        public float prevStartRangedAttackDistance, startRangedAttackDistance;
        public float prevMaxAttackDistance, maxAttackDistance;
        public float prevSpeed, speed;
        public float prevMinAttackAngle, minAttackAngle;
        public float prevSharpDefense, sharpDefense;
        public float prevDefense, defense;
        public float prevKnockbackThreshold, knockbackThreshold;
        public bool prevIgnoreBuilds, ignoreBuilds;
        public float prevFollowPlayerDistance, followPlayerDistance;
        public float prevFollowPlayerAccuracy, followPlayerAccuracy;
        public bool prevOnlyRangedInRangedPattern, onlyRangedInRangedPattern;
        public bool prevBoss, boss;



        //ToolTip
        public string prevTooltip = "";
        public string tooltipString;

        public MobType mobSelected = null;


        public H_MobSpawner() : base(new Rect(430, 420, 430, 320), "Mob Spawner menu", 7, false) { }

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
            styleForStats.normal.textColor = Color.white;
            styleForStats.fontSize = 10;

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
            GUI.backgroundColor = H_GUIColors.GUIBackgroundColor;
            GUI.contentColor = H_GUIColors.GUIFrontColor;
            GUILayout.Label("");
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
                            GUI.Box(new Rect(10, 50, 135, 190), "Stats");
                            showMobsAdvanced();
                            loadMobStats();
                            if(tolbarIntList == 0)
                            {
                                if (mobSelected != null)
                                {
                                    mobStatsEdit();
                                    if (GUI.Button(new Rect(10,245,135, 35), "Spawn \n" + mobSelected.name))
                                    {
                                        MobSpawner.Instance.ServerSpawnNewMob(MobManager.Instance.GetNextId(), mobSelected.id, spawnPosition, powerMultiplierAmount, multiBossAmount, Mob.BossType.None, -1);                                       
                                        resetMobStats();
                                    }      
                                }
                                else
                                {
                                    GUI.backgroundColor = Color.black;
                                    GUI.contentColor = Color.black;
                                    GUI.Button(new Rect(10, 245, 135, 35), "Spawn \n" + "None");
                                    GUI.backgroundColor = H_GUIColors.GUIBackgroundColor;
                                    GUI.contentColor = H_GUIColors.GUIFrontColor;
                                }
                            }
                            else
                            {
                                if (mobSelected != null)
                                {
                                    mobStatsEdit();
                                    if (GUI.Button(new Rect(10, 245, 135, 35), "Add to list \n" + mobSelected.name))
                                    {
                                        if (mobListTab.Exists(x => x.id == mobSelected.id) &&
                                            mobListTab.Exists(x => x.multiplier == powerMultiplierAmount) &&
                                            mobListTab.Exists(x => x.ranged == mobSelected.ranged) &&
                                            mobListTab.Exists(x => x.rangedCooldown == mobSelected.rangedCooldown) &&
                                            mobListTab.Exists(x => x.startAttackDistance == mobSelected.startAttackDistance) &&
                                            mobListTab.Exists(x => x.startRangedAttackDistance == mobSelected.startRangedAttackDistance) &&
                                            mobListTab.Exists(x => x.maxAttackDistance == mobSelected.maxAttackDistance) &&
                                            mobListTab.Exists(x => x.speed == mobSelected.speed) &&
                                            mobListTab.Exists(x => x.minAttackAngle == mobSelected.minAttackAngle) &&
                                            mobListTab.Exists(x => x.sharpDefense == mobSelected.sharpDefense) &&
                                            mobListTab.Exists(x => x.defense == mobSelected.defense) &&
                                            mobListTab.Exists(x => x.knockbackThreshold == mobSelected.knockbackThreshold) &&
                                            mobListTab.Exists(x => x.ignoreBuilds == mobSelected.ignoreBuilds) &&
                                            mobListTab.Exists(x => x.followPlayerDistance == mobSelected.followPlayerDistance) &&
                                            mobListTab.Exists(x => x.followPlayerAccuracy == mobSelected.followPlayerAccuracy) &&
                                            mobListTab.Exists(x => x.onlyRangedInRangedPattern == mobSelected.onlyRangedInRangedPattern) &&
                                            mobListTab.Exists(x => x.boss == mobSelected.boss) &&
                                            mobListTab.Exists(x => x.statsEdited == true))
                                            
                                        {
                                            var sameMob = mobListTab.Find(x => (x.id == mobSelected.id) &&
                                                                               (x.multiplier == powerMultiplierAmount) &&
                                                                               (x.ranged == mobSelected.ranged) &&
                                                                               (x.rangedCooldown == mobSelected.rangedCooldown) &&
                                                                               (x.startAttackDistance == mobSelected.startAttackDistance) &&
                                                                               (x.startRangedAttackDistance == mobSelected.startRangedAttackDistance) &&
                                                                               (x.maxAttackDistance == mobSelected.maxAttackDistance) &&
                                                                               (x.speed == mobSelected.speed) &&
                                                                               (x.minAttackAngle == mobSelected.minAttackAngle) &&
                                                                               (x.sharpDefense == mobSelected.sharpDefense) &&
                                                                               (x.defense == mobSelected.defense) &&
                                                                               (x.knockbackThreshold == mobSelected.knockbackThreshold) &&
                                                                               (x.ignoreBuilds == mobSelected.ignoreBuilds) &&
                                                                               (x.followPlayerDistance == mobSelected.followPlayerDistance) &&
                                                                               (x.followPlayerAccuracy == mobSelected.followPlayerAccuracy) &&
                                                                               (x.onlyRangedInRangedPattern == mobSelected.onlyRangedInRangedPattern) &&
                                                                               (x.boss == mobSelected.boss) &&
                                                                               (x.statsEdited == true));

                                            sameMob.quantity += ItemSpawnerAmount;
                                        }
                                        else
                                        {                                              
                                            mobListTab.Add(new mobList()
                                            {
                                                name = mobSelected.name,
                                                id = mobSelected.id,
                                                multiplier = powerMultiplierAmount,
                                                quantity = ItemSpawnerAmount,
                                                statsEdited = true,
                                                ranged = mobSelected.ranged,
                                                rangedCooldown = mobSelected.rangedCooldown,
                                                startAttackDistance = mobSelected.startAttackDistance,
                                                startRangedAttackDistance = mobSelected.startRangedAttackDistance,
                                                maxAttackDistance = mobSelected.maxAttackDistance,
                                                speed = mobSelected.speed,
                                                minAttackAngle = mobSelected.minAttackAngle,
                                                sharpDefense = mobSelected.sharpDefense,
                                                defense = mobSelected.defense,
                                                knockbackThreshold = mobSelected.knockbackThreshold,
                                                ignoreBuilds = mobSelected.ignoreBuilds,
                                                followPlayerDistance = mobSelected.followPlayerDistance,
                                                followPlayerAccuracy = mobSelected.followPlayerAccuracy,
                                                onlyRangedInRangedPattern = mobSelected.onlyRangedInRangedPattern,
                                                boss = mobSelected.boss

                                            }); 
                                        }
                                        resetMobStats();
                                        mobSelected = null;
                                        yScroll += 20;
                                    }
                                }
                                else
                                {
                                    GUI.backgroundColor = Color.black;
                                    GUI.contentColor = Color.black;
                                    GUI.Button(new Rect(10, 245, 135, 35), "Add to list \n" + "None");
                                    GUI.backgroundColor = H_GUIColors.GUIBackgroundColor;
                                    GUI.contentColor = H_GUIColors.GUIFrontColor;
                                }
                            }
                            
                            tolbarIntMenu = GUI.Toolbar(new Rect(10, 22, 410, 23), tolbarIntMenu, toolbarStringsAdvMenuCurrent);         
                            break;

                         
                        case 2:
                            //Position menu
                            if (tolbarIntPosition == 1)
                            {
                                tolbarIntMenu = GUI.Toolbar(new Rect(10, 22, 410, 23), tolbarIntMenu, toolbarStringsAdvMenuCurrent);    
                                
                                switch (tolbarIntPositionMode)
                                {
                                    case 0:
                                        
                                        showPositionPlayer();
                                        GUI.backgroundColor = Color.black;
                                        GUI.contentColor = Color.black;
                                        showPositionWaypoint();
                                        showPositionOther();
                                        GUI.backgroundColor = H_GUIColors.GUIBackgroundColor;
                                        GUI.contentColor = H_GUIColors.GUIFrontColor;
                                        break;
                                    case 1:
                                        GUI.backgroundColor = Color.black;
                                        GUI.contentColor = Color.black;
                                        showPositionPlayer();
                                        GUI.backgroundColor = H_GUIColors.GUIBackgroundColor;
                                        GUI.contentColor = H_GUIColors.GUIFrontColor;
                                        showPositionWaypoint();
                                        GUI.backgroundColor = Color.black;
                                        GUI.contentColor = Color.black;
                                        showPositionOther();
                                        GUI.backgroundColor = H_GUIColors.GUIBackgroundColor;
                                        GUI.contentColor = H_GUIColors.GUIFrontColor;
                                        break;
                                    case 2:
                                        GUI.backgroundColor = Color.black;
                                        GUI.contentColor = Color.black;
                                        showPositionPlayer();                                     
                                        showPositionWaypoint();
                                        GUI.backgroundColor = H_GUIColors.GUIBackgroundColor;
                                        GUI.contentColor = H_GUIColors.GUIFrontColor;
                                        showPositionOther();  
                                        break;
                                }
                                tolbarIntPositionMode = GUI.Toolbar(new Rect(10, 50, 410, 23), tolbarIntPositionMode, toolbarStringsPositionMode);
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

        void showPositionPlayer() {
            GUI.Box(new Rect(10, 50, 134, 230), "");
            playerListScrollPosition = GUI.BeginScrollView(new Rect(10, 80, 134, 190), playerListScrollPosition, new Rect(10, 80, 50, yPlayer), false, false);
            if (yPlayer < 190)
            {
                tolbarPlayerSelected = GUI.SelectionGrid(new Rect(15, 80, 125, yPlayer), tolbarPlayerSelected, playersListString, 1);
            }
            else
            {
                tolbarPlayerSelected = GUI.SelectionGrid(new Rect(15, 80, 110, yPlayer), tolbarPlayerSelected, playersListString, 1);
            }
            GUI.EndScrollView();
        }

        void showPositionWaypoint()
        {
            GUI.Box(new Rect(148, 50, 134, 230), "");
            waypointListScrollView = GUI.BeginScrollView(new Rect(148, 80, 134, 190), waypointListScrollView, new Rect(148, 80, 50, yWaypoints), false, false);
            if (yWaypoints < 190)
            {
                tolbarWaypointSelected = GUI.SelectionGrid(new Rect(153, 80, 125, yWaypoints), tolbarWaypointSelected, waypointsListString, 1);
            }
            else
            {
                tolbarWaypointSelected = GUI.SelectionGrid(new Rect(153, 80, 110, yWaypoints), tolbarWaypointSelected, waypointsListString, 1);
            }
            GUI.EndScrollView();
        }

        void showPositionOther()
        {
            GUI.Box(new Rect(286, 50, 134, 230), "");
            tolbarOthersSelected = GUI.SelectionGrid(new Rect(291, 80, 125, 25), tolbarOthersSelected, toolbarStringsOthersMode, 1);
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
                if (mob.statsEdited)
                {
                    GUI.Label(new Rect(160, y, 230, 17), mob.name + " | x" + mob.multiplier + " | x" + mob.quantity + " | stats edited", centeredStyle);
                }
                else
                {
                    GUI.Label(new Rect(160, y, 230, 17), mob.name + " | x" + mob.multiplier + " | x" + mob.quantity, centeredStyle);
                }
                  
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
                if (mob.statsEdited)
                {
                    if (GUI.Button(new Rect(160, y, 230, 20), mob.name + " | x" + mob.multiplier + " |  x" + mob.quantity + " | stats edited"))
                    {
                        mobListTab.Remove(mob);
                        yScroll -= 20;
                    }
                }
                else {
                    if (GUI.Button(new Rect(160, y, 230, 20), mob.name + " | x" + mob.multiplier + " |  x" + mob.quantity))
                    {
                        mobListTab.Remove(mob);
                        yScroll -= 20;
                    }
                }                     
                y += 20;
            }
            GUI.EndScrollView();
        }

        void buttonSetting(MobType mob)
        {
            //If list enabled
            if (tolbarIntList == 1)
            {
                //if in List menu
                if (tolbarIntMenu == 1)
                {
                    if (mobSelected == mob)
                    {
                        mobSelected = null;
                    }
                    else
                    {
                        mobSelected = mob;
                    }
                }
                //if in first menu
                else
                {
                    //If mob is already in list modify it quantity                   
                    if (mobListTab.Exists(x => x.id == mob.id) && mobListTab.Exists(x => x.multiplier == powerMultiplierAmount) && mobListTab.Exists(x => x.statsEdited == false))
                    {
                        var sameMob = mobListTab.Find(x => (x.id == mob.id) && (x.multiplier == powerMultiplierAmount) && (x.statsEdited == false));
                        sameMob.quantity += ItemSpawnerAmount;
                    }
                    //if is not already in list add it
                    else
                    {
                        mobListTab.Add(new mobList() { name = mob.name, id = mob.id, multiplier = powerMultiplierAmount, quantity = ItemSpawnerAmount, statsEdited = false }); ;
                        yScroll += 20;
                    }
                }
            }
            //If list is disabled
            else
            {
                //if in list menu
                if (tolbarIntMenu == 1)
                {
                    if (mobSelected == mob)
                    {
                        mobSelected = null;
                    }
                    else
                    {
                        mobSelected = mob;
                    }
                }
                //if in first menu
                else
                {
                    for (int j = 0; j < ItemSpawnerAmount; j++)
                    {
                        MobSpawner.Instance.ServerSpawnNewMob(MobManager.Instance.GetNextId(), mob.id, spawnPosition, powerMultiplierAmount, multiBossAmount, Mob.BossType.None, -1);
                    }
                }
            }

        }

        void showMobsAdvanced()
        {
            
            GUI.Box(new Rect(150, 50, 250, 230), "Mobs list");
            mobListScrollPositition = GUI.BeginScrollView(new Rect(160, 80, 260, 200), mobListScrollPositition, new Rect(140, 50, 240, 290), false, true);
            int x2 = 140;
            int y2 = 50;
            foreach (MobType mob in MobSpawner.Instance.allMobs)
            {
                //mob Selected
                if (mobSelected != null)
                {
                    //mob selected is the same as mob element
                    if(mob.name == mobSelected.name)
                    {
                        GUI.backgroundColor = Color.green;
                        if (GUI.Button(new Rect(x2, y2, 110, 22), new GUIContent(mob.name, mob.ranged + "$" +mob.rangedCooldown + "=" +mob.startAttackDistance + ")" +mob.startRangedAttackDistance + "à" +mob.maxAttackDistance + "ç" +mob.speed + "_" +mob.minAttackAngle + "è" + mob.sharpDefense + "-" +mob.defense + "(" +mob.knockbackThreshold + "'" +mob.ignoreBuilds + "é" +mob.followPlayerDistance + "&" +mob.followPlayerAccuracy + "²" +mob.onlyRangedInRangedPattern + "*" +mob.boss + "/")))
                        {
                            buttonSetting(mob);
                        }
                        GUI.backgroundColor = H_GUIColors.GUIBackgroundColor;
                        if (x2 == 260)
                        {
                            x2 = 140; y2 += 32;
                        }
                        else
                            x2 += 120;
                    }
                    //mob selected is not the same as mob element
                    else
                    {
                        if (GUI.Button(new Rect(x2, y2, 110, 22), new GUIContent(mob.name, mob.ranged + "$" + mob.rangedCooldown + "=" + mob.startAttackDistance + ")" + mob.startRangedAttackDistance + "à" + mob.maxAttackDistance + "ç" + mob.speed + "_" + mob.minAttackAngle + "è" + mob.sharpDefense + "-" + mob.defense + "(" + mob.knockbackThreshold + "'" + mob.ignoreBuilds + "é" + mob.followPlayerDistance + "&" + mob.followPlayerAccuracy + "²" + mob.onlyRangedInRangedPattern + "*" + mob.boss + "/")))
                        {
                            buttonSetting(mob);
                        }
                        GUI.backgroundColor = H_GUIColors.GUIBackgroundColor;
                        if (x2 == 260)
                        {
                            x2 = 140; y2 += 32;
                        }
                        else
                            x2 += 120;
                    }
                }
                //No mob selected
                else
                {
                    if (GUI.Button(new Rect(x2, y2, 110, 22), new GUIContent(mob.name, mob.ranged + "$" + mob.rangedCooldown + "=" + mob.startAttackDistance + ")" + mob.startRangedAttackDistance + "à" + mob.maxAttackDistance + "ç" + mob.speed + "_" + mob.minAttackAngle + "è" + mob.sharpDefense + "-" + mob.defense + "(" + mob.knockbackThreshold + "'" + mob.ignoreBuilds + "é" + mob.followPlayerDistance + "&" + mob.followPlayerAccuracy + "²" + mob.onlyRangedInRangedPattern + "*" + mob.boss + "/")))
                    {
                        buttonSetting(mob);
                    }
                    if (x2 == 260)
                    {
                        x2 = 140; y2 += 32;
                    }
                    else
                        x2 += 120;
                }  
            }
            GUI.EndScrollView();
        }


        void mobStatsEdit()
        {
            mobStatsListScrollPosition = GUI.BeginScrollView(new Rect(15, 80, 125, 155), mobStatsListScrollPosition, new Rect(15, 80, 100, 415), false, true);

            mobSelected.ranged =  GUI.Toggle(new Rect(15, 80, 105, 23), mobSelected.ranged, "Ranged");
            mobSelected.onlyRangedInRangedPattern = GUI.Toggle(new Rect(15, 100, 105, 23), mobSelected.onlyRangedInRangedPattern, "Ranged Pattern");
            mobSelected.boss = GUI.Toggle(new Rect(15, 120, 105, 23), mobSelected.boss, "Boss");
            mobSelected.ignoreBuilds = GUI.Toggle(new Rect(15, 140, 105, 23), mobSelected.ignoreBuilds, "Ignore build");
            GUI.Label(new Rect(15, 166, 100, 23), "Ranged cooldown: " + mobSelected.rangedCooldown, styleForStats);
            mobSelected.rangedCooldown = GUI.HorizontalSlider(new Rect(15, 180, 100, 23), mobSelected.rangedCooldown, 0f, 100f);
            GUI.Label(new Rect(15, 196, 100, 23), "Start atk. dist: " + mobSelected.startAttackDistance, styleForStats);
            mobSelected.startAttackDistance = GUI.HorizontalSlider(new Rect(15, 210, 100, 23), mobSelected.startAttackDistance, 0f, 100f);
            GUI.Label(new Rect(15, 226, 100, 23), "Start rang. atk. dist: " + mobSelected.startRangedAttackDistance, styleForStats);
            mobSelected.startRangedAttackDistance = GUI.HorizontalSlider(new Rect(15, 240, 100, 23), mobSelected.startRangedAttackDistance, 0f, 100f);
            GUI.Label(new Rect(15, 256, 100, 23), "Max atk dist: " + mobSelected.maxAttackDistance, styleForStats);
            mobSelected.maxAttackDistance = GUI.HorizontalSlider(new Rect(15, 270, 100, 23), mobSelected.maxAttackDistance, 0f, 300f);
            GUI.Label(new Rect(15, 286, 100, 23), "Speed: " + mobSelected.speed, styleForStats);
            mobSelected.speed = GUI.HorizontalSlider(new Rect(15, 300, 100, 23), mobSelected.speed, 0f, 100f);
            GUI.Label(new Rect(15, 316, 100, 23), "Min atk angle: " + mobSelected.minAttackAngle, styleForStats);
            mobSelected.minAttackAngle = GUI.HorizontalSlider(new Rect(15, 330, 100, 23), mobSelected.minAttackAngle, 0f, 300f);
            GUI.Label(new Rect(15, 346, 100, 23), "Sharp defense: " + mobSelected.sharpDefense, styleForStats);
            mobSelected.sharpDefense = GUI.HorizontalSlider(new Rect(15, 360, 100, 23), mobSelected.sharpDefense, 0f, 100f);
            GUI.Label(new Rect(15, 376, 100, 23), "Defense: " + mobSelected.defense, styleForStats);
            mobSelected.defense = GUI.HorizontalSlider(new Rect(15, 390, 100, 23), mobSelected.defense, 0f, 100f);
            GUI.Label(new Rect(15, 406, 100, 23), "KB Treshold: " + mobSelected.knockbackThreshold, styleForStats);
            mobSelected.knockbackThreshold = GUI.HorizontalSlider(new Rect(15, 420, 105, 23), mobSelected.knockbackThreshold, 0f, 100f);
            GUI.Label(new Rect(15, 436, 100, 23), "Follow player Dist: " + mobSelected.followPlayerDistance, styleForStats);
            mobSelected.followPlayerDistance = GUI.HorizontalSlider(new Rect(15, 450, 105, 23), mobSelected.followPlayerDistance, 0f, 100f);
            GUI.Label(new Rect(15, 466, 100, 23), "Follow player Acc: " + mobSelected.followPlayerAccuracy, styleForStats);
            mobSelected.followPlayerAccuracy = GUI.HorizontalSlider(new Rect(15, 480, 105, 23), mobSelected.followPlayerAccuracy, 0f, 100f);

            GUI.EndScrollView();
        }

        void loadMobStats()
        {
            tooltipString = GUI.tooltip;
            rangedString = Variables.deleteAllAfter(tooltipString, "$");           
            ranged = Variables.stringToBool(rangedString);
            rangedCooldownString = Variables.deleteAllBeetween("$", tooltipString, "=");
            rangedCooldown = Variables.stringToFloat(rangedCooldownString);
            startAttackDistanceString = Variables.deleteAllBeetween("=", tooltipString, ")");
            startAttackDistance = Variables.stringToFloat(startAttackDistanceString);
            startRangedAttackDistanceString = Variables.deleteAllBeetween(")", tooltipString, "à");
            startRangedAttackDistance = Variables.stringToFloat(startRangedAttackDistanceString);
            maxAttackDistanceString = Variables.deleteAllBeetween("à", tooltipString, "ç");
            maxAttackDistance = Variables.stringToFloat(maxAttackDistanceString);
            speedString = Variables.deleteAllBeetween("ç", tooltipString, "_");
            speed = Variables.stringToFloat(speedString);
            minAttackAngleString = Variables.deleteAllBeetween("_", tooltipString, "è");
            minAttackAngle = Variables.stringToFloat(minAttackAngleString);
            sharpDefenseString = Variables.deleteAllBeetween("è", tooltipString, "-");
            sharpDefense = Variables.stringToFloat(sharpDefenseString);
            defenseString = Variables.deleteAllBeetween("-", tooltipString, "(");
            defense = Variables.stringToFloat(defenseString);
            knockbackThresholdString = Variables.deleteAllBeetween("(", tooltipString, "'");
            knockbackThreshold = Variables.stringToFloat(knockbackThresholdString);
            ignoreBuildsString = Variables.deleteAllBeetween("'", tooltipString, "é");
            ignoreBuilds = Variables.stringToBool(ignoreBuildsString);
            followPlayerDistanceString = Variables.deleteAllBeetween("é", tooltipString, "&");
            followPlayerDistance = Variables.stringToFloat(followPlayerDistanceString);
            followPlayerAccuracyString = Variables.deleteAllBeetween("&", tooltipString, "²");
            followPlayerAccuracy = Variables.stringToFloat(followPlayerAccuracyString);
            onlyRangedInRangedPatternString = Variables.deleteAllBeetween("²", tooltipString, "*");
            onlyRangedInRangedPattern = Variables.stringToBool(onlyRangedInRangedPatternString);
            bossString = Variables.deleteAllBeetween("*", tooltipString, "/");
            boss = Variables.stringToBool(bossString);

            if (GUI.tooltip.Length > 1)
            {
                prevRanged = ranged;
                prevRangedCooldown = rangedCooldown;
                prevStartAttackDistance = startAttackDistance;
                prevStartRangedAttackDistance = startRangedAttackDistance;
                prevMaxAttackDistance = maxAttackDistance;
                prevSpeed = speed;
                prevMinAttackAngle = minAttackAngle;
                prevSharpDefense = sharpDefense;
                prevDefense = defense;
                prevKnockbackThreshold = knockbackThreshold;
                prevIgnoreBuilds = ignoreBuilds;
                prevFollowPlayerDistance = followPlayerDistance;
                prevFollowPlayerAccuracy = followPlayerAccuracy;
                prevOnlyRangedInRangedPattern = onlyRangedInRangedPattern;
                prevBoss = boss;
            }

            mobStatsListScrollPosition = GUI.BeginScrollView(new Rect(15, 80, 125, 155), mobStatsListScrollPosition, new Rect(15, 80, 100, 415), false, true);
            /*if (tolbarIntList == 1)
            {
                GUI.Toggle(new Rect(15, 80, 105, 23), prevRanged, "Ranged");
                GUI.Toggle(new Rect(15, 100, 105, 23), prevOnlyRangedInRangedPattern, "Ranged Pattern");
                GUI.Toggle(new Rect(15, 120, 105, 23), prevBoss, "Boss");
                GUI.Toggle(new Rect(15, 140, 105, 23), prevIgnoreBuilds, "Ignore build");
                GUI.Label(new Rect(15, 166, 100, 23), "Ranged cooldown: " + prevRangedCooldown, styleForStats);
                GUI.HorizontalSlider(new Rect(15, 180, 100, 23), prevRangedCooldown, 0f, 100f);
                GUI.Label(new Rect(15, 196, 100, 23), "Start atk. dist: " + prevStartAttackDistance, styleForStats);
                GUI.HorizontalSlider(new Rect(15, 210, 100, 23), prevStartAttackDistance, 0f, 100f);
                GUI.Label(new Rect(15, 226, 100, 23), "Start rang. atk. dist: " + prevStartRangedAttackDistance, styleForStats);
                GUI.HorizontalSlider(new Rect(15, 240, 100, 23), prevStartRangedAttackDistance, 0f, 100f);
                GUI.Label(new Rect(15, 256, 100, 23), "Max atk dist: " + prevMaxAttackDistance, styleForStats);
                GUI.HorizontalSlider(new Rect(15, 270, 100, 23), prevMaxAttackDistance, 0f, 300f);
                GUI.Label(new Rect(15, 286, 100, 23), "Speed: " + prevSpeed, styleForStats);
                GUI.HorizontalSlider(new Rect(15, 300, 100, 23), prevSpeed, 0f, 100f);
                GUI.Label(new Rect(15, 316, 100, 23), "Min atk angle: " + prevMinAttackAngle, styleForStats);
                GUI.HorizontalSlider(new Rect(15, 330, 100, 23), prevMinAttackAngle, 0f, 300f);
                GUI.Label(new Rect(15, 346, 100, 23), "Sharp defense: " + prevSharpDefense, styleForStats);
                GUI.HorizontalSlider(new Rect(15, 360, 100, 23), prevSharpDefense, 0f, 100f);
                GUI.Label(new Rect(15, 376, 100, 23), "Defense: " + prevDefense, styleForStats);
                GUI.HorizontalSlider(new Rect(15, 390, 100, 23), prevDefense, 0f, 100f);
                GUI.Label(new Rect(15, 406, 100, 23), "KB Treshold: " + prevKnockbackThreshold, styleForStats);
                GUI.HorizontalSlider(new Rect(15, 420, 105, 23), prevKnockbackThreshold, 0f, 100f);
                GUI.Label(new Rect(15, 436, 100, 23), "Follow player Dist: " + prevFollowPlayerDistance, styleForStats);
                GUI.HorizontalSlider(new Rect(15, 450, 105, 23), prevFollowPlayerDistance, 0f, 100f);
                GUI.Label(new Rect(15, 466, 100, 23), "Follow player Acc: " + prevFollowPlayerAccuracy, styleForStats);
                GUI.HorizontalSlider(new Rect(15, 480, 105, 23), prevFollowPlayerAccuracy, 0f, 100f);
            }
            else
            {*/
                if (mobSelected == null)
                {
                    GUI.Toggle(new Rect(15, 80, 105, 23), prevRanged, "Ranged");
                    GUI.Toggle(new Rect(15, 100, 105, 23), prevOnlyRangedInRangedPattern, "Ranged Pattern");
                    GUI.Toggle(new Rect(15, 120, 105, 23), prevBoss, "Boss");
                    GUI.Toggle(new Rect(15, 140, 105, 23), prevIgnoreBuilds, "Ignore build");
                    GUI.Label(new Rect(15, 166, 100, 23), "Ranged cooldown: " + prevRangedCooldown, styleForStats);
                    GUI.HorizontalSlider(new Rect(15, 180, 100, 23), prevRangedCooldown, 0f, 100f);
                    GUI.Label(new Rect(15, 196, 100, 23), "Start atk. dist: " + prevStartAttackDistance, styleForStats);
                    GUI.HorizontalSlider(new Rect(15, 210, 100, 23), prevStartAttackDistance, 0f, 100f);
                    GUI.Label(new Rect(15, 226, 100, 23), "Start rang. atk. dist: " + prevStartRangedAttackDistance, styleForStats);
                    GUI.HorizontalSlider(new Rect(15, 240, 100, 23), prevStartRangedAttackDistance, 0f, 100f);
                    GUI.Label(new Rect(15, 256, 100, 23), "Max atk dist: " + prevMaxAttackDistance, styleForStats);
                    GUI.HorizontalSlider(new Rect(15, 270, 100, 23), prevMaxAttackDistance, 0f, 300f);
                    GUI.Label(new Rect(15, 286, 100, 23), "Speed: " + prevSpeed, styleForStats);
                    GUI.HorizontalSlider(new Rect(15, 300, 100, 23), prevSpeed, 0f, 100f);
                    GUI.Label(new Rect(15, 316, 100, 23), "Min atk angle: " + prevMinAttackAngle, styleForStats);
                    GUI.HorizontalSlider(new Rect(15, 330, 100, 23), prevMinAttackAngle, 0f, 300f);
                    GUI.Label(new Rect(15, 346, 100, 23), "Sharp defense: " + prevSharpDefense, styleForStats);
                    GUI.HorizontalSlider(new Rect(15, 360, 100, 23), prevSharpDefense, 0f, 100f);
                    GUI.Label(new Rect(15, 376, 100, 23), "Defense: " + prevDefense, styleForStats);
                    GUI.HorizontalSlider(new Rect(15, 390, 100, 23), prevDefense, 0f, 100f);
                    GUI.Label(new Rect(15, 406, 100, 23), "KB Treshold: " + prevKnockbackThreshold, styleForStats);
                    GUI.HorizontalSlider(new Rect(15, 420, 105, 23), prevKnockbackThreshold, 0f, 100f);
                    GUI.Label(new Rect(15, 436, 100, 23), "Follow player Dist: " + prevFollowPlayerDistance, styleForStats);
                    GUI.HorizontalSlider(new Rect(15, 450, 105, 23), prevFollowPlayerDistance, 0f, 100f);
                    GUI.Label(new Rect(15, 466, 100, 23), "Follow player Acc: " + prevFollowPlayerAccuracy, styleForStats);
                    GUI.HorizontalSlider(new Rect(15, 480, 105, 23), prevFollowPlayerAccuracy, 0f, 100f);

                }
           // }
            
            GUI.EndScrollView();
        }

        void resetMobStats()
        {
            mobSelected.ranged = prevRanged;
            mobSelected.rangedCooldown = prevRangedCooldown;
            mobSelected.startAttackDistance = prevStartAttackDistance;
            mobSelected.startRangedAttackDistance = prevStartRangedAttackDistance;
            mobSelected.maxAttackDistance = prevMaxAttackDistance;
            mobSelected.speed = prevSpeed;
            mobSelected.minAttackAngle = prevMinAttackAngle;
            mobSelected.sharpDefense = prevSharpDefense;
            mobSelected.defense = prevDefense;
            mobSelected.knockbackThreshold = prevKnockbackThreshold;
            mobSelected.ignoreBuilds = prevIgnoreBuilds;
            mobSelected.followPlayerDistance = prevFollowPlayerDistance;
            mobSelected.followPlayerAccuracy = prevFollowPlayerAccuracy;
            mobSelected.onlyRangedInRangedPattern = prevOnlyRangedInRangedPattern;
            mobSelected.boss = prevBoss;

            mobSelected = null;
        }

        void showMobNormal()
        {
            GUI.Box(new Rect(150, 25, 250, 255), "Mobs list");
            mobListScrollPositition = GUI.BeginScrollView(new Rect(160, 50, 260, 230), mobListScrollPositition, new Rect(140, 25, 240, 290), false, true);
            int x = 140;
            int y = 25;
            foreach (MobType mob in MobSpawner.Instance.allMobs)
            {
                if (GUI.Button(new Rect(x, y, 110, 22), mob.name))
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
                if (mob.statsEdited)
                {
                    Array.Find(MobSpawner.Instance.allMobs, x => x.id == mob.id).speed = mob.speed;
                    for (int j = 0; j < mob.quantity; j++)
                    {
                        MobSpawner.Instance.ServerSpawnNewMob(MobManager.Instance.GetNextId(), mob.id, spawnPosition, mob.multiplier, 1, Mob.BossType.None, -1);
                    }
                }
                else
                {
                    for (int j = 0; j < mob.quantity; j++)
                    {
                        MobSpawner.Instance.ServerSpawnNewMob(MobManager.Instance.GetNextId(), mob.id, spawnPosition, mob.multiplier, 1, Mob.BossType.None, -1);
                    }
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
        public bool statsEdited { get; set; }
        public bool ranged { get; set; }
        public float rangedCooldown { get; set; }
        public float startAttackDistance { get; set; }
        public float startRangedAttackDistance { get; set; }
        public float maxAttackDistance { get; set; }
        public float speed { get; set; }
        public float minAttackAngle { get; set; }
        public float sharpDefense { get; set; }
        public float defense { get; set; }
        public float knockbackThreshold { get; set; }
        public bool ignoreBuilds { get; set; }
        public float followPlayerDistance { get; set; }
        public float followPlayerAccuracy { get; set; }
        public bool onlyRangedInRangedPattern { get; set; }
        public bool boss { get; set; }
    }
}