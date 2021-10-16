using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using UnityEngine;

namespace test.CT_Hacks
{
    public class H_Misc : Menu
    {
        private int coin, prevCoin = 0;
        public static Vector2 ScrollPosition { get; set; } = Vector2.zero;
        public static readonly Rect ItemSpawnerSliderPosition = new Rect(20, 330, 100, 20);
        public static readonly Rect ItemSpawnerCountPosition = new Rect(135, 330, 50, 20);
        public static readonly Rect ItemSpawnerScrollViewPosition = new Rect(100, 20, 500, 330);
        public static readonly Rect ItemSpawnerScrollViewBounds = new Rect(100, 20, 360, 500);
        public static int ItemSpawnerAmount { get; set; } = 1;
        public static ItemManager ItemManager = ItemManager.Instance;
        public static PowerupInventory PowerupInventory = PowerupInventory.Instance;

        public H_Misc() : base(new Rect(500, 10, 600, 400), "Item spawners menu", 1, false) { }

        public void Update()
        {

            
        }
        public override void runWin(int id)
        {
            
            ScrollPosition = GUILayout.BeginScrollView(ScrollPosition, false, true, GUILayout.Width(580), GUILayout.Height(350));

            int x = 150;
            int y = 0;

            int buttonWidth = 60;

            for (int i = 0; i < ItemManager.allScriptableItems.Count(); i++)
            {
                InventoryItem item = ItemManager.allScriptableItems[i];

                if (GUI.Button(new Rect(x, y, 50, 50), new GUIContent(item.sprite.texture, "Spawn " + item.name)))
                    ClientSend.DropItem(item.id, ItemSpawnerAmount);

                if (x == 510)  
                {
                    x = 150; y += 60;
                    GUILayout.Space(buttonWidth);
                }
                else
                    x += 60;
            }
            GUILayout.Space(buttonWidth); //Créer une line de scroll en plus psk elle affiche pas la dernière ligne d'item
            GUILayout.EndScrollView();

            ItemSpawnerAmount = (int)GUI.HorizontalSlider(ItemSpawnerSliderPosition, ItemSpawnerAmount, 1.0f, 69.0f);
            GUI.Label(ItemSpawnerCountPosition, ItemSpawnerAmount.ToString() + "x");

            base.runWin(id);
        }

    }
}