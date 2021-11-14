using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Reflection;
using System;
using System.Runtime.InteropServices;
using test.CT_System;

namespace test.CT_Hacks
{
    public class H_Player : Menu
    {
        private bool invincible = false;
        private bool stamina = false;
        private bool hunger = false;
        private bool noclip = false;
        private bool clicktp = false;
        private bool fly = false;
        public int itemId;
        private bool instarevive = false;
        private bool instantKill = false;
        private bool freecam = false;
        private float speedhack = 1.0f;
        private float prevSpeedhack = 1.0f;
        private float gravity = 25.0f;
        private float prevGravity = 1.0f;
        private float jumpforce = 1.0f;
        private float prevJumpforce = 1.0f;


        public H_Player() : base(new Rect(220, 10, 200, 200), "Player Menu", 1, false) { }

        public void Update()

        {

            if (clicktp && Input.GetKeyDown(KeyCode.Mouse1))
            {
                UnityEngine.Object.FindObjectOfType<PlayerMovement>().GetRb().position = Variables.FindTpPos();
            }

            if (fly)
            {
               typeof(PlayerMovement).GetField("jumpCounterResetTime", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(PlayerMovement.Instance, 0);
               typeof(PlayerMovement).GetField("jumpCooldown", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(PlayerMovement.Instance, 0f);
               typeof(PlayerMovement).GetField("grounded", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(PlayerMovement.Instance, true);
                PlayerStatus.Instance.stamina = PlayerStatus.Instance.maxStamina;
                PlayerStatus.Instance.hunger = PlayerStatus.Instance.maxHunger;
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

            if (freecam)
            {
                MoveCamera.Instance.state = MoveCamera.CameraState.Freecam;
            }
            else
            {
                MoveCamera.Instance.state = MoveCamera.CameraState.Player;
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
                PlayerStatus.Instance.stamina = PlayerStatus.Instance.maxStamina;
                PlayerStatus.Instance.hunger = PlayerStatus.Instance.maxHunger;
            }

            if (instarevive && PlayerStatus.Instance.IsPlayerDead())
            {
                ClientSend.RevivePlayer(LocalClient.instance.myId, -1, false);
            }

            if (speedhack != prevSpeedhack)
            {
                float maxSpeed = 5.5f + speedhack;
                float maxRunSpeed = 12 + speedhack;
                float maxWalkSpeed = 5.5f + speedhack;
                typeof(PlayerMovement).GetField("maxSpeed", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(PlayerMovement.Instance, maxSpeed);
                typeof(PlayerMovement).GetField("maxWalkSpeed", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(PlayerMovement.Instance, maxWalkSpeed);
                typeof(PlayerMovement).GetField("maxRunSpeed", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(PlayerMovement.Instance, maxRunSpeed);
                prevSpeedhack = speedhack;
            }

            if (gravity != prevGravity)
            {              
                PlayerMovement.Instance.extraGravity = gravity;
                prevGravity = gravity;
            }

            if (jumpforce != prevJumpforce)
            {
                float force = 11 + jumpforce;
                typeof(PlayerMovement).GetField("jumpForce", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(PlayerMovement.Instance, force);
                prevJumpforce = jumpforce;
            }

            if (instantKill)
            {    
                 Hotbar.Instance.currentItem.attackDamage = 999999;
                 Hotbar.Instance.currentItem.resourceDamage = 999999;
            }
            else
            {
                itemId = Hotbar.Instance.currentItem.id;
                Hotbar.Instance.currentItem.attackDamage = ItemManager.Instance.allItems[itemId].attackDamage;
            }
        }
        public override void runWin(int id)
        {
            GUI.backgroundColor = H_GUIColors.GUIBackgroundColor;
            GUI.contentColor = H_GUIColors.GUIFrontColor;
            invincible = GUILayout.Toggle(invincible, "Godmode");
            stamina = GUILayout.Toggle(stamina, "Infinite stamina");
            hunger = GUILayout.Toggle(hunger, "Infinite hunger");
            instantKill = GUILayout.Toggle(instantKill, "Instant kill");
            clicktp = GUILayout.Toggle(clicktp, "Click Tp");
            noclip = GUILayout.Toggle(noclip, "No Clip");
            //freecam = GUILayout.Toggle(freecam, "Freecam (soon)");
            fly = GUILayout.Toggle(fly, "Fly");
            instarevive = GUILayout.Toggle(instarevive, "Instant revive");
            GUILayout.Label("Gravity: " + gravity);
            gravity = (int)Math.Round(GUILayout.HorizontalSlider(gravity, 0, 50), 1);
            GUILayout.Label("Speed hack: " + speedhack);
            speedhack = (int)Math.Round(GUILayout.HorizontalSlider(speedhack, 1, 100), 1);
            GUILayout.Label("Jump force: " + jumpforce);
            jumpforce = (int)Math.Round(GUILayout.HorizontalSlider(jumpforce, 1, 50), 1);
            base.runWin(id);
        }
    }
}