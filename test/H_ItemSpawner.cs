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
        public bool itemItem = true;
        public bool storageItem = false;
        public bool foodItem = false;
        public bool axeItem = false;
        public bool bowItem = false;
        public bool pickaxeItem = false;
        public bool swordItem = false;
        public bool shieldItem = false;
        public bool shovelItem = false;
        public bool stationItem = false;
        public string[] toolbarStrings = { "Main", "Stats"};
        public int toolbarInt = 0;
        public bool allItem = false;
        public bool isDropped = true;
        public string prevTooltip = "";
        public string tooltipString, description, prevDescription;
        public string title, prevTitle;
        public string itemName;
        public int attackDamage = 0;
        public InventoryItem itemSelected;
        public int recupAttackDamage;
        public int prevAttackDamage = 0;
        public String attackDamageString, prevAttackDamageString;

        public H_ItemSpawner() : base(new Rect(430, 10, 600, 400), "Item spawners menu", 2, false) { }


        public void Update()
        {
            if (attackDamage != prevAttackDamage)
            {
                prevAttackDamage = attackDamage;
            }

        }
        public override void runWin(int id)
        {
            
            ScrollPosition = GUILayout.BeginScrollView(ScrollPosition, false, true, GUILayout.Width(580), GUILayout.Height(350));

            int x = 150;
            int y = 0;
            int buttonWidth = 60;

            foreach (InventoryItem item in ItemManager.allScriptableItems)
            {
                if (axeItem)
                {
                    showItem(item, InventoryItem.ItemType.Axe);
                }
                if (bowItem)
                {
                    showItem(item, InventoryItem.ItemType.Bow);
                }
                if (itemItem)
                {
                    showItem(item, InventoryItem.ItemType.Item);
                }
                if (shieldItem)
                {
                    showItem(item, InventoryItem.ItemType.Shield);
                }
                if (storageItem)
                {
                    showItem(item, InventoryItem.ItemType.Storage);
                }
                if (pickaxeItem)
                {
                    showItem(item, InventoryItem.ItemType.Pickaxe);
                }
                if (stationItem)
                {
                    showItem(item, InventoryItem.ItemType.Station);
                }
                if (shovelItem)
                {
                    showItem(item, InventoryItem.ItemType.Shovel);
                }
                if (swordItem)
                {
                    showItem(item, InventoryItem.ItemType.Sword);
                }
                if (foodItem)
                {
                    showItem(item, InventoryItem.ItemType.Food);
                }
            }

            GUILayout.Space(buttonWidth); //Créer une line de scroll en plus psk elle affiche pas la dernière ligne d'item
            GUILayout.EndScrollView();

            toolbarInt = GUI.Toolbar(new Rect(10, 25, 143, 23), toolbarInt, toolbarStrings);
            switch (toolbarInt)
            {
                case 0:
                    GUI.Box(new Rect(10, 60, 143, 175), "Filters");
                    itemItem = GUI.Toggle(new Rect(20, 85, 60, 23), itemItem, "Item");
                    axeItem = GUI.Toggle(new Rect(90, 85, 60, 23), axeItem, "Axe");
                    pickaxeItem = GUI.Toggle(new Rect(20, 115, 65, 23), pickaxeItem, "Pickaxe");
                    swordItem = GUI.Toggle(new Rect(90, 115, 60, 23), swordItem, "Sword");
                    shieldItem = GUI.Toggle(new Rect(20, 145, 60, 23), shieldItem, "Shield");
                    shovelItem = GUI.Toggle(new Rect(90, 145, 60, 23), shovelItem, "Shovel");
                    stationItem = GUI.Toggle(new Rect(20, 175, 60, 23), stationItem, "Station");
                    foodItem = GUI.Toggle(new Rect(90, 175, 60, 23), foodItem, "Food");
                    storageItem = GUI.Toggle(new Rect(20, 205, 60, 23), storageItem, "Storage");
                    bowItem = GUI.Toggle(new Rect(90, 205, 60, 23), bowItem, "Bow");
                    GUI.Box(new Rect(10, 240, 143, 50), "Drop it");
                    isDropped = GUI.Toggle(new Rect(20, 263, 70, 23), isDropped, "inventory");
                    isDropped = GUI.Toggle(new Rect(100, 263, 60, 23), !isDropped, "floor");

                    break;

                case 1:
                    tooltipString = GUI.tooltip;
                    title = Variables.deleteAllAfter(tooltipString, "à");
                    description = Variables.deleteAllBeetween("à", tooltipString, "é");
                    attackDamageString = Variables.deleteAllBeetween("é", tooltipString, "è");
                    recupAttackDamage = (int)Variables.stringToInt(prevAttackDamageString);

                    if (GUI.tooltip.Length > 1)
                    {
                        prevTitle = title;
                        prevDescription = description;
                        prevAttackDamageString = attackDamageString;
                        attackDamage = recupAttackDamage;
                    }

                    GUI.Label(new Rect(10, 55, 100, 20), "Quantity : x" + ItemSpawnerAmount.ToString());
                    ItemSpawnerAmount = (int)GUI.HorizontalSlider(new Rect(10, 75, 140, 20), ItemSpawnerAmount, 1.0f, 69.0f);
                    
                    GUI.Box(new Rect(10, 90, 140, 23), prevTitle);                
                    GUI.Box(new Rect(10, 120, 140, 75), "Description");
                    GUI.Label(new Rect(15, 140, 135, 75), prevDescription);
                    
                    GUI.Label(new Rect(10, 200, 135, 23), "Attack Damage: " + attackDamage);
                    attackDamage = (int)Math.Round(GUI.HorizontalSlider(new Rect(10, 220, 135, 23), attackDamage, 0, 1000), 1);
                 
                    break;
            }

            if(itemSelected == null)
            {
                itemName = "None selected";
            }
            else
            {
                itemName = itemSelected.name;
            }

            if (GUI.Button(new Rect(10, 330, 140, 40), "Spawn \n" + itemName))
            {
                
                if (isDropped)
                {
                    itemSelected.attackDamage = attackDamage;
                    ClientSend.DropItem(itemSelected.id, ItemSpawnerAmount);
                }
                else
                {
                    InventoryItem itemInventory = itemSelected;
                    itemInventory.amount = (int)ItemSpawnerAmount;
                    InventoryUI.Instance.AddItemToInventory(itemInventory);
                }
            }


            void showItem(InventoryItem item, InventoryItem.ItemType type)
            {
                if (item.type == type)
                {
                    
                    if (GUI.Button(new Rect(x, y, 50, 50), new GUIContent(item.sprite.texture, item.name + "à" + item.description +"é"+ item.attackDamage + "è")))
                    {
                        if (itemName == item.name)
                        {
                            itemSelected = null;
                            itemName = "None selected"; 
                        }
                        else 
                        {
                            itemSelected = item;
                            
                        }                             
                    }
                    if (x == 510)
                    {
                        x = 150; y += 60;
                        GUILayout.Space(buttonWidth);
                    }
                    else
                    {
                        x += 60;
                    }
                }
            }

            base.runWin(id);
        }

    }
}