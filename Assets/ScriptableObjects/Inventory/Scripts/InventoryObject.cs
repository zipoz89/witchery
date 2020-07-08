    using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName ="New Inventory",menuName = "Invenotry System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabaseObject database;
    public Inventory Container;
    public InventorySlot[] GetSlots { get { return Container.Slots; } }



    public bool AddItem(Item _item, int _amount)
    {
        
        InventorySlot slot = FindItemOnInventory(_item);
        if (EmptySlotCount <= 0 && slot ==null)
            return false;
        if (!database.ItemObjects[_item.Id].stackable || slot == null)
        {
            SetEmptySlot(_item, _amount);
            return true;
        }
        slot.AddAmount(_amount);
        return true;
    }

    public bool ConsumeFromInventory(Item _item, int _amount)
    {
        InventorySlot slot = FindItemOnInventory(_item);
        if (slot != null)
        {
            if (slot.amount - _amount < 0)
                return false;
            else if (slot.amount - _amount == 0)
            {
                slot.RemoveItem();
                return true;
            }
            else 
            {
                slot.UpdateSlot(slot.item, slot.amount - _amount);
                return true;
            }
        }
        return false;
    }
    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.Id <= -1)
                {
                    counter++;
                }
            }
            return counter;
        }
    }
    public InventorySlot FindItemOnInventory(Item _item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id == _item.Id)
            {
                return GetSlots[i];
            }
        }
        return null;
    }
    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id <= -1)
            {
                GetSlots[i].UpdateSlot(_item, _amount);
                return GetSlots[i];
            }
        }
        //set up functionality for full inventory
        return null;
    }

    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        //Debug.Log("zamieniam");

        
        if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
        {
            InventorySlot temp = new InventorySlot(item2.item, item2.amount);
            item2.UpdateSlot(item1.item, item1.amount);
            item1.UpdateSlot(temp.item, temp.amount);
        }
    }

    public bool ConsumeItems(InventorySlot item1, int _amount)
    {
        if (item1.amount > _amount)
        {
            item1.UpdateSlot(item1.item, item1.amount - _amount);
            return true;
        }
        else if (item1.amount == _amount)
        {
            item1.RemoveItem();
            return true;
        }
        else return false;
    }
    public void PutOneItem(InventorySlot item1, InventorySlot item2)
    {
        if (item2.CanPlaceInSlot(item1.ItemObject))
        {
            item2.UpdateSlot(item1.item, 1);
            if(item1.amount  >1)
            item1.UpdateSlot(item1.item, item1.amount-1);
            else
            item1.RemoveItem();
        }
    }

    public void RemoveAmountOfItem(InventorySlot item1,int _amount)
    {
        if (item1.amount-_amount > 0)
            item1.UpdateSlot(item1.item, item1.amount-_amount);
        else if(item1.amount-_amount==0)
            item1.RemoveItem();
        
    }
    public void AddAmountOfItem(InventorySlot item1, int _amount)
    {
        
        item1.UpdateSlot(item1.item, item1.amount + _amount);
    }
    public bool SetItemInSlot(InventorySlot item1, InventorySlot item2, int _amount)
    {
        if (item2.CanPlaceInSlot(item1.ItemObject))
        {
            item2.UpdateSlot(item1.item, _amount);
            return true;
        }
        return false;
    }
    public bool AddOrSetSlot(InventorySlot item1, InventorySlot item2, int _amount)
    {

        if (item2.item.Id == -1)
        {
            //Debug.Log("wstawiam");
            if(SetItemInSlot(item1, item2, _amount))
                return true;
            return false;
        }
        else if (item1.item.Id == item2.item.Id)
        {
            //Debug.Log("dodaję " + item2.amount + _amount);
            AddAmountOfItem(item2, _amount);
            return true;
        }

        return false;
    }


    [ContextMenu("Save")]
    public void Save()
    {
        //string saveData = JsonUtility.ToJson(this, true);
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        //bf.Serialize(file, saveData);
        //file.Close();
        //Debug.Log("saving at" + savePath);
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }
    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            //file.Close();

            //Debug.Log("loading at" + savePath);
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < GetSlots.Length; i++)
            {
                GetSlots[i].UpdateSlot(newContainer.Slots[i].item, newContainer.Slots[i].amount);
            }
            stream.Close();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container.Clear();
    }
}
[System.Serializable]
public class Inventory
{
    public InventorySlot[] Slots = new InventorySlot[35];
    public void Clear()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].RemoveItem();
        }
    }
}

public delegate void SlotUpdated(InventorySlot _slot);

[System.Serializable]
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];
    [System.NonSerialized]
    public UserInterface parent;
    [System.NonSerialized]
    public GameObject slotDisplay;
    [System.NonSerialized]
    public SlotUpdated OnAfterUpdate;
    [System.NonSerialized]
    public SlotUpdated OnBeforeUpdate;
    public Item item = new Item();
    public int amount;

    public ItemObject ItemObject
    {
        get
        {
            if (item.Id >= 0)
            {
                return parent.inventory.database.ItemObjects[item.Id];
            }
            return null;
        }
    }

    public InventorySlot()
    {
        UpdateSlot(new Item(), 0);
    }
    public InventorySlot(Item _item, int _amount)
    {
        UpdateSlot(_item, _amount);
    }
    public void UpdateSlot(Item _item, int _amount)
    {
        if (OnBeforeUpdate != null)
            OnBeforeUpdate.Invoke(this);
        item = _item;
        amount = _amount;
        //if (_amount > 0)
        //    GameEvents.current.SlotUpdate();
        if (OnAfterUpdate != null)
            OnAfterUpdate.Invoke(this);
        
    }
    public void UpdateSlot()
    {
        if (OnBeforeUpdate != null)
            OnBeforeUpdate.Invoke(this);
        if (OnAfterUpdate != null)
            OnAfterUpdate.Invoke(this);
    }
    public void RemoveItem()
    {
        UpdateSlot(new Item(), 0);
    }
    public void AddAmount(int value)
    {
        UpdateSlot(item, amount += value);

    }
    public bool CanPlaceInSlot(ItemObject _itemObject)
    {

        if (_itemObject == null) return true;
        if (AllowedItems.Length <= 0)
        {


            return true;
        }
            
        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (_itemObject.type == AllowedItems[i])
            {
                //Debug.Log("mozna");
                return true;
            }
        }
        return false;
    }
}