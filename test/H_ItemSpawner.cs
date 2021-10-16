using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using UnityEngine;

namespace test.CT_Hacks
{
    public class H_ItemSpawner : Menu
    {
        public static Vector2 ScrollPosition { get; set; } = Vector2.zero;
        public static readonly Rect ItemSpawnerSliderPosition = new Rect(10, 40, 140, 20);
        public static readonly Rect ItemSpawnerlabel = new Rect(10, 20, 100, 20);

        public static readonly Rect ItemSpawnerScrollViewPosition = new Rect(100, 20, 500, 330);
        public static readonly Rect ItemSpawnerScrollViewBounds = new Rect(100, 20, 360, 500);
        public static int ItemSpawnerAmount { get; set; } = 1;
        public static ItemManager ItemManager = ItemManager.Instance;
        public static PowerupInventory PowerupInventory = PowerupInventory.Instance;


        public string prevTooltip = "";

        public H_ItemSpawner() : base(new Rect(430, 10, 600, 400), "Item spawners menu", 2, false) { }


        public void Update()
        {

        }
        public override void runWin(int id)
        {
            
            ScrollPosition = GUILayout.BeginScrollView(ScrollPosition, false, true, GUILayout.Width(580), GUILayout.Height(350));
            

            int x = 150;
            int y = 0;

            int buttonWidth = 60;

            foreach (InventoryItem item in ItemManager.allScriptableItems)
            {

                if (GUI.Button(new Rect(x, y, 50, 50), new GUIContent(item.sprite.texture, item.name)))
                   
                    
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

            GUI.Label(ItemSpawnerlabel, "Quantity : x" + ItemSpawnerAmount.ToString());
            ItemSpawnerAmount = (int)GUI.HorizontalSlider(ItemSpawnerSliderPosition, ItemSpawnerAmount, 1.0f, 69.0f);
            
            if (GUI.tooltip.Length > 1)
            {
                prevTooltip = GUI.tooltip;
            }

            GUI.Box(new Rect(10, 55, 140, 23), prevTooltip);


            base.runWin(id);
        }

    }
}