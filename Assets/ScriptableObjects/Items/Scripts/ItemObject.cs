using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    TakeOnly,
    Default,  
    Ingredient,
    Fuel,
    EmptyFlask,
    Flask,
    
}

public enum Atributes
{
    waer
}

[CreateAssetMenu(fileName = "New Item", menuName = "Invenotry System/Item/item")]
public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite uiDisplay;
    public bool stackable;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
    public Item data = new Item();

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
    public int Id = -1;
    public Item() 
    {
        Name = "";
        Id = -1;
    }

    //public ItemObject itemObj;
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
        //itemObj = item;
    }
}