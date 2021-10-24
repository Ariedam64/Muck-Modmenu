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

namespace test.CT_Hacks
{
	public class H_Server : Menu
	{

		public H_Server() : base(new Rect(430, 420, 600, 400), "Other Player menu", 6, false) { }

		public static int SuPlayersButY, SuPlayerSelection, playerMov;
		public static bool follow = false;
		public static bool spectate = false;
		public static int playersInt = 0;
		public static Vector3 PrevVelocity;
		public static PlayerManager[] array = new PlayerManager[0];
		public static MenuUI[] array3 = new MenuUI[0];
		public static PlayerManager[] arrayPlayer = new PlayerManager[0];
		public int toolbarInt = 0;
		public string[] toolbarStrings = { "Actions", "Spawner", "Fun" };

		public void Update()
        {
			if (follow)
            {
				PlayerMovement.Instance.transform.position = LB_Menu.listeJoueur[SuPlayerSelection].transform.position;
			}
			if (spectate)
			{
				PPController.Instance.Reset();
				typeof(MoveCamera).GetField("playerTarget", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(MoveCamera.Instance, LB_Menu.listeJoueur[SuPlayerSelection].transform);
				typeof(MoveCamera).GetField("spectatingId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(MoveCamera.Instance, LB_Menu.listeJoueur[SuPlayerSelection].id);
				MoveCamera.Instance.state = MoveCamera.CameraState.Spectate;
			}
            else
            {
				PPController.Instance.Reset();
				typeof(MoveCamera).GetField("playerTarget", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(MoveCamera.Instance, null);
				typeof(MoveCamera).GetField("spectatingId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(MoveCamera.Instance, null);
				MoveCamera.Instance.PlayerRespawn(PlayerMovement.Instance.transform.position);
			}
		}
		public override void runWin(int id)
		{

			GUI.Box(new Rect(10, 25, 120, 365), LB_Menu.listeJoueur.Length.ToString() + "/10 Players");
			for (int i = 0; i < LB_Menu.listeJoueur.Length; i++)
			{
				SuPlayersButY = i * 25 + 60;
				if (GUI.Button(new Rect(20, (float)SuPlayersButY, 100, 20), LB_Menu.listeJoueur[i].username))
				{
					SuPlayerSelection = i;
				}
			}

			toolbarInt = GUI.Toolbar(new Rect(140, 25, 450, 23), toolbarInt, toolbarStrings);
			switch (toolbarInt)
            {
				case 0:
					if (GUI.Button(new Rect(150, 80, 130, 30), "Kill[HOST]"))
					{
						ServerSend.HitPlayer(LocalClient.instance.myId, 69420, 0f, LB_Menu.listeJoueur[SuPlayerSelection].id, 1, LB_Menu.listeJoueur[SuPlayerSelection].transform.position);
					}
					if (GUI.Button(new Rect(150, 120, 130, 30), "Kick[HOST]"))
					{
						ServerSend.DisconnectPlayer(LB_Menu.listeJoueur[SuPlayerSelection].id);
					}
					if (GUI.Button(new Rect(150, 160, 130, 30), "Tp to player"))
					{
						PlayerMovement.Instance.GetRb().position = LB_Menu.listeJoueur[SuPlayerSelection].transform.position;	
					}
					if (GUI.Button(new Rect(150, 200, 130, 30), "Tp player to boat"))
					{
						ServerSend.RevivePlayer(Server.clients[LB_Menu.listeJoueur[SuPlayerSelection].id].id, LB_Menu.listeJoueur[SuPlayerSelection].id, true, -1);
					}
					if (GUI.Button(new Rect(150, 240, 130, 30), "Instant revive"))
					{
						ClientSend.RevivePlayer(LB_Menu.listeJoueur[SuPlayerSelection].id, LB_Menu.listeJoueur[SuPlayerSelection].graveId, false);
					}
					follow = GUI.Toggle(new Rect(150, 280, 130, 20), follow, "Follow player");
					spectate = GUI.Toggle(new Rect(150, 300, 130, 20), spectate, "Spectate player");
					break;
				case 1:

					break;
				case 2:
					if (GUI.Button(new Rect(150, 80, 130, 30), "Cage"))
					{
						Vector3 position = LB_Menu.listeJoueur[SuPlayerSelection].transform.position;
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