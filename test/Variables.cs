using System;
using System.Collections.Generic;
using System.Text;
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

	}
}