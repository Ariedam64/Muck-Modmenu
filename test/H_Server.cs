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

		public H_Server() : base(new Rect(390, 420, 600, 400), "Other Player menu", 6, false) { }

		public static int SuPlayersButY, joueurSelectionne, playerMov;
		public static bool follow = false;
		public static bool spectate = false;
		public static int playersInt = 0;
		public int yPlayer;
		public string[] selStrings;
		public bool green = true;
		public static Vector3 PrevVelocity;
		public static PlayerManager[] array = new PlayerManager[0];
		public static MenuUI[] array3 = new MenuUI[0];
		public static PlayerManager[] arrayPlayer = new PlayerManager[0];
		public int toolbarInt = 0;
		public string[] toolbarStrings = { "Actions", "Mob spawner (soon)", "Fun" };

		public void Update()
        {
			selStrings = LB_Menu.listeJoueur.Select(x => x.username).ToArray();
			yPlayer = (15 * selStrings.Length) + 10 * selStrings.Length - 1;

			if (follow)
            {
				var pos = LB_Menu.listeJoueur[joueurSelectionne].transform.position;
				pos.z -= 2;
				PlayerMovement.Instance.transform.position = pos;
			}
			if (spectate)
			{
				PPController.Instance.Reset();
				typeof(MoveCamera).GetField("playerTarget", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(MoveCamera.Instance, LB_Menu.listeJoueur[joueurSelectionne].transform);
				typeof(MoveCamera).GetField("spectatingId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(MoveCamera.Instance, LB_Menu.listeJoueur[joueurSelectionne].id);
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
			joueurSelectionne = GUI.SelectionGrid(new Rect(20, 60, 100, yPlayer), joueurSelectionne, selStrings, 1);

			toolbarInt = GUI.Toolbar(new Rect(140, 25, 450, 23), toolbarInt, toolbarStrings);
			switch (toolbarInt)
            {
				case 0:

					if (LB_Menu.listeJoueur[joueurSelectionne].transform.position == PlayerMovement.Instance.transform.position)
                    {
						if (GUI.Button(new Rect(150, 80, 130, 30), "Kill[HOST]"))
						{
							ServerSend.HitPlayer(LocalClient.instance.myId, 69420, 0f, LB_Menu.listeJoueur[joueurSelectionne].id, 1, LB_Menu.listeJoueur[joueurSelectionne].transform.position);
						}
						if (GUI.Button(new Rect(150, 120, 130, 30), "Kick[HOST]"))
						{
							ServerSend.DisconnectPlayer(LB_Menu.listeJoueur[joueurSelectionne].id);
						}						
						if (GUI.Button(new Rect(150, 160, 130, 30), "Instant revive"))
						{
							ClientSend.RevivePlayer(LB_Menu.listeJoueur[joueurSelectionne].id, LB_Menu.listeJoueur[joueurSelectionne].graveId, false);
						}

					}
                    else
					{
						if (GUI.Button(new Rect(150, 80, 130, 30), "Kill[HOST]"))
						{
							ServerSend.HitPlayer(LocalClient.instance.myId, 69420, 0f, LB_Menu.listeJoueur[joueurSelectionne].id, 1, LB_Menu.listeJoueur[joueurSelectionne].transform.position);
						}
						if (GUI.Button(new Rect(150, 120, 130, 30), "Kick[HOST]"))
						{
							ServerSend.DisconnectPlayer(LB_Menu.listeJoueur[joueurSelectionne].id);
						}
						if (GUI.Button(new Rect(150, 160, 130, 30), "Tp to player"))
						{
							PlayerMovement.Instance.GetRb().position = LB_Menu.listeJoueur[joueurSelectionne].transform.position;
						}
						if (GUI.Button(new Rect(150, 200, 130, 30), "Tp player to boat"))
						{
							ServerSend.RevivePlayer(Server.clients[LB_Menu.listeJoueur[joueurSelectionne].id].id, LB_Menu.listeJoueur[joueurSelectionne].id, false, -1);
						}
						if (GUI.Button(new Rect(150, 240, 130, 30), "Instant revive"))
						{
							ClientSend.RevivePlayer(LB_Menu.listeJoueur[joueurSelectionne].id, LB_Menu.listeJoueur[joueurSelectionne].graveId, false);
						}
						follow = GUI.Toggle(new Rect(150, 280, 130, 20), follow, "Follow player");
						spectate = GUI.Toggle(new Rect(150, 300, 130, 20), spectate, "Spectate player");
					}
					
					break;
				case 1:

					break;
				case 2:
					if (GUI.Button(new Rect(150, 80, 130, 30), "Cage"))
					{
						Vector3 position = LB_Menu.listeJoueur[joueurSelectionne].transform.position;
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
					GUI.Label(new Rect(150, 130, 130, 23), "What I should add?");
					break;
            }

			base.runWin(id);
        }
    }
}