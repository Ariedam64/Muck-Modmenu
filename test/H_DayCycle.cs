using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Runtime.InteropServices;
using System.Reflection;

namespace test.CT_Hacks
{
    public class H_DayCycle : Menu
    {

        private float startDay = 0f;
        private float middleDay = 0.25f;
        private float startNight = 0.5f;
        private float middleNight = 0.75f;
        private float speedTime = 1f;
        private float prevSpeedTime = 1f;
        private float timeSet;
        public int currentDay=0;
        private bool frooze = false;
        public static DayCycle dayCycleInstance;
        public H_DayCycle() : base(new Rect(1250, 10, 200, 200), "Day Cycle menu", 4, false) { }

        public void Update()
        {
            if (frooze)
            {
                DayCycle.dayDuration = 99999f;
            }
            else
            {
                
                if(speedTime == 1)
                {
                    DayCycle.dayDuration= 100f;
                }
                else
                {
                    if (speedTime != prevSpeedTime)
                    {
                        timeSet = 100 / speedTime;
                        prevSpeedTime = speedTime;
                    }
                    DayCycle.dayDuration = timeSet;
                }

                
            }
        }
        public override void runWin(int id)
        {
            GUI.backgroundColor = H_GUIColors.GUIBackgroundColor;
            GUI.contentColor = H_GUIColors.GUIFrontColor;
            if (GUILayout.Button("Start Day"))
            {
                DayCycle.time = startDay;
            }
            if (GUILayout.Button("Middle Day"))
            {
                DayCycle.time = middleDay;
            }
            if (GUILayout.Button("Start Night"))
            {
                DayCycle.time = startNight;
            }
            if (GUILayout.Button("Middle Night"))
            {
                DayCycle.time = middleNight;
            }
            GUILayout.Label("Time multiplier: " + speedTime);
            speedTime = (float)Math.Round(GUILayout.HorizontalSlider(speedTime, 1f, 10000f), 1);
            GUILayout.Label("Current day: " + currentDay);
            currentDay = (int)Math.Round(GUILayout.HorizontalSlider(currentDay, 0, 1000), 1);
            if (GUILayout.Button("Set day"))
            {
                GameManager.instance.UpdateDay(currentDay);
            }
            frooze = GUILayout.Toggle(frooze, "Freeze time");
            base.runWin(id);
        }
    }
}