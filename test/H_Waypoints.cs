using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Reflection;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;

namespace test.CT_Hacks
{
    public class H_Waypoints : Menu
    {
        public static Vector2 waypointsListScrollPosition { get; set; } = Vector2.zero;
        public Map.MapMarker mapMarker;
        public static List<waypoint> waypointsList = new List<waypoint>();
        public string[] toolbarStringsWaypoints = { "Add", "Tp", "Del" };
        public int toolbarIntWaypoints = 0;
        public string waypointName = "";
        public int yScroll = 0;
        public bool isDisplayOnMap = true;
        public GUIStyle centeredStyle;

        public H_Waypoints() : base(new Rect(870, 420, 350, 210), "Waypoints menu", 9, false) { }

        public void Update()
        {
            centeredStyle.alignment = TextAnchor.UpperCenter;
            centeredStyle.normal.textColor = Color.white;
            centeredStyle.normal.background = Texture2D.grayTexture;
            
            if (isDisplayOnMap)
            {
                foreach (waypoint waypoint in waypointsList)
                {
                    addMyMarker(waypoint);
                }                 
            }
            else
            {
                {
                    foreach (waypoint waypoint in waypointsList)
                    {
                        removeMyMarker(waypoint);
                    }
                }
            }
        }
        public override void runWin(int id)
        {
            GUILayout.Label("");
            GUI.Box(new Rect(10, 25, 155, 55), "Mode");
            toolbarIntWaypoints = GUI.Toolbar(new Rect(15, 50, 145, 23), toolbarIntWaypoints, toolbarStringsWaypoints);
            GUI.Box(new Rect(170, 25, 150, 145), "Waypoints list");
            switch (toolbarIntWaypoints)
            {
                //New
                case 0:
                    GUI.Box(new Rect(10, 85, 155, 85), "Create a waypoint");
                    waypointName = GUI.TextField(new Rect(20,110,135,23),waypointName, 10);
                    if (GUI.Button(new Rect(40,140,100,23), "Create"))
                    {
                        if (waypointName != "" && (!isExistWaypointWithName(waypointName)))
                        {
                            createNewWaypoint();
                            waypointName = "";
                            yScroll += 20;
                            toolbarIntWaypoints = 1;
                        }         
                    }
                    showLabel();
                    break;

                //Tp
                case 1:
                    GUI.Box(new Rect(10, 85, 155, 55), "Settings");
                    isDisplayOnMap = GUI.Toggle(new Rect(20, 110, 135, 23), isDisplayOnMap, "Display on map");
                    showButtonTp();
                    break;

                //Del
                case 2:
                    GUI.Box(new Rect(10, 85, 155, 55), "Others");
                    if(GUI.Button(new Rect(20, 110, 135, 23), "Clear list")){
                        waypointsList.Clear();
                        yScroll = 0;
                    }
                    showButtonDelete();
                    break;
            }

            if (GUI.Button(new Rect(10, 180, 330, 20), "Exit"))
            {
                this.isOpen = false;
            }
            base.runWin(id);
        }

        void showLabel()
        {
            waypointsListScrollPosition = GUI.BeginScrollView(new Rect(175, 50, 165, 110), waypointsListScrollPosition, new Rect(175, 50, 130, yScroll), false, true);
            int y = 50;
            foreach (waypoint waypoint in waypointsList)
            {
                GUI.Label(new Rect(180, y, 130, 17), waypoint.name, centeredStyle);
                y += 20;
            }
            GUI.EndScrollView();
        }

        void showButtonTp()
        {
            waypointsListScrollPosition = GUI.BeginScrollView(new Rect(175, 50, 165, 110), waypointsListScrollPosition, new Rect(175, 50, 130, yScroll), false, true);
            int y = 50;
            foreach (waypoint waypoint in waypointsList)
            {
                if (GUI.Button(new Rect(180, y, 130, 20), waypoint.name))
                {
                    PlayerMovement.Instance.transform.position = waypoint.position;
                }
                y += 20;
            }
            GUI.EndScrollView();
        }

        void showButtonDelete()
        {
            waypointsListScrollPosition = GUI.BeginScrollView(new Rect(175, 50, 165, 110), waypointsListScrollPosition, new Rect(175, 50, 130, yScroll), false, true);
            int y = 50;
            foreach (waypoint waypoint in waypointsList)
            {
                if (GUI.Button(new Rect(180, y, 130, 20), waypoint.name))
                {
                    waypointsList.Remove(waypoint);
                    removeMyMarker(waypoint);
                    yScroll -= 20;
                }
                y += 20;
            }
            GUI.EndScrollView();
        }

        Map.MapMarker findMarker(waypoint waypoint)
        {
            return(Map.Instance.mapMarkers.Find(x => x.worldObject == waypoint.transform));
        }

        void addMyMarker(waypoint waypoint)
        {
            if (!(Map.Instance.mapMarkers.Exists(x => x.worldObject == waypoint.transform)))
            {
                var texture = Resources.Load<Texture2D>("flag");
                Map.Instance.AddMarker(waypoint.transform, Map.MarkerType.Ping, texture, Color.red, waypoint.name, 1);
            }
        }

        void removeMyMarker(waypoint waypoint)
        {
            Map.Instance.RemoveMarker(findMarker(waypoint));
        }

        void createNewWaypoint()
        {
            GameObject emptyGO = new GameObject();
            Transform newTransform = emptyGO.transform;
            newTransform.position = PlayerMovement.Instance.transform.position;
            waypointsList.Add(new waypoint() { id = Map.Instance.mapMarkers.Count + 1, name = waypointName, position = newTransform.position, transform = newTransform });
        }

        bool isExistWaypointWithName(string name)
        {
            return(waypointsList.Exists(x => x.name == name));
        }

        public class waypoint
        {
            public int id { get; set; }
            public string name { get; set; }         
            public Vector3 position { get; set; }
            public Transform transform { get; set; }
        }
    }
}