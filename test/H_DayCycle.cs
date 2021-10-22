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
        private float timeDay;
        private float prevTimeDay;
        private bool frooze = false;


        public H_DayCycle() : base(new Rect(1250, 10, 200, 200), "Day Cycle menu", 4, false) { }

        public void Update()

        {
            if (timeDay != prevTimeDay)
            {
                DayCycle.time = timeDay/100;
                prevTimeDay = timeDay;
            }

            if (frooze)
            {

            }

        }
        public override void runWin(int id)
        {

            if (GUILayout.Button("Start Day"))
            {
                DayCycle.time = startDay;
            }
            if (GUILayout.Button("Middle ay"))
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

            GUILayout.Label("Day time: " + timeDay);
            timeDay = (float)Math.Round(GUILayout.HorizontalSlider(timeDay, 0f, 100f), 1);


            frooze = GUILayout.Toggle(frooze, "Frooze");

            base.runWin(id);
        }

    }
}