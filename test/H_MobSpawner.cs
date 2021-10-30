using System.Collections.Generic;
using UnityEngine;
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
        public int ItemSpawnerAmount = 1;
        public int powerMultiplierAmount = 1;
        public int multiBossAmount = 1;
        public int speedAmount = 1;

        public string[] toolbarStrings = { "Me", "Cursor" };
        public int toolbarInt = 0;
        public string prevTooltip = "";
        public string mobStats;
        public string prevMobSpeed, mobSpeed;
        public string prevMobMaxAttackDistance ,maxAttackDistance;
        public string prevMobBoss, mobBoss;
        public string prevMobDefense, mobDefense;

        public H_MobSpawner() : base(new Rect(1040, 420, 430, 200), "Mob Spawner menu", 7, false) { }

        public void Update()
        {

        }
        public override void runWin(int id)
        {
            ScrollPosition2 = GUILayout.BeginScrollView(ScrollPosition2, false, true, GUILayout.Width(430), GUILayout.Height(200));

            int x = 160;
            int y = 5;
            int buttonWidth = 32;       

            foreach (MobType mob in MobSpawner.Instance.allMobs)
            {
                if (GUI.Button(new Rect(x, y, 120, 22), new GUIContent(mob.name, mob.maxAttackDistance + "$1" + mob.speed)))
                {
                    switch (toolbarInt)
                    {
                        case 0:
                            for (int j = 0; j < ItemSpawnerAmount; j++)
                            {
                                MobSpawner.Instance.ServerSpawnNewMob(MobManager.Instance.GetNextId(), mob.id, PlayerMovement.Instance.GetRb().position, powerMultiplierAmount, multiBossAmount, Mob.BossType.None, -1);
                                MobManager.Instance.mobs[Variables.getMobIntancied() - 1].SetSpeed(speedAmount);
                            }
                            break;
                        case 1:
                            for (int j = 0; j < ItemSpawnerAmount; j++)
                            {
                                MobSpawner.Instance.ServerSpawnNewMob(MobManager.Instance.GetNextId(), mob.id, Variables.FindTpPos(), powerMultiplierAmount, multiBossAmount, Mob.BossType.None, -1);
                                MobManager.Instance.mobs[Variables.getMobIntancied() - 1].SetSpeed(speedAmount);
                            }
                            break;
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

            

            mobStats = GUI.tooltip;
            mobSpeed = mobStats.Substring(mobStats.IndexOf("$1") + 1);
            int index = mobStats.LastIndexOf("$1");
            if (index >= 0)
            {
                maxAttackDistance = mobStats.Substring(0, index);
            }

            if (GUI.tooltip.Length > 1)
            {
                prevTooltip = GUI.tooltip;
                prevMobSpeed = mobSpeed;
                prevMobMaxAttackDistance = maxAttackDistance;
            }

            //Convertir le speed en float
            float floatMobSpeed;
            float.TryParse(prevMobSpeed, out floatMobSpeed);


            //Affichage stats
            GUI.Box(new Rect(10, 25, 145, 150), "Stats ");
            GUI.Label(new Rect(15, 45, 95, 20), "Quantity : x" + ItemSpawnerAmount);
            ItemSpawnerAmount = (int)GUI.HorizontalSlider(new Rect(15, 65, 130, 20), ItemSpawnerAmount, 1, 100);
            GUI.Label(new Rect(15, 75, 140, 20), "Power + health : x" + powerMultiplierAmount);
            powerMultiplierAmount = (int)GUI.HorizontalSlider(new Rect(15, 95, 130, 20), powerMultiplierAmount, 1, 50);
            GUI.Label(new Rect(15, 105, 140, 20), "[Client] Speed : x" + speedAmount);
            speedAmount = (int)GUI.HorizontalSlider(new Rect(15, 125, 130, 20), speedAmount, 1, 10);

            GUI.Label(new Rect(15, 135, 250, 60), "Speed : " + floatMobSpeed * speedAmount);
            GUI.Label(new Rect(15, 155, 140, 25), "Attack distance : " + prevMobMaxAttackDistance + "m");

            GUI.Label(new Rect(15, 175, 250, 23), "Spawn on: ");
            toolbarInt = GUI.Toolbar(new Rect(10, 195, 143, 23), toolbarInt, toolbarStrings);



            base.runWin(id);
        }

    }
}