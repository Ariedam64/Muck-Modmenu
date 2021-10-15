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


        public H_Misc() : base(new Rect(10, 10, 200, 200), "Misc Menu", 1, false) { }

        public void Update()
        {
            if (coin != prevCoin)
            {

            }

        }
        public override void runWin(int id)
        {
            GUILayout.Label("Coin Multiplier: " + coin);
            coin = (int)Math.Round(GUILayout.HorizontalSlider(coin, 0, 10000), 1);
            base.runWin(id);
        }

    }
}