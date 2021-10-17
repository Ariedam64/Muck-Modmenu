using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using UnityEngine;

namespace test.CT_Hacks
{
    public class H_PowerUp : Menu
    {
        public static Vector2 ScrollPosition { get; set; } = Vector2.zero;
        public static readonly Rect ItemSpawnerSliderPosition = new Rect(10, 40, 170, 20);
        public static readonly Rect ItemSpawnerlabel = new Rect(10, 20, 100, 20);
        public static int ItemSpawnerAmount { get; set; } = 1;

        public static PowerupInventory PowerupInventory = PowerupInventory.Instance;
        public static ItemManager ItemManager = ItemManager.Instance;


        public string prevTooltip = "";
        public string description, description2, prevDescription;

        public H_PowerUp() : base(new Rect(1460, 10, 400, 400), "PowerUp menu", 5, false) { }


        public void Update()
        {
            
        }
        public override void runWin(int id)
        {
            
            ScrollPosition = GUILayout.BeginScrollView(ScrollPosition, false, true, GUILayout.Width(430), GUILayout.Height(350));
            

            int x = 180;
            int y = 0;

            int buttonWidth = 60;

            for (int i = 0; i < ItemManager.allPowerups.Count(); i++)
            {
                Powerup powerup = ItemManager.allPowerups[i];

                if (GUI.Button(new Rect(x, y, 50, 50), new GUIContent(powerup.sprite.texture, powerup.name + "\n $" + powerup.description)))
                    for (int j = 0; j < ItemSpawnerAmount; j++)
                    {
                        ClientSend.DropItem(powerup.id, ItemSpawnerAmount);
                    }
                if (x == 360)
                {
                    x = 180; y += 60;
                    GUILayout.Space(buttonWidth);
                }
                else
                    x += 60;

        }


            GUILayout.Space(buttonWidth); //Créer une line de scroll en plus psk elle affiche pas la dernière ligne d'item
            GUILayout.EndScrollView();

            GUI.Label(ItemSpawnerlabel, "Quantity : x" + ItemSpawnerAmount.ToString());
            ItemSpawnerAmount = (int)GUI.HorizontalSlider(ItemSpawnerSliderPosition, ItemSpawnerAmount, 1.0f, 20.0f);

            description = GUI.tooltip;
            description2 = description.Substring(description.IndexOf("$") + 1);

            if (GUI.tooltip.Length > 1)
            {
                prevTooltip = GUI.tooltip;
                prevDescription = description2;
            }


            
            GUI.Box(new Rect(10, 55, 170, 23), prevTooltip);
            GUI.Box(new Rect(10, 85, 170, 60), "Description");
            GUI.Label(new Rect(15, 105, 165, 66), prevDescription);
           


            base.runWin(id);
        }

    }
}