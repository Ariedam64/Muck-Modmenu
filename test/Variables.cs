using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using UnityEngine;

namespace test
{
    public class Variables
    {

		public static Vector3 FindTpPos()
		{
			Transform playerCam = PlayerMovement.Instance.playerCam;
			RaycastHit raycastHit;
			if (Physics.Raycast(playerCam.position, playerCam.forward, out raycastHit, 1500f))
			{
				Vector3 b = Vector3.zero;
				if (raycastHit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
				{
					b = Vector3.one;
				}
				return raycastHit.point + b;
			}
			return Vector3.zero;
		}

		public static void sendMessage(string username, string message)
        {
			ChatBox tchat = UnityEngine.Object.FindObjectOfType<ChatBox>();
			tchat.AppendMessage(0, message, username);
			ClientSend.SendChatMessage(message);
		}

		internal static object GetInstanceField(Type type, object instance, string fieldName)
		{
			BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
			FieldInfo field = type.GetField(fieldName, bindFlags);
			return field.GetValue(instance);
		}

		internal static void instanceField(Type type, object instance, string fieldName, object value)
		{
			BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
			FieldInfo field = type.GetField(fieldName, bindFlags);
			field.SetValue(instance, value);
		}

		public static int getMobIntancied()
        {
			return (int)GetInstanceField(typeof(MobManager), MobManager.Instance, "mobId");
		}

	}
}