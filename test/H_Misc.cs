using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace test.CT_Hacks
{
    public class H_Misc : Menu
    {
        private Player ply = null;
        private int coin = 0;
        private int prevCoin = 0;
        private bool fly = false;
        private bool success = false;

        public H_Misc() : base(new Rect(10, 10, 200, 200), "Misc Menu", 1, false) { }

        public void Update()
        {
            if (coin != prevCoin)
            {
                //PlayerStats.coinsValue = coin;
                //prevCoin = coin;
            }

            if (success)
            {
                
                PlayerStatus.Instance.hp = PlayerStatus.Instance.maxHp;
            }

            if (fly)
            {
                typeof(PlayerMovement).GetMethod("ResetJump", BindingFlags.Public | BindingFlags.Instance);
            }


        }
        public override void runWin(int id)
        {
            GUILayout.Label("Coin Multiplier: " + coin);
            coin = (int)Math.Round(GUILayout.HorizontalSlider(coin, 0, 9999999), 1);
            success = GUILayout.Toggle(success, "Invincible");
            fly = GUILayout.Toggle(fly, "fly");
            base.runWin(id);
        }

    }
}