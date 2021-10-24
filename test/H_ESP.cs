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
    public class H_ESP : Menu
    {
		public bool player;
		public bool playersnapline;
		public int lineposition;

		public H_ESP() : base(new Rect(1680, 10, 230, 400), "ESP Menu", 8, false) { }

		public void Update()
        {
			
		}
		public override void runWin(int id)
		{
			
			base.runWin(id);
        }

	}
}