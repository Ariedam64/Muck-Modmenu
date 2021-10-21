using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Reflection;
using System.Linq;
using System.Runtime.InteropServices;

namespace test.CT_Hacks
{
    public class H_OtherPlayer : Menu
    {

        public H_OtherPlayer() : base(new Rect(430, 420, 600, 400), "Other Player menu", 6, false) { }

		public static int SuPlayersButY, SuPlayerSelection, playerMov;
		public static bool follow = false;
		public static bool spectate = false;
		public static PlayerManager[] array;
		public static LocalClient[] arrayTest;
		public static ItemManager[] array2;
		public static Client client;
		public static OnlinePlayer[] oPlayer;
		public static PowerupInventory PowerupInventory = PowerupInventory.Instance;
		public static ItemManager ItemManager = ItemManager.Instance;
		public static Player ply;


		public void Update()
        {
			if (follow)
            {
				PlayerMovement.Instance.transform.position = array[SuPlayerSelection].transform.position;
			}
			if (spectate)
			{
				Variables.sendMessage("Hack", "Cette option n'est pas encore disponible");
			}
		}
        public override void runWin(int id)
        {

			array = UnityEngine.Object.FindObjectsOfType<PlayerManager>();
			array2 = UnityEngine.Object.FindObjectsOfType <ItemManager>();
			arrayTest = UnityEngine.Object.FindObjectsOfType<LocalClient>();
			oPlayer = UnityEngine.Object.FindObjectsOfType(typeof(OnlinePlayer)) as OnlinePlayer[];

			GUI.Box(new Rect(10, 50, 120, 400), "Players");
			
			for (int i = 0; i < array.Length; i++)
			{
				SuPlayersButY = i * 25 + 80;
				if (GUI.Button(new Rect(20, (float)SuPlayersButY, 100, 20), array[i].username))
				{
					SuPlayerSelection = i;
				}
			} 

			GUI.Label(new Rect(150, 50, 70, 20), "Selected:");
			GUI.Label(new Rect(210, 50, 50, 20), array[SuPlayerSelection].username);

			if (GUI.Button(new Rect(150, 80, 90, 20), "Kill[HOST]"))
			{
				ServerSend.HitPlayer(LocalClient.instance.myId, 69420, 0f, array[SuPlayerSelection].id, 1, array[SuPlayerSelection].transform.position);
			}
			if (GUI.Button(new Rect(150, 110, 90, 20), "Kick[HOST]"))
			{
				ServerSend.DisconnectPlayer(array[SuPlayerSelection].id);
			}
			if (GUI.Button(new Rect(150, 140, 90, 20), "Cage"))
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

			if (GUI.Button(new Rect(150, 170, 90, 20), "Tp me-player"))
			{
				PlayerMovement.Instance.GetRb().position = array[SuPlayerSelection].transform.position;
			}

			follow = GUI.Toggle(new Rect(150, 200, 90, 20), follow, "Follow player");

			follow = GUI.Toggle(new Rect(150, 230, 90, 20), spectate, "Spectate player");

			if (GUI.Button(new Rect(150, 260, 90, 20), "Tp player-me"))
			{


					//MobManager.Instance.mobs[i].SetDestination(PlayerMovement.Instance.GetRb().position);
					MobManager.Instance.mobs[MobManager.Instance.mobs.Count() - 1].transform.position = PlayerMovement.Instance.GetRb().position;
				

			}
			base.runWin(id);
        }

    }
}