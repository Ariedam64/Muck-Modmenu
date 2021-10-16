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
    public class H_Player : Menu
    {
        private bool invincible = false;
        private bool stamina = false;
        private bool hunger = false;
        private bool noclip = false;
        private bool clicktp = false;
        private bool instantkill = false;


        public H_Player() : base(new Rect(220, 10, 200, 200), "Player Menu", 1, false) { }

        public void Update()

        {
            if (clicktp && Input.GetKeyDown(KeyCode.Mouse1))
            {
                UnityEngine.Object.FindObjectOfType<PlayerMovement>().GetRb().position = Variables.FindTpPos();
            }

            if (invincible)
            {
                PlayerStatus.Instance.hp = (float)PlayerStatus.Instance.maxHp;
                PlayerStatus.Instance.shield = (float)PlayerStatus.Instance.maxShield;
            }

            if (stamina)
            {
                PlayerStatus.Instance.stamina = PlayerStatus.Instance.maxStamina;
            }

            if (hunger)
            {
                PlayerStatus.Instance.hunger = PlayerStatus.Instance.maxHunger;
            }

            if (noclip)
            {
                UnityEngine.Object.FindObjectOfType<PlayerMovement>().GetPlayerCollider().enabled = false;
            }
            else
            {
                UnityEngine.Object.FindObjectOfType<PlayerMovement>().GetPlayerCollider().enabled = true;
            }

            
            if (instantkill)
            {
                PlayerStatus.Instance.CanJump();
                
            }


        }
        public override void runWin(int id)
        {
            invincible = GUILayout.Toggle(invincible, "Godmode");
            stamina = GUILayout.Toggle(stamina, "Ininite stamina");
            hunger = GUILayout.Toggle(hunger, "Infinir hunger");
            clicktp = GUILayout.Toggle(clicktp, "Click Tp");
            noclip = GUILayout.Toggle(noclip, "No Clip");
            base.runWin(id);
        }

    }
}