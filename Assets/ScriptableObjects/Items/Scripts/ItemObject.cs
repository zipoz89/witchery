using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Ingredient,
    Fuel,
    Default
}

public enum Atributes
{
    waer
}


public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite uiDisplay;
    public ItemType type;
    [TextArea(15,20)]
    public string description;

    public Item createItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    //public ItemObject itemObj;
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
        //itemObj = item;
    }
}