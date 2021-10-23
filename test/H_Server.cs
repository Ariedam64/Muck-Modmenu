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

namespace test.CT_Hacks
{
	public class H_Server : Menu
	{

		public H_Server() : base(new Rect(430, 420, 600, 400), "Other Player menu", 6, false) { }

		public static int SuPlayersButY, SuPlayerSelection, playerMov;
		public static bool follow = false;
		public static bool spectate = false;
		public static int playersInt = 0;
		public static PlayerManager[] array = new PlayerManager[0];
		public int toolbarInt = 0;
		public string[] toolbarStrings = { "Actions", "Spawner", "Fun" };

		public void Update()
        {
			if (follow)
            {
				PlayerMovement.Instance.transform.position = array[SuPlayerSelection].transform.position;
			}
			if (spectate)
			{
				PlayerMovement.Instance.GetRb().velocity = new Vector3(0f, 0f, 0f);
				typeof(MoveCamera).GetField("playerTarget", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(MoveCamera.Instance, array[SuPlayerSelection].transform);
				typeof(MoveCamera).GetField("spectatingId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(MoveCamera.Instance, array[SuPlayerSelection].id);
				MoveCamera.Instance.state = MoveCamera.CameraState.Spectate;
			}
            else
            {
				typeof(MoveCamera).GetField("playerTarget", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(MoveCamera.Instance, null);
				typeof(MoveCamera).GetField("spectatingId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(MoveCamera.Instance, null);
				MoveCamera.Instance.state = MoveCamera.CameraState.Player;
			}
		}
		public override void runWin(int id)
		{

			GUI.Box(new Rect(10, 25, 120, 365), array.Length.ToString() + " Players");
			toolbarInt = GUI.Toolbar(new Rect(140, 25, 450, 23), toolbarInt, toolbarStrings);

			if (array.Length == 0)
            {
				array = UnityEngine.Object.FindObjectsOfType<PlayerManager>();
			}
			
            for (int i = 0; i < array.Length; i++)
			{
				SuPlayersButY = i * 25 + 60;
				if (GUI.Button(new Rect(20, (float)SuPlayersButY, 100, 20), array[i].username))
				{
					SuPlayerSelection = i;
				}
			}
			
			switch (toolbarInt)
            {
				case 0:
					if (GUI.Button(new Rect(150, 80, 130, 30), "Kill[HOST]"))
					{
						ServerSend.HitPlayer(LocalClient.instance.myId, 69420, 0f, array[SuPlayerSelection].id, 1, array[SuPlayerSelection].transform.position);
					}
					if (GUI.Button(new Rect(150, 120, 130, 30), "Kick[HOST]"))
					{
						ServerSend.DisconnectPlayer(array[SuPlayerSelection].id);
					}
					if (GUI.Button(new Rect(150, 160, 130, 30), "Tp me-player"))
					{
						PlayerMovement.Instance.GetRb().position = array[SuPlayerSelection].transform.position;	
					}
					if (GUI.Button(new Rect(150, 200, 130, 30), "Instant revive"))
					{
						ClientSend.RevivePlayer(array[SuPlayerSelection].id, array[SuPlayerSelection].graveId, false);
					}
					follow = GUI.Toggle(new Rect(150, 240, 130, 20), follow, "Follow player");
					spectate = GUI.Toggle(new Rect(150, 270, 130, 20), spectate, "Spectate player");
					break;
				case 1:
					break;
				case 2:
					if (GUI.Button(new Rect(150, 80, 130, 30), "Cage"))
					{
						Vector3 position = array[SuPlayerSelection].transform.position;
						position.y += 5f;
						Vector3 vector = position;
						Vector3 beuh = position;
						beuh.y -= 7f;
						vector.x -= 3.5f;
						vector.y -= 3.5f;
						Vector3 pos = vector;
						pos.x += 7f;
						Vector3 vector2 = position;
						vector2.y -= 3.5f;
						vector2.z -= 3.5f;
						Vector3 pos2 = vector2;
						pos2.z += 7f;
						ClientSend.RequestBuild(35, position, 0);
						ClientSend.RequestBuild(35, beuh, 0);
						ClientSend.RequestBuild(41, vector, 90);
						ClientSend.RequestBuild(41, pos, 90);
						ClientSend.RequestBuild(41, vector2, 180);
						ClientSend.RequestBuild(41, pos2, 180);
					}
					break;
            }
			
			base.runWin(id);
        }

    }
}