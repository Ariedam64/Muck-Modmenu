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
        public bool storageItem = true;
        public bool foodItem = true;
        public bool axeItem = true;
        public bool bowItem = true;
        public bool pickaxeItem = true;
        public bool swordItem = true;
        public bool shieldItem = true;
        public bool shovelItem = true;
        public bool stationItem = true;
        public bool allItem = true;


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

            foreach (InventoryItem item in ItemManager.allScriptableItems)
            {
                if (axeItem){
                    if (item.type == InventoryItem.ItemType.Axe){
                        if (GUI.Button(new Rect(x, y, 50, 50), new GUIContent(item.sprite.texture, item.name + "\n $" + item.description))){
                            ClientSend.DropItem(item.id, ItemSpawnerAmount);
                        }
                        addCollumn();
                    }
                }
                if (bowItem){
                    if (item.type == InventoryItem.ItemType.Bow){
                        if (GUI.Button(new Rect(x, y, 50, 50), new GUIContent(item.sprite.texture, item.name + "\n $" + item.description))){
                            ClientSend.DropItem(item.id, ItemSpawnerAmount);
                        }
                        addCollumn();
                    }
                }
                if (itemItem)
                {
                    if (item.type == InventoryItem.ItemType.Item)
                    {
                        if (GUI.Button(new Rect(x, y, 50, 50), new GUIContent(item.sprite.texture, item.name + "\n $" + item.description)))
                        {
                            ClientSend.DropItem(item.id, ItemSpawnerAmount);
                        }
                        addCollumn();
                    }
                }
                if (shieldItem)
                {
                    if (item.type == InventoryItem.ItemType.Shield)
                    {
                        if (GUI.Button(new Rect(x, y, 50, 50), new GUIContent(item.sprite.texture, item.name + "\n $" + item.description)))
                        {
                            ClientSend.DropItem(item.id, ItemSpawnerAmount);
                        }
                        addCollumn();
                    }
                }
                if (storageItem)
                {
                    if (item.type == InventoryItem.ItemType.Storage)
                    {
                        if (GUI.Button(new Rect(x, y, 50, 50), new GUIContent(item.sprite.texture, item.name + "\n $" + item.description)))
                        {
                            ClientSend.DropItem(item.id, ItemSpawnerAmount);
                        }
                        addCollumn();
                    }
                }
                if (pickaxeItem)
                {
                    if (item.type == InventoryItem.ItemType.Pickaxe)
                    {
                        if (GUI.Button(new Rect(x, y, 50, 50), new GUIContent(item.sprite.texture, item.name + "\n $" + item.description)))
                        {
                            ClientSend.DropItem(item.id, ItemSpawnerAmount);
                        }
                        addCollumn();
                    }
                }
                if (stationItem)
                {
                    if (item.type == InventoryItem.ItemType.Station)
                    {
                        if (GUI.Button(new Rect(x, y, 50, 50), new GUIContent(item.sprite.texture, item.name + "\n $" + item.description)))
                        {
                            ClientSend.DropItem(item.id, ItemSpawnerAmount);
                        }
                        addCollumn();
                    }
                }
                if (shovelItem)
                {
                    if (item.type == InventoryItem.ItemType.Shovel)
                    {
                        if (GUI.Button(new Rect(x, y, 50, 50), new GUIContent(item.sprite.texture, item.name + "\n $" + item.description)))
                        {
                            ClientSend.DropItem(item.id, ItemSpawnerAmount);
                        }
                        addCollumn();
                    }
                }
                if (swordItem)
                {
                    if (item.type == InventoryItem.ItemType.Sword)
                    {
                        if (GUI.Button(new Rect(x, y, 50, 50), new GUIContent(item.sprite.texture, item.name + "\n $" + item.description)))
                        {
                            ClientSend.DropItem(item.id, ItemSpawnerAmount);
                        }
                        addCollumn();
                    }
                }
                if (foodItem)
                {
                    if (item.type == InventoryItem.ItemType.Food)
                    {
                        if (GUI.Button(new Rect(x, y, 50, 50), new GUIContent(item.sprite.texture, item.name + "\n $" + item.description)))
                        {
                            ClientSend.DropItem(item.id, ItemSpawnerAmount);
                        }
                        addCollumn();
                    }
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
            GUI.Box(new Rect(10, 167, 140, 200), "Filters");
            itemItem = GUI.Toggle(new Rect(20, 195, 80, 23), itemItem, "Item");
            axeItem = GUI.Toggle(new Rect(90, 195, 80, 23), axeItem, "Axe");

            pickaxeItem = GUI.Toggle(new Rect(20, 230, 80, 23), pickaxeItem, "Pickaxe");
            swordItem = GUI.Toggle(new Rect(90, 230, 80, 23), swordItem, "Sword");

            shieldItem = GUI.Toggle(new Rect(20, 265, 80, 23), shieldItem, "Shield");
            shovelItem = GUI.Toggle(new Rect(90, 265, 80, 23), shovelItem, "Shovel");

            stationItem = GUI.Toggle(new Rect(20, 300, 80, 23), stationItem, "Station");
            foodItem = GUI.Toggle(new Rect(90, 300, 80, 23), foodItem, "Food");

            storageItem = GUI.Toggle(new Rect(20, 335, 80, 23), storageItem, "Storage");
            bowItem = GUI.Toggle(new Rect(90, 335, 80, 23), bowItem, "Bow");



            //Function
            void addCollumn()
            {
                if (x == 510){
                    x = 150; y += 60;
                    GUILayout.Space(buttonWidth);
                }
                else{
                    x += 60;
                }
            }
            base.runWin(id);
        }

    }
}