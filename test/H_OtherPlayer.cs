using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Runtime.InteropServices;

namespace test.CT_Hacks
{
    public class H_OtherPlayer : Menu
    {

        public H_OtherPlayer() : base(new Rect(430, 420, 600, 400), "Other Player menu", 6, false) { }

		public static int SuPlayersButY, SuPlayerSelection;
		public static bool follow;
		public static PlayerManager[] array = UnityEngine.Object.FindObjectsOfType<PlayerManager>();


		public void Update()
        {
			if (follow)
			{
				PlayerMovement.Instance.transform.position = array[SuPlayerSelection].transform.position;
			}
		}
        public override void runWin(int id)
        {
			GUI.Box(new Rect(10, 50, 120, 400), "Players");

			PlayerManager[] array = UnityEngine.Object.FindObjectsOfType<PlayerManager>();
			for (int i = 0; i < array.Length; i++)
			{
				SuPlayersButY = i * 20 + 80;
				if (GUI.Button(new Rect(20f, (float)SuPlayersButY, 100f, 20f), array[i].username))
				{
					SuPlayerSelection = i;
				}
			}

			GUI.Label(new Rect(150, 60, 70, 20), "Selected:");
			if (array[SuPlayerSelection].username == UnityEngine.Object.FindObjectOfType<PlayerManager>().username)
			{
				GUI.Label(new Rect(210, 60, 50, 20), array[SuPlayerSelection].username);
			}
			if (GUI.Button(new Rect(150, 80, 90, 15), "Kill[HOST]"))
			{
				ServerSend.HitPlayer(LocalClient.instance.myId, 69420, 0f, array[SuPlayerSelection].id, 1, array[SuPlayerSelection].transform.position);
			}
			if (GUI.Button(new Rect(150, 100, 90, 15), "Kick[HOST]"))
			{
				ServerSend.DisconnectPlayer(array[SuPlayerSelection].id);
			}
			if (GUI.Button(new Rect(150, 120, 90, 15), "Cage"))
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
			if (GUI.Button(new Rect(150, 140, 90, 15), "Tp me-player"))
			{
				PlayerMovement.Instance.GetRb().position = array[SuPlayerSelection].transform.position;
			}
			GUI.Toggle(new Rect(150, 160, 90, 15), follow, "Follow");

			



			base.runWin(id);
        }

    }
}