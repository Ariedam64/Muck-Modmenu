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
        public int ItemSpawnerAmount = 1;
        public int powerMultiplierAmount = 1;
        public int multiBossAmount = 1;
        public int speedAmount = 1;
        public int mobId;

        public string prevTooltip = "";
        public string mobStats;
        public string prevMobSpeed, mobSpeed;
        public string prevMobMaxAttackDistance ,maxAttackDistance;
        public string prevMobBoss, mobBoss;
        public string prevMobDefense, mobDefense;

        internal static object GetInstanceField(Type type, object instance, string fieldName)
        {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Static;
            FieldInfo field = type.GetField(fieldName, bindFlags);
            return field.GetValue(instance);
        }

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

            //Pour avoir le nombre de mob qui ont spawn
            mobId = (int)GetInstanceField(typeof(MobManager), MobManager.Instance, "mobId");

            for (int i = 0; i < MobSpawner.Instance.allMobs.Length; i++)
            {
                MobType mob = MobSpawner.Instance.allMobs[i];
                if (GUI.Button(new Rect(x, y, 120, 22), new GUIContent(mob.name, mob.maxAttackDistance + "$1" + mob.speed)))
                {
                    for (int j = 0; j < ItemSpawnerAmount; j++){
                        MobSpawner.Instance.ServerSpawnNewMob(MobManager.Instance.GetNextId(), mob.id, PlayerMovement.Instance.GetRb().position, powerMultiplierAmount, multiBossAmount, Mob.BossType.None, -1);
                        MobManager.Instance.mobs[mobId].SetSpeed(mob.speed * speedAmount);
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
            GUI.Box(new Rect(10, 10, 140, 210), "Stats");
            GUI.Label(new Rect(15, 30, 95, 20), "Quantity : x" + ItemSpawnerAmount);
            ItemSpawnerAmount = (int)GUI.HorizontalSlider(new Rect(15, 50, 130, 20), ItemSpawnerAmount, 1, 100);
            GUI.Label(new Rect(15, 60, 140, 20), "Power + health : x" + powerMultiplierAmount);
            powerMultiplierAmount = (int)GUI.HorizontalSlider(new Rect(15, 80, 130, 20), powerMultiplierAmount, 1, 50);
            GUI.Label(new Rect(15, 90, 140, 20), "[Visual] Speed : x" + speedAmount);
            speedAmount = (int)GUI.HorizontalSlider(new Rect(15, 110, 130, 20), speedAmount, 1, 10);

            GUI.Label(new Rect(15, 140, 250, 60), "Speed : " + floatMobSpeed * speedAmount);
            GUI.Label(new Rect(15, 165, 140, 25), "Attack distance : " + prevMobMaxAttackDistance + "m");



            base.runWin(id);
        }

    }
}