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
        private bool instarevive = false;
        private bool mobteleporthit = false;
        private bool instantKill = false;
        private float speedhack = 1.0f;
        private float prevSpeedhack = 1.0f;
        private float jumpforce = 1.0f;
        private float prevJumpforce = 1.0f;


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
                PlayerMovement.Instance.GetRb().velocity = new Vector3(0f, 0f, 0f);
                float num = Input.GetKey(KeyCode.LeftControl) ? 0.5f : (Input.GetKey(InputManager.sprint) ? 1f : 0.5f);
                if (Input.GetKey(InputManager.jump))
                {
                    PlayerStatus.Instance.transform.position = new Vector3(PlayerStatus.Instance.transform.position.x, PlayerStatus.Instance.transform.position.y + num, PlayerStatus.Instance.transform.position.z);
                }
                Vector3 position = PlayerStatus.Instance.transform.position;
                if (Input.GetKey(InputManager.forward))
                {
                    PlayerStatus.Instance.transform.position = new Vector3(position.x + Camera.main.transform.forward.x * Camera.main.transform.up.y * num, position.y + Camera.main.transform.forward.y * num, position.z + Camera.main.transform.forward.z * Camera.main.transform.up.y * num);
                }
                if (Input.GetKey(InputManager.backwards))
                {
                    PlayerStatus.Instance.transform.position = new Vector3(position.x - Camera.main.transform.forward.x * Camera.main.transform.up.y * num, position.y - Camera.main.transform.forward.y * num, position.z - Camera.main.transform.forward.z * Camera.main.transform.up.y * num);
                }
                if (Input.GetKey(InputManager.right))
                {
                    PlayerStatus.Instance.transform.position = new Vector3(position.x + Camera.main.transform.right.x * num, position.y, position.z + Camera.main.transform.right.z * num);
                }
                if (Input.GetKey(InputManager.left))
                {
                    PlayerStatus.Instance.transform.position = new Vector3(position.x - Camera.main.transform.right.x * num, position.y, position.z - Camera.main.transform.right.z * num);
                }

            }

            if (instarevive && PlayerStatus.Instance.IsPlayerDead())
            {
                ClientSend.RevivePlayer(LocalClient.instance.myId, -1, false);
            }

            if (speedhack != prevSpeedhack)
            {
                PlayerStatus.Instance.currentSpeedArmorMultiplier = speedhack;
                prevSpeedhack = speedhack;
            }

            if (jumpforce != prevJumpforce && Input.GetKeyDown(KeyCode.Space))
            {
                PlayerMovement.Instance.GetRb().AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
                prevJumpforce = jumpforce;
                Variables.sendMessage("Hack", "Cette option n'est pas encore disponible");
            }

            if (mobteleporthit && Input.GetKey(KeyCode.Mouse0))
            {
                foreach (Mob mob in UnityEngine.Object.FindObjectsOfType(typeof(Mob)) as Mob[])
                {
                    PlayerMovement.Instance.transform.position = mob.transform.position + new Vector3(0f, 0f, 3f);
                }
            }

            if (instantKill)
            {
                 Hotbar.Instance.currentItem.attackDamage = 9999;
                 Hotbar.Instance.currentItem.resourceDamage = 9999;
                 Hotbar.Instance.currentItem.attackSpeed = 100f;
            }



        }
        public override void runWin(int id)
        {
            invincible = GUILayout.Toggle(invincible, "Godmode");
            stamina = GUILayout.Toggle(stamina, "Ininite stamina");
            hunger = GUILayout.Toggle(hunger, "Infinir hunger");
            instantKill = GUILayout.Toggle(instantKill, "Boost weapon");
            clicktp = GUILayout.Toggle(clicktp, "Click Tp");
            noclip = GUILayout.Toggle(noclip, "No Clip");
            instarevive = GUILayout.Toggle(instarevive, "Instant revive");
            GUILayout.Label("Speed hack: " + speedhack);
            speedhack = (float)Math.Round(GUILayout.HorizontalSlider(speedhack, 1f, 100f), 1);
            GUILayout.Label("Jump force: " + jumpforce);
            jumpforce = (float)Math.Round(GUILayout.HorizontalSlider(jumpforce, 1f, 20f), 1);
            mobteleporthit = GUILayout.Toggle(mobteleporthit, "Teleport to mob");
            base.runWin(id);
        }

    }
}