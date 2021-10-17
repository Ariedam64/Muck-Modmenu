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
    public class H_Misc : Menu
    {

        public H_Misc() : base(new Rect(1040, 10, 200, 200), "Misc menu", 3, false) { }

        public void Update()

        {

        }
        public override void runWin(int id)
        {
            GUILayout.Label("_________BREAK_________");
            if (GUILayout.Button("All trees"))
            {
                HitableTree[] array = UnityEngine.Object.FindObjectsOfType<HitableTree>();
                for (int l = 0; l < array.Length; l++)
                {
                    array[l].Hit(9999, 9999f, 1, Vector3.zero, 1);
                }
            }
            if (GUILayout.Button("All rocks"))
            {
                HitableRock[] array2 = UnityEngine.Object.FindObjectsOfType<HitableRock>();
                for (int l = 0; l < array2.Length; l++)
                {
                    array2[l].Hit(9999, 9999f, 1, Vector3.zero, 1);
                }
            }
            if (GUILayout.Button("All resources"))
            {
                HitableResource[] array3 = UnityEngine.Object.FindObjectsOfType<HitableResource>();
                for (int l = 0; l < array3.Length; l++)
                {
                    array3[l].Hit(9999, 9999f, 1, Vector3.zero, 1);
                }
            }
            if (GUILayout.Button("User chests"))
            {
                HitableChest[] array4 = UnityEngine.Object.FindObjectsOfType<HitableChest>();
                for (int l = 0; l < array4.Length; l++)
                {
                    array4[l].Hit(9999, 9999f, 1, Vector3.zero, 1);
                }
            }
            GUILayout.Label("__________KILL__________");
            if (GUILayout.Button("All mobs"))
            {
                HitableMob[] array6 = UnityEngine.Object.FindObjectsOfType<HitableMob>();
                for (int l = 0; l < array6.Length; l++)
                {
                    array6[l].Hit(9999, 9999f, 1, Vector3.zero, 0);
                }
            }
            GUILayout.Label("__________USE__________");
            if (GUILayout.Button("All shrines"))
            {
                ShrineInteractable[] array7 = UnityEngine.Object.FindObjectsOfType<ShrineInteractable>();
                for (int l = 0; l < array7.Length; l++)
                {
                    array7[l].Interact();
                }
            }
            if (GUILayout.Button("All chests"))
            {
                LootContainerInteract[] array8 = UnityEngine.Object.FindObjectsOfType<LootContainerInteract>();
                for (int l = 0; l < array8.Length; l++)
                {
                    ClientSend.PickupInteract(array8[l].GetId());
                }
            }
            GUILayout.Label("_________OTHERS_________");
            if (GUILayout.Button("Break/kill everything"))
            {
                Hitable[] array5 = UnityEngine.Object.FindObjectsOfType<Hitable>();
                for (int l = 0; l < array5.Length; l++)
                {
                    array5[l].Hit(9999, 9999f, 1, Vector3.zero, 0);
                }
            }
            

            base.runWin(id);
        }

    }
}