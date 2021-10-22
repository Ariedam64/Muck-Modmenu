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
        public bool allItem = false;
        public bool isDropped = true;


        public string prevTooltip = "";
        public string description, prevDescription, description2;

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

            foreach (InventoryItem item in ItemManager.allScriptableItems){
                if (axeItem){
                    showItem(item, InventoryItem.ItemType.Axe);
                }
                if (bowItem){
                    showItem(item, InventoryItem.ItemType.Bow);             
                }
                if (itemItem){
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

            description = GUI.tooltip;
            description2 = description.Substring(description.IndexOf("$") + 1);

            if (GUI.tooltip.Length > 1)
            {
                prevTooltip = GUI.tooltip;
                prevDescription = description2;
            }


            //Quantity
            GUI.Label(ItemSpawnerlabel, "Quantity : x" + ItemSpawnerAmount.ToString());
            ItemSpawnerAmount = (int)GUI.HorizontalSlider(ItemSpawnerSliderPosition, ItemSpawnerAmount, 1.0f, 69.0f);
            //Name
            GUI.Box(new Rect(10, 55, 140, 23), prevTooltip);
            //Description
            GUI.Box(new Rect(10, 85, 140, 75), "Description");
            GUI.Label(new Rect(15, 105, 135, 75), prevDescription);
            //Filters
            GUI.Box(new Rect(10, 167, 140, 180), "Filters");
            itemItem = GUI.Toggle(new Rect(20, 195, 60, 23), itemItem, "Item");
            axeItem = GUI.Toggle(new Rect(90, 195, 60, 23), axeItem, "Axe");
            pickaxeItem = GUI.Toggle(new Rect(20, 225, 65, 23), pickaxeItem, "Pickaxe");
            swordItem = GUI.Toggle(new Rect(90, 225, 60, 23), swordItem, "Sword");
            shieldItem = GUI.Toggle(new Rect(20, 255, 60, 23), shieldItem, "Shield");
            shovelItem = GUI.Toggle(new Rect(90, 255, 60, 23), shovelItem, "Shovel");
            stationItem = GUI.Toggle(new Rect(20, 285, 60, 23), stationItem, "Station");
            foodItem = GUI.Toggle(new Rect(90, 285, 60, 23), foodItem, "Food");
            storageItem = GUI.Toggle(new Rect(20, 315, 60, 23), storageItem, "Storage");
            bowItem = GUI.Toggle(new Rect(90, 315, 60, 23), bowItem, "Bow");

            isDropped = GUI.Toggle(new Rect(15, 350, 60, 23), isDropped, "Drop it");


            //Function

            void showItem(InventoryItem item, InventoryItem.ItemType type)
            {
                if (item.type == type)
                {
                    if (GUI.Button(new Rect(x, y, 50, 50), new GUIContent(item.sprite.texture, item.name + "\n $" + item.description)))
                    {
                        if (isDropped)
                            ClientSend.DropItem(item.id, ItemSpawnerAmount);
                        else
                        {
                            InventoryItem itemInventory = item;
                            itemInventory.amount = (int)ItemSpawnerAmount;
                            InventoryUI.Instance.AddItemToInventory(itemInventory);
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