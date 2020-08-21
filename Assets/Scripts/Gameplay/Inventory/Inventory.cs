﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory
{
    public int Size = 5;
    public event EventHandler OnItemListChanged;

    private List<Item> Items;
    private Action<Item> UseItemAction;
    public static Inventory Instance { get; private set; }

    public Inventory(Action<Item> useItemAction)
    {
        this.UseItemAction = useItemAction;
        Items = new List<Item>();

        //AddItem(new Item { Type = Item.ItemType.Weapon, Name = "Sword_6", Amount = 1 });
        //AddItem(new Item { Type = Item.ItemType.Weapon, Name = "Sword_7", Amount = 1 });
        //AddItem(new Item { Type = Item.ItemType.Armor, Name = "Helmet_3", Amount = 1 });
        //AddItem(new Item { Type = Item.ItemType.Consumable, Name = "Items_Consumable_13", Amount = 1 });
        //AddItem(new Item { Type = Item.ItemType.Consumable, Name = "Items_Consumable_14", Amount = 1 });
    }

    public void AddItem(Item item)
    {
        if (Items.Count > Size) return;

        if (item.IsStackable())
        {
            bool itemInInventory = false;
            foreach (Item inventoryItem in Items)
            {
                if (inventoryItem.Name == item.Name)
                {
                    inventoryItem.Amount += item.Amount;
                    itemInInventory = true;
                }
            }
            if (!itemInInventory)
            {
                Items.Add(item);
            }
        }
        else
        {
            Items.Add(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        if (Items.Count == 0) return;

        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in Items)
            {
                if (inventoryItem.Name == item.Name)
                {
                    inventoryItem.Amount -= item.Amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.Amount <= 0)
            {
                Items.Remove(itemInInventory);
            }
        }
        else
        {
            Items.Remove(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Item item)
    {
        UseItemAction(item);
    }

    public List<Item> GetItems()
    {
        return Items;
    }
}
