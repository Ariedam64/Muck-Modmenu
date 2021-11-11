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

		public int toolbarIntGUIContent = 0;
		public int toolbarIntGUIBackgroundColor = 0;
		public int toolbarIntGUIContentColor = 0;
		public int toolbarIntGUISelect = 0;
		public int toolbarIntGUISelectColor = 0;
		public int toolbarIntGUIUnselectColor = 0;
		public static Color GUIBackgroundColor;
		public static Color GUIFrontColor;
		public static Color GUIOriginalbackgroundColor = GUI.backgroundColor;
		public static Color GUIOriginalContentColor = GUI.contentColor;
		public string[] toolbarStringGUI = { "Background", "Content" };
		public string[] toolbarStringGUIBackgroundColors = { "Original", "Black", "Red", "Green", "Blue", "Cyan", "Gray", "Grey", "Magenta", "Yellow" };
		public string[] toolbarStringGUIContentColors = { "Original", "Black", "Red", "Green", "Blue", "Cyan", "Gray", "Grey", "Magenta", "Yellow" };


		public H_GUIColors() : base(new Rect(1680, 10, 230, 400), "GUI Colors Menu", 8, false) { }

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
		}
		public override void runWin(int id)
		{
			GUI.backgroundColor = H_GUIColors.GUIBackgroundColor;
			GUI.contentColor = H_GUIColors.GUIFrontColor;
			GUILayout.Label("");
			GUI.Box(new Rect(10, 25, 210, 55), "GUI");
			toolbarIntGUIContent = GUI.Toolbar(new Rect(15, 50, 200, 23), toolbarIntGUIContent, toolbarStringGUI);
			GUI.Box(new Rect(10, 85, 210, 160), toolbarStringGUI[toolbarIntGUIContent].ToString() + " colors");
			GUIColorsScrollPosition = GUI.BeginScrollView(new Rect(10, 110, 205, 130), GUIColorsScrollPosition, new Rect(10, 95, 50, 320), false, true);
			switch (toolbarIntGUIContent)
            {
				case 0:
					toolbarIntGUIBackgroundColor = GUI.SelectionGrid(new Rect(15, 100, 180, 310), toolbarIntGUIBackgroundColor, toolbarStringGUIBackgroundColors, 1);               
					break;
				case 1:
					toolbarIntGUIContentColor = GUI.SelectionGrid(new Rect(15, 100, 180, 310), toolbarIntGUIContentColor, toolbarStringGUIContentColors, 1);
					
					break;
            }
			GUI.EndScrollView();
			base.runWin(id);
        }

	}
}