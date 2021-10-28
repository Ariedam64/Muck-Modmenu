using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using UnityEngine;
using System.IO;
using System.Windows.Forms;

namespace test.CT_Hacks
{
    public class H_ItemSpawner : Menu
    {



        //Menu
        public static Vector2 ScrollPosition { get; set; } = Vector2.zero;
        public static Vector2 ScrollPositionStats { get; set; } = Vector2.zero;

        public readonly Color prevColorGUI = GUI.backgroundColor;
        public string[] toolbarStrings = { "Main", "Stats" };
        public int toolbarInt = 0;
        public bool allItem = false;
        public bool isDropped = false;
        public static int ItemSpawnerAmount { get; set; } = 1;
        public static ItemManager ItemManager = ItemManager.Instance;

        //Filtres
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
        public bool prevItemItem = false;
        public bool prevStorageItem = false;
        public bool prevFoodItem = false;
        public bool prevAxeItem = false;
        public bool prevBowItem = false;
        public bool prevPickaxeItem = false;
        public bool prevSwordItem = false;
        public bool prevShieldItem = false;
        public bool prevShovelItem = false;
        public bool prevStationItem = false;


        //ToolTip
        public string prevTooltip = "";
        public string tooltipString;
        public string description;
        public string prevDescription;
        public string titleItem;
        public string prevTitle;
        public string itemName;


        //Items
        public InventoryItem itemSelected;

        public int prevItemMax;
        public bool prevItemStackable;

        public int attackMultiplier = 1;
        public int prevAttackMultiplier = 1;

        public String attackDamageString;
        public String attackRangeString;
        public String attackSpeedString;
        public String itemHealString;
        public String itemArmorString;
        public String itemStaminaString;
        public String itemHungerString;
        public String itemRessourceDamageString;
        public String itemSharpnessString;
        public String itemProcessTimeString;
        public String itemBuildableString;

        public int attackDamage;
        public float attackSpeed;
        public float attackRange;
        public float itemHeal;
        public int itemArmor;
        public float itemStamina;
        public float itemHunger;
        public int itemDamageRessource;
        public float itemSharpness;
        public float itemProcessTime;
        public bool itemBuildable;

        public int prevAttackDamage;
        public float prevAttackRange;
        public float prevAttackSpeed;
        public float prevItemHeal;
        public int prevItemArmor;
        public float prevItemStamina;
        public float prevItemHunger;
        public int prevItemDamageRessource;
        public float prevItemSharpness;
        public float prevItemProcessTime;
        public bool prevItemBuildable;


        public H_ItemSpawner() : base(new Rect(430, 10, 600, 400), "Item spawners menu", 2, false) { }


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

            tooltipString = GUI.tooltip;
            titleItem = Variables.deleteAllAfter(tooltipString, "à");
            description = Variables.deleteAllBeetween("à", tooltipString, "é");
            attackDamageString = Variables.deleteAllBeetween("é", tooltipString, "è");
            attackDamage = Variables.stringToInt(attackDamageString);
            attackRangeString = Variables.deleteAllBeetween("è", tooltipString, "ç");
            attackRange = Variables.stringToFloat(attackRangeString); 
            attackSpeedString = Variables.deleteAllBeetween("ç", tooltipString, "(");
            attackSpeed = Variables.stringToFloat(attackSpeedString);
            itemHealString = Variables.deleteAllBeetween("(", tooltipString, ")");
            itemHeal = Variables.stringToFloat(itemHealString);
            itemArmorString = Variables.deleteAllBeetween(")", tooltipString, "=");
            itemArmor = Variables.stringToInt(itemArmorString);
            itemStaminaString = Variables.deleteAllBeetween("=", tooltipString, "+");
            itemStamina = Variables.stringToFloat(itemStaminaString);
            itemHungerString = Variables.deleteAllBeetween("+", tooltipString, "-");
            itemHunger = Variables.stringToFloat(itemHungerString);
            itemRessourceDamageString = Variables.deleteAllBeetween("-", tooltipString, "*");
            itemDamageRessource = Variables.stringToInt(itemRessourceDamageString);
            itemSharpnessString = Variables.deleteAllBeetween("*", tooltipString, "/");
            itemSharpness = Variables.stringToFloat(itemSharpnessString);
            itemProcessTimeString = Variables.deleteAllBeetween("/", tooltipString, "ù");
            itemProcessTime = Variables.stringToFloat(itemProcessTimeString);
            itemBuildableString = Variables.deleteAllBeetween("ù", tooltipString, "$");
            itemBuildable = Variables.stringToBool(itemBuildableString);

            if (GUI.tooltip.Length > 1)
            {
                prevTitle = titleItem;
                prevDescription = description;
                prevAttackDamage = attackDamage;
                prevAttackRange = attackRange;
                prevAttackSpeed = attackSpeed;
                prevItemHeal = itemHeal;
                prevItemArmor = itemArmor;
                prevItemStamina = itemStamina;
                prevItemHunger = itemHunger;
                prevItemDamageRessource = itemDamageRessource;
                prevItemSharpness = itemSharpness;
                prevItemProcessTime = itemProcessTime;
                prevItemBuildable = itemBuildable;
            }

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
                    isDropped = GUI.Toggle(new Rect(20, 263, 60, 23), isDropped, "floor");
                    isDropped = GUI.Toggle(new Rect(70, 263, 70, 23), !isDropped, "inventory");
                    GUI.Label(new Rect(10, 296, 100, 20), "Quantity : x" + ItemSpawnerAmount.ToString());
                    ItemSpawnerAmount = (int)GUI.HorizontalSlider(new Rect(10, 316, 140, 20), ItemSpawnerAmount, 1.0f, 100.0f);

                    break;

                case 1:

                    GUI.Box(new Rect(10, 60, 140, 23), prevTitle);                
                    GUI.Box(new Rect(10, 90, 140, 75), "Description");
                    GUI.Label(new Rect(15, 110, 135, 75), prevDescription);

                   

                    if (itemSelected==null)
                    {
                        //Item non selectionné
                        if (prevItemHeal == 0 && prevItemArmor == 0 && prevItemHunger == 0 && prevItemStamina == 0 & prevItemBuildable == false && prevAttackSpeed != 0.7f)
                        {
                            GUI.Label(new Rect(10, 170, 135, 23), "Attack damage: " + prevAttackDamage);
                            GUI.HorizontalSlider(new Rect(10, 190, 135, 23), prevAttackDamage, 0, 300);
                            GUI.Label(new Rect(10, 200, 135, 23), "Attack range: " + prevAttackRange);
                            GUI.HorizontalSlider(new Rect(10, 220, 135, 23), prevAttackRange, 0f, 100f);
                            GUI.Label(new Rect(10, 230, 135, 23), "Attack speed: " + prevAttackSpeed);
                            GUI.HorizontalSlider(new Rect(10, 250, 135, 23), prevAttackSpeed, 0f, 100f);
                            GUI.Label(new Rect(10, 260, 135, 23), "Ress. damage: " + prevItemDamageRessource);
                            GUI.HorizontalSlider(new Rect(10, 280, 135, 23), prevItemDamageRessource, 0, 100);
                            GUI.Label(new Rect(10, 290, 135, 23), "Sharpness: " + prevItemSharpness);
                            GUI.HorizontalSlider(new Rect(10, 310, 135, 23), prevItemSharpness, 0f, 100f);
                        }
                        else if (prevItemHeal > 1 || prevItemStamina > 1 || prevItemHunger > 1)
                        {
                            GUI.Label(new Rect(10, 170, 135, 23), "Heal: " + prevItemHeal);
                            GUI.HorizontalSlider(new Rect(10, 190, 135, 23), prevItemHeal, 0f, 100f);
                            GUI.Label(new Rect(10, 200, 135, 23), "Stamina: " + prevItemStamina);
                            GUI.HorizontalSlider(new Rect(10, 220, 135, 23), prevItemStamina, 0f, 100f);
                            GUI.Label(new Rect(10, 230, 135, 23), "Hunger: " + prevItemHunger);
                            GUI.HorizontalSlider(new Rect(10, 250, 135, 23), prevItemHunger, 0f, 100f);
                            GUI.Label(new Rect(10, 260, 135, 23), "Process time: " + prevItemProcessTime);
                            GUI.HorizontalSlider(new Rect(10, 280, 135, 23), prevItemProcessTime, 0f, 100f);
                        }
                        else if (prevItemArmor > 1)
                        {
                            GUI.Label(new Rect(10, 170, 135, 23), "Armor: " + prevItemArmor);
                            GUI.HorizontalSlider(new Rect(10, 190, 135, 23), prevItemArmor, 0f, 100f);
                        }
                        else
                        {
                            GUI.Label(new Rect(10, 170, 135, 23), "No stats avaible");
                        }

                    }
                    else
                    {
                        //Item selectionné
                       

                        //Armes
                        if (itemSelected.heal == 0 && itemSelected.stamina == 0 && itemSelected.armor == 0 && itemSelected.buildable == false && itemSelected.attackSpeed != 0.7f)
                        {
                            ScrollPositionStats = GUI.BeginScrollView(new Rect(10, 170, 160, 150), ScrollPositionStats, new Rect(5, 170, 135, 190), false, false);
                            GUI.Label(new Rect(10, 170, 135, 23), "Multiplier: " + attackMultiplier);
                            attackMultiplier = (int)Math.Round(GUI.HorizontalSlider(new Rect(10, 190, 135, 23), attackMultiplier, 1, 10), 1);
                            GUI.Label(new Rect(10, 200, 135, 23), "Attack damage: " + itemSelected.attackDamage);
                            itemSelected.attackDamage = (int)Math.Round(GUI.HorizontalSlider(new Rect(10, 220, 135, 23), itemSelected.attackDamage, 0, 300 * attackMultiplier), 1);
                            GUI.Label(new Rect(10, 230, 135, 23), "Attack range: " + itemSelected.attackRange);
                            itemSelected.attackRange = (float)Math.Round(GUI.HorizontalSlider(new Rect(10, 250, 135, 23), itemSelected.attackRange, 0f, 100f * attackMultiplier), 1);
                            GUI.Label(new Rect(10, 260, 135, 23), "Attack speed: " + itemSelected.attackSpeed);
                            itemSelected.attackSpeed = (float)Math.Round(GUI.HorizontalSlider(new Rect(10, 280, 135, 23), itemSelected.attackSpeed, 0f, 100f * attackMultiplier), 1);
                            GUI.Label(new Rect(10, 290, 135, 23), "Ress. damage: " + itemSelected.resourceDamage);
                            itemSelected.resourceDamage = (int)Math.Round(GUI.HorizontalSlider(new Rect(10, 310, 135, 23), itemSelected.resourceDamage, 0, 100 * attackMultiplier), 1);
                            GUI.Label(new Rect(10, 320, 135, 23), "Sharpness: " + itemSelected.sharpness);
                            itemSelected.sharpness = (float)Math.Round(GUI.HorizontalSlider(new Rect(10, 340, 135, 23), itemSelected.sharpness, 0f, 100f * attackMultiplier), 1);
                            GUI.EndScrollView();
                        }
                        
                        //Bouffe
                        else if (itemSelected.tag.ToString() == "Food")
                        { 
                            GUI.Label(new Rect(10, 170, 135, 23), "Heal: " + itemSelected.heal);
                            itemSelected.heal = (float)Math.Round(GUI.HorizontalSlider(new Rect(10, 190, 135, 23), itemSelected.heal, 0, 100 * attackMultiplier), 1);
                            GUI.Label(new Rect(10, 200, 135, 23), "Stamina: " + itemSelected.stamina);
                            itemSelected.stamina = (int)Math.Round(GUI.HorizontalSlider(new Rect(10, 220, 135, 23), itemSelected.stamina, 0f, 100f * attackMultiplier), 1);
                            GUI.Label(new Rect(10, 230, 135, 23), "Hunger: " + itemSelected.hunger);
                            itemSelected.hunger = (int)Math.Round(GUI.HorizontalSlider(new Rect(10, 250, 135, 23), itemSelected.hunger, 0f, 100f * attackMultiplier), 1);
                            GUI.Label(new Rect(10, 260, 135, 23), "Process Time: " + itemSelected.processTime);
                            itemSelected.processTime = (int)Math.Round(GUI.HorizontalSlider(new Rect(10, 280, 135, 23), itemSelected.processTime, 0f, 100f * attackMultiplier), 1);
                        }
                        //Armure
                        else if (itemSelected.armor > 1 && itemSelected.tag.ToString() != "Fuel")
                        {
                            GUI.Label(new Rect(10, 170, 135, 23), "Armor: " + itemSelected.armor);
                            itemSelected.armor = (int)Math.Round(GUI.HorizontalSlider(new Rect(10, 190, 135, 23), itemSelected.armor, 0, 100 * attackMultiplier), 1);
                        }
                        else
                        {
                            GUI.Label(new Rect(10, 170, 135, 23), "No stats avaible");
                        }
                    }
                    

                    if (GUI.Button(new Rect(10, 330, 140, 40), "Spawn \n" + itemName))
                    {
                        prevItemStackable = itemSelected.stackable;
                        prevItemMax = itemSelected.max;

                        if (ItemSpawnerAmount > 1)
                        {
                            itemSelected.stackable = true;
                            itemSelected.max = ItemSpawnerAmount;
                        }

                        if (isDropped)
                        {
                            ClientSend.DropItem(itemSelected.id, ItemSpawnerAmount);
                            resetItemStats();
                            showAllItems();
                        }
                        else
                        {
                            InventoryItem itemInventory = itemSelected;
                            itemInventory.amount = (int)ItemSpawnerAmount;
                            InventoryUI.Instance.AddItemToInventory(itemInventory);
                            resetItemStats();
                            showAllItems();
                        }
                        itemSelected.stackable = prevItemStackable;
                        itemSelected.max = prevItemMax;

                    }
                    break;
            }
            

            if (GUI.Button(new Rect(10, 375, 580, 20), "Exit"))
            {
                this.isOpen = false;
            }

            if (itemSelected == null)
            {
                itemName = "None selected";
            }
            else
            {
                itemName = itemSelected.name;
                itemSelect(itemSelected);
            }

            void showItem(InventoryItem item, InventoryItem.ItemType type)
            {
                if (item.type == type)
                {                   
                    if (GUI.Button(new Rect(x, y, 50, 50), new GUIContent(item.sprite.texture, item.name + "à" + item.description +"é"+ item.attackDamage + "è" + item.attackRange + "ç" + item.attackSpeed + "(" + item.heal + ")" + item.armor + "=" + item.stamina + "+" + item.hunger + "-" + item.resourceDamage + "*" + item.sharpness + "/" + item.processTime + "ù" + item.buildable + "$")))
                    {
                        prevItemStackable = item.stackable;
                        prevItemMax = item.max;

                        if (ItemSpawnerAmount > 1)
                        {
                            item.stackable = true;
                            item.max = ItemSpawnerAmount;
                        }

                        if (toolbarInt == 0)
                        {
                           if (isDropped)
                            {        
                                ClientSend.DropItem(item.id, ItemSpawnerAmount);
                                item.stackable = prevItemStackable;
                                item.max = prevItemMax;
                            }
                            else
                            {
                                InventoryItem itemInventory = item;
                                itemInventory.amount = (int)ItemSpawnerAmount;
                                InventoryUI.Instance.AddItemToInventory(itemInventory);
                            }
                        }

                        item.stackable = prevItemStackable;
                        item.max = prevItemMax;

                        if (toolbarInt == 1)
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

            void resetItemStats()
            {
                itemSelected.processTime = prevItemProcessTime;
                itemSelected.sharpness = prevItemSharpness;
                itemSelected.resourceDamage = prevItemDamageRessource;
                itemSelected.hunger = prevItemHunger;
                itemSelected.stamina = prevItemStamina;
                itemSelected.armor = prevItemArmor;
                itemSelected.heal = prevItemHeal;
                itemSelected.attackSpeed = prevAttackSpeed;
                itemSelected.attackRange = prevAttackRange;
                itemSelected.attackDamage = prevAttackDamage;
                attackMultiplier = prevAttackMultiplier;
            }

            void itemSelect(InventoryItem item)
            {
                if (itemItem)
                {
                    itemItem = false;
                    prevItemItem = true;
                }
                if (storageItem)
                {
                    storageItem = false;
                    prevStorageItem = true;
                }
                if (foodItem)
                {
                    foodItem = false;
                    prevFoodItem = true;
                }
                if (axeItem)
                {
                    axeItem = false;
                    prevAxeItem = true;
                }
                if (bowItem)
                {
                    bowItem = false;
                    prevBowItem = true;
                }
                if (pickaxeItem)
                {
                    pickaxeItem = false;
                    prevPickaxeItem = true;
                }
                if (swordItem)
                {
                    swordItem = false;
                    prevSwordItem = true;
                }
                if (shieldItem)
                {
                    shieldItem = false;
                    prevShieldItem = true;
                }
                if (shovelItem)
                {
                    shovelItem = false;
                    prevShovelItem = true;
                }
                if (stationItem)
                {
                    stationItem = false;
                    prevStationItem = true;
                }
                
                GUI.backgroundColor = Color.green;
                if (GUI.Button(new Rect(160, 20, 50, 50), new GUIContent(item.sprite.texture, item.name + "à" + item.description + "é" + item.attackDamage + "è")))
                {
                    showAllItems();
                }

            }

            void showAllItems()
            {
                resetItemStats();
                itemSelected = null;
                itemName = "None selected";
                if (prevItemItem)
                {
                    itemItem = true;
                    prevItemItem = false;
                }
                if (prevStationItem)
                {
                    storageItem = true;
                    prevStorageItem = false;
                }
                if (prevFoodItem)
                {
                    foodItem = true;
                    prevFoodItem = false;
                }
                if (prevAxeItem)
                {
                    axeItem = true;
                    prevAxeItem = false;
                }
                if (prevBowItem)
                {
                    bowItem = true;
                    prevBowItem = false;
                }
                if (prevPickaxeItem)
                {
                    pickaxeItem = true;
                    prevPickaxeItem = false;
                }
                if (prevSwordItem)
                {
                    swordItem = true;
                    prevSwordItem = false;
                }
                if (prevShieldItem)
                {
                    shieldItem = true;
                    prevShieldItem = false;
                }
                if (prevShovelItem)
                {
                    shovelItem = true;
                    prevShovelItem = false;
                }
                if (prevStationItem)
                {
                    stationItem = true;
                    prevStationItem = false;
                }
                GUI.backgroundColor = prevColorGUI;
            }

            base.runWin(id);
        }

    }
}

/* CSV LIST ITEMS STATS
  if (GUI.Button(new Rect(10, 350, 50, 20), ""))
    {
    string fileName = @"MyTest.txt";
    StreamWriter sw = File.CreateText(fileName);
    sw.WriteLine("Item name;armor;attackDamage;attackRange;attackSpeed;buildable;buildRotation;craftable;craftAmount;grid;heal;hunger;important;max;processable;processTime;rarity;ressourceDamage;scale;sharpness;stackable;stamina;tag;tier;unlockFirst");
    foreach (InventoryItem item in ItemManager.allScriptableItems)
    {

        sw.WriteLine(item.name.ToString() + ";" +
                 item.armor.ToString() + ";" +
                 item.attackDamage.ToString() + ";" +
                 item.attackRange.ToString() + ";" +
                 item.attackSpeed.ToString() + ";" +
                 item.buildable.ToString() + ";" +
                 item.buildRotation.ToString() + ";" +
                 item.craftable.ToString() + ";" +
                 item.craftAmount.ToString() + ";" +
                 item.grid.ToString() + ";" +
                 item.heal.ToString() + ";" +
                 item.hunger.ToString() + ";" +
                 item.important.ToString() + ";" +
                 item.max.ToString() + ";" +
                 item.processable.ToString() + ";" +
                 item.processTime.ToString() + ";" +
                 item.rarity.ToString() + ";" +
                 item.resourceDamage.ToString() + ";" +
                 item.scale.ToString() + ";" +
                 item.sharpness.ToString() + ";" +
                 item.stackable.ToString() + ";" +
                 item.stamina.ToString() + ";" +
                 item.tag.ToString() + ";" +
                 item.tier.ToString() + ";" +
                 item.unlockWithFirstRequirementOnly.ToString());


    }
}*/