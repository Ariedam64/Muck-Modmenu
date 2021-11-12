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
    public class H_GUIColors : Menu
    {
		public static Vector2 GUIColorsScrollPosition { get; set; } = Vector2.zero;

		public int toolbarIntModeSelected = 0;
		public int toolbarIntGUIContent = 0;
		public int toolbarIntGUIBackgroundColor = 0;
		public int toolbarIntGUIContentColor = 0;
		public int toolbarIntGUIMenu = 0;
		public int toolbarIntGUIBackgroundMenu = 0;
		public int toolbarIntGUIContentMenu = 0;
		public static Color GUIBackgroundColor;
		public static Color GUIFrontColor;
		public static Color MenuBackgroundColor;
		public static Color MenuFrontColor;
		public static Color GUIOriginalbackgroundColor = GUI.backgroundColor;
		public static Color GUIOriginalContentColor = GUI.contentColor;
		public string[] toolbarStringModeSelected= { "Menus", "Contents" };
		public string[] toolbarStringGUI = { "Objects", "Labels" };
		public string[] toolbarStringMenu = { "Border", "Labels" };
		public string[] toolbarStringGUIColors = { "Original", "Black", "Red", "Green", "Blue", "Cyan", "Gray", "Grey", "Magenta", "Yellow" };


		public H_GUIColors() : base(new Rect(1680, 10, 230, 335), "Customize your cheat!", 8, false) { }

		public void Update()
        {
			switch (toolbarIntGUIBackgroundColor)
			{
				case 0:
					GUIBackgroundColor = GUIOriginalbackgroundColor;
					break;
				case 1:
					GUIBackgroundColor = Color.black;
					break;
				case 2:
					GUIBackgroundColor = Color.red;
					break;
				case 3:
					GUIBackgroundColor = Color.green;
					break;
				case 4:
					GUIBackgroundColor = Color.blue;
					break;
				case 5:
					GUIBackgroundColor = Color.cyan;
					break;
				case 6:
					GUIBackgroundColor = Color.gray;
					break;
				case 7:
					GUIBackgroundColor = Color.grey;
					break;
				case 8:
					GUIBackgroundColor = Color.magenta;
					break;
				case 9:
					GUIBackgroundColor = Color.yellow;
					break;
			}
			switch (toolbarIntGUIContentColor)
			{
				case 0:
					GUIFrontColor = GUIOriginalContentColor;
					break;
				case 1:
					GUIFrontColor = Color.black;
					break;
				case 2:
					GUIFrontColor = Color.red;
					break;
				case 3:
					GUIFrontColor = Color.green;
					break;
				case 4:
					GUIFrontColor = Color.blue;
					break;
				case 5:
					GUIFrontColor = Color.cyan;
					break;
				case 6:
					GUIFrontColor = Color.gray;
					break;
				case 7:
					GUIFrontColor = Color.grey;
					break;
				case 8:
					GUIFrontColor = Color.magenta;
					break;
				case 9:
					GUIFrontColor = Color.yellow;
					break;
			}
			switch (toolbarIntGUIBackgroundMenu)
			{
				case 0:
					MenuBackgroundColor = GUIOriginalbackgroundColor;
					break;
				case 1:
					MenuBackgroundColor = Color.black;
					break;
				case 2:
					MenuBackgroundColor = Color.red;
					break;
				case 3:
					MenuBackgroundColor = Color.green;
					break;
				case 4:
					MenuBackgroundColor = Color.blue;
					break;
				case 5:
					MenuBackgroundColor = Color.cyan;
					break;
				case 6:
					MenuBackgroundColor = Color.gray;
					break;
				case 7:
					MenuBackgroundColor = Color.grey;
					break;
				case 8:
					MenuBackgroundColor = Color.magenta;
					break;
				case 9:
					MenuBackgroundColor = Color.yellow;
					break;
			}
			switch (toolbarIntGUIContentMenu)
			{
				case 0:
					MenuFrontColor = GUIOriginalContentColor;
					break;
				case 1:
					MenuFrontColor = Color.black;
					break;
				case 2:
					MenuFrontColor = Color.red;
					break;
				case 3:
					MenuFrontColor = Color.green;
					break;
				case 4:
					MenuFrontColor = Color.blue;
					break;
				case 5:
					MenuFrontColor = Color.cyan;
					break;
				case 6:
					MenuFrontColor = Color.gray;
					break;
				case 7:
					MenuFrontColor = Color.grey;
					break;
				case 8:
					MenuFrontColor = Color.magenta;
					break;
				case 9:
					MenuFrontColor = Color.yellow;
					break;
			}
		}
		public override void runWin(int id)
		{
			GUI.backgroundColor = H_GUIColors.GUIBackgroundColor;
			GUI.contentColor = H_GUIColors.GUIFrontColor;
			GUILayout.Label("");
			toolbarIntModeSelected = GUI.Toolbar(new Rect(10, 25, 210, 23), toolbarIntModeSelected, toolbarStringModeSelected);
			GUI.Box(new Rect(10, 60, 210, 55), toolbarStringModeSelected[toolbarIntModeSelected].ToString());
			switch (toolbarIntModeSelected)
            {
				case 0:
					toolbarIntGUIMenu = GUI.Toolbar(new Rect(15, 85, 200, 23), toolbarIntGUIMenu, toolbarStringMenu);
					break;
				case 1:
					toolbarIntGUIContent = GUI.Toolbar(new Rect(15, 85, 200, 23), toolbarIntGUIContent, toolbarStringGUI);
					break;
            }

			GUI.Box(new Rect(10, 125, 210, 160), "Colors");
			GUIColorsScrollPosition = GUI.BeginScrollView(new Rect(10, 145, 205, 130), GUIColorsScrollPosition, new Rect(10, 130, 50, 320), false, true);
			switch (toolbarIntModeSelected)
            {
				case 0:
                    switch (toolbarIntGUIMenu)
                    {
						case 0:
							toolbarIntGUIBackgroundMenu = GUI.SelectionGrid(new Rect(15, 135, 180, 310), toolbarIntGUIBackgroundMenu, toolbarStringGUIColors, 1);
							break;
						case 1:
							toolbarIntGUIContentMenu = GUI.SelectionGrid(new Rect(15, 135, 180, 310), toolbarIntGUIContentMenu, toolbarStringGUIColors, 1);
							break;
					}
					break;
				case 1:
					switch (toolbarIntGUIContent)
					{
						case 0:
							toolbarIntGUIBackgroundColor = GUI.SelectionGrid(new Rect(15, 135, 180, 310), toolbarIntGUIBackgroundColor, toolbarStringGUIColors, 1);
							break;
						case 1:
							toolbarIntGUIContentColor = GUI.SelectionGrid(new Rect(15, 135, 180, 310), toolbarIntGUIContentColor, toolbarStringGUIColors, 1);
							break;
					}
					break;
            }
			
			GUI.EndScrollView();

			if (GUI.Button(new Rect(10, 295, 210, 30), "Reset default"))
			{
				toolbarIntGUIBackgroundColor = 0;
				toolbarIntGUIContentColor = 0;
				toolbarIntGUIBackgroundMenu = 0;
				toolbarIntGUIContentMenu = 0;
			}

			base.runWin(id);
		}

	}
}