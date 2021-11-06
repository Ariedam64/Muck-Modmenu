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
        public Map.MapMarker mapMarker;
        public Vector3[] tabPosition = new Vector3[10];
        public String[] tabString = new String[10];
        public Transform[] tabTransform = new Transform[10];
        public Color[] tabColor = new Color[10];
        public int compteur = 0;
        public string waypointName = "";
        public TextAsset imageAsset;
        public Texture2D flag;
        

        public H_Waypoints() : base(new Rect(1460, 420, 230, 200), "Waypoints menu", 9, false) { }

        public void Update()
        {
          
        }
        public override void runWin(int id)
        {
            GUILayout.Label("Waypoint name:");
            waypointName = GUILayout.TextField(waypointName, 25);
            if (GUILayout.Button("Set a new waypoint"))
            {
                if (waypointName != "")
                {
                    tabString[compteur] = waypointName;
                    tabPosition[compteur] = PlayerMovement.Instance.transform.position;
                    GameObject emptyGO = new GameObject();
                    Transform newTransform = emptyGO.transform;
                    newTransform.position = PlayerMovement.Instance.transform.position;
                    tabTransform[compteur] = newTransform;
                    var texture = Resources.Load<Texture2D>("flag");

                    Map.Instance.AddMarker(tabTransform[compteur], Map.MarkerType.Ping, texture, Color.red, tabString[compteur], 1);
                    waypointName = "";
                    compteur++;
                }    
            } 
            GUILayout.Label("-----------" + compteur + "/10 waypoints sets----------");

            for (int i = 0; i < compteur; i++){
                if (GUILayout.Button(tabString[i])){
                   PlayerMovement.Instance.transform.position = tabPosition[i];
                }
            }

            GUILayout.Label("---------------------------------------------------");
            base.runWin(id);
        }
    }
}