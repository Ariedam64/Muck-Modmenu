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

        //========  VARIABLES
        public static Vector2 ScrollPosition { get; set; } = Vector2.zero;
        public static readonly Rect PowerUpSpawnerSliderPosition = new Rect(10, 40, 190, 20);
        public static readonly Rect PowerUItemSpawnerlabel = new Rect(10, 20, 100, 20);

        public static PowerupInventory PowerupInventory = PowerupInventory.Instance;
        public static ItemManager ItemManager = ItemManager.Instance;

        public int PowerUpSpawnerAmount = 1;
        public string prevTooltip = "";
        public string description, description2, prevDescription;

        public H_PowerUp() : base(new Rect(1460, 10, 210, 420), "PowerUp menu", 5, false) { }

        public void Update()
        {
            
        }
        public override void runWin(int id)
        {
            GUI.backgroundColor = H_GUIColors.GUIBackgroundColor;
            GUI.contentColor = H_GUIColors.GUIFrontColor;

            GUILayout.Label("");
            //Bouton de spawn
            int x = 25;
            int y = 155;

            ScrollPosition = GUI.BeginScrollView(new Rect(5,150,195,235), ScrollPosition, new Rect(20, 150, 175, 540), false, true);
               
            for (int i = 0; i < ItemManager.allPowerups.Count(); i++)
            {
                Powerup powerup = ItemManager.allPowerups[i];

                GUI.backgroundColor = H_GUIColors.GUIOriginalbackgroundColor;
                GUI.contentColor = H_GUIColors.GUIOriginalContentColor;
                if (GUI.Button(new Rect(x, y, 50, 50), new GUIContent(powerup.sprite.texture, powerup.name + "\n $" + powerup.description))){
                    for (int j = 0; j < PowerUpSpawnerAmount; j++){
                        UiEvents.Instance.AddPowerup(ItemManager.Instance.allPowerups[powerup.id]);
                        PlayerStatus.Instance.UpdateStats();
                        PowerupUI.Instance.AddPowerup(powerup.id);
                    }
                }
                GUI.backgroundColor = H_GUIColors.GUIBackgroundColor;
                GUI.contentColor = H_GUIColors.GUIFrontColor;
                if (x == 145)
                {
                    x = 25; y += 60;
                }
                else
                    x += 60;
            }

            GUI.EndScrollView();

            //Gestion du tooltip
            description = GUI.tooltip;
            description2 = description.Substring(description.IndexOf("$") + 1);

            if (GUI.tooltip.Length > 1)
            {
                prevTooltip = GUI.tooltip;
                prevDescription = description2;
            }

            //Affichage
            GUI.Label(PowerUItemSpawnerlabel, "Quantity : x" + PowerUpSpawnerAmount);
            PowerUpSpawnerAmount = (int)GUI.HorizontalSlider(PowerUpSpawnerSliderPosition, PowerUpSpawnerAmount, 1, 50);
            GUI.Box(new Rect(10, 55, 190, 23), prevTooltip);
            GUI.Box(new Rect(10, 85, 190, 60), "Description");
            GUI.Label(new Rect(15, 105, 180, 66), prevDescription);

            if (GUI.Button(new Rect(10, 390, 190, 20), "Exit"))
            {
                this.isOpen = false;
            }

            base.runWin(id);
        }

    }
}