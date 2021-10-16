using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace test.CT_Hacks
{
    public class H_Player : Menu
    {
        private bool fly = false;
        private bool invincible = false;
        private bool stamina = false;
        private bool hunger = false;

        public H_Player() : base(new Rect(10, 10, 200, 200), "Player Menu", 1, false) { }

        public void Update()
        {
            if (fly)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    typeof(PlayerStatus).GetField("CanJump", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(PlayerStatus.Instance, true);
                    MethodInfo privMethod = PlayerMovement.Instance.GetType().GetMethod("ResetJump", BindingFlags.NonPublic | BindingFlags.Instance);
                    privMethod.Invoke(PlayerMovement.Instance, new object[] {  });
                    
                }
            }

            if (invincible)
            {
                PlayerStatus.Instance.hp = PlayerStatus.Instance.maxHp;
            }

            if (stamina)
            {
                PlayerStatus.Instance.stamina = PlayerStatus.Instance.maxStamina;
            }

            if (hunger)
            {
                PlayerStatus.Instance.hunger = PlayerStatus.Instance.maxHunger;
            }



        }
        public override void runWin(int id)
        {
            invincible = GUILayout.Toggle(invincible, "Invincible");
            stamina = GUILayout.Toggle(stamina, "Stamina infini");
            hunger = GUILayout.Toggle(hunger, "Faim infini");
            fly = GUILayout.Toggle(fly, "Fly Hack");
            base.runWin(id);
        }

    }
}