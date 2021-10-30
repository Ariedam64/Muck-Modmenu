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
    public class H_Waypoints : Menu
    {
      
        public string prevMobDefense, mobDefense;

        public H_Waypoints() : base(new Rect(1460, 420, 430, 200), "Waypoints menu", 9, false) { }

        public void Update()
        {

        }
        public override void runWin(int id)
        {



            base.runWin(id);
        }

    }
}