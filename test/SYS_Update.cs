using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using test.CT_System;
using Steamworks;

namespace test.CT_Hacks
{
	public class SYS_Update : Menu
	{
		public SYS_Update() : base(new Rect(105, 150, 200, 200), "MAH UPDATE", 10, false) { }

		public void Update()
        {
			
		}
		public override void runWin(int id)
		{
			GUI.backgroundColor = H_GUIColors.GUIBackgroundColor;
			GUI.contentColor = H_GUIColors.GUIFrontColor;
			GUILayout.Label("");


			base.runWin(id);
        }

		
    }
}