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
		public H_Server() : base(new Rect(105, 370, 315, 300), "Server menu", 6, false) { }

		public static int SuPlayersButY, joueurSelectionne, playerMov;
		public static bool follow = false;
		public static bool spectate = false;
		public int yPlayer;
		public string[] selStrings;
		public static Vector2 playerListScrollPosition { get; set; } = Vector2.zero;
		public bool green = true;
		public static Vector3 PrevVelocity;
		public static PlayerManager[] array = new PlayerManager[0];
		public static MenuUI[] array3 = new MenuUI[0];
		public static PlayerManager[] arrayPlayer = new PlayerManager[0];
		public int toolbarInt = 0;

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
				typeof(MoveCamera).GetField("playerTarget", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(MoveCamera.Instance, LB_Menu.listeJoueur[joueurSelectionne].transform);
				typeof(MoveCamera).GetField("spectatingId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(MoveCamera.Instance, LB_Menu.listeJoueur[joueurSelectionne].id);
				MoveCamera.Instance.state = MoveCamera.CameraState.Spectate;
				PPController.Instance.Reset();
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
			GUI.backgroundColor = H_GUIColors.GUIBackgroundColor;
			GUI.contentColor = H_GUIColors.GUIFrontColor;
			GUILayout.Label("");
			GUI.Box(new Rect(10, 25, 120, 265), LB_Menu.listeJoueur.Length.ToString() + "/10 Players");
			playerListScrollPosition = GUI.BeginScrollView(new Rect(10, 60, 130, 220), playerListScrollPosition, new Rect(10, 60, 50, yPlayer), false, false); ;
			joueurSelectionne = GUI.SelectionGrid(new Rect(20, 60, 100, yPlayer), joueurSelectionne, selStrings, 1);
			GUI.EndScrollView();
			if (LB_Menu.listeJoueur[joueurSelectionne].transform.position == PlayerMovement.Instance.transform.position)
            {
				if (GUI.Button(new Rect(150, 25, 130, 30), "Kill[HOST]"))
				{
					ServerSend.HitPlayer(LocalClient.instance.myId, 69420, 0f, LB_Menu.listeJoueur[joueurSelectionne].id, 1, LB_Menu.listeJoueur[joueurSelectionne].transform.position);
				}
				if (GUI.Button(new Rect(150, 60, 130, 30), "Kick[HOST]"))
				{
					ServerSend.DisconnectPlayer(LB_Menu.listeJoueur[joueurSelectionne].id);
				}
				if (GUI.Button(new Rect(150, 95, 130, 30), "Instant revive"))
				{
					ClientSend.RevivePlayer(LB_Menu.listeJoueur[joueurSelectionne].id, LB_Menu.listeJoueur[joueurSelectionne].graveId, false);
				}
				if (GUI.Button(new Rect(150, 130, 130, 30), "Cage"))
				{
					spawnCage();
				}
				GUI.Label(new Rect(154, 165, 130, 23), "What I should add? Make suggestion on Discord!");
			}
			else
			{
				if (GUI.Button(new Rect(150, 25, 130, 30), "Kill[HOST]"))
				{
					ServerSend.HitPlayer(LocalClient.instance.myId, 69420, 0f, LB_Menu.listeJoueur[joueurSelectionne].id, 1, LB_Menu.listeJoueur[joueurSelectionne].transform.position);
				}
				if (GUI.Button(new Rect(150, 60, 130, 30), "Kick[HOST]"))
				{
					ServerSend.DisconnectPlayer(LB_Menu.listeJoueur[joueurSelectionne].id);
				}
				if (GUI.Button(new Rect(150, 95, 130, 30), "Tp to player"))
				{
					PlayerMovement.Instance.GetRb().position = LB_Menu.listeJoueur[joueurSelectionne].transform.position;
				}
				if (GUI.Button(new Rect(150, 130, 130, 30), "Tp player to boat"))
				{
					ServerSend.RevivePlayer(Server.clients[LB_Menu.listeJoueur[joueurSelectionne].id].id, LB_Menu.listeJoueur[joueurSelectionne].id, false, -1);
				}
				if (GUI.Button(new Rect(150, 165, 130, 30), "Instant revive"))
				{
					ClientSend.RevivePlayer(LB_Menu.listeJoueur[joueurSelectionne].id, LB_Menu.listeJoueur[joueurSelectionne].graveId, false);
				}
				if (GUI.Button(new Rect(150, 200, 130, 30), "Cage"))
				{
					spawnCage();
				}
				follow = GUI.Toggle(new Rect(150, 235, 130, 20), follow, "Follow player");
				spectate = GUI.Toggle(new Rect(150, 255, 130, 20), spectate, "Spectate player");
				GUI.Label(new Rect(154, 275, 130, 23), "What I should add? Make suggestion on Discord!");
			}
		
			base.runWin(id);
        }

		void spawnCage()
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
    }
}