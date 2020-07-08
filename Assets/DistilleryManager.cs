using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistilleryManager : MonoBehaviour
{
    Animator animator;
    Animator animator2;
    //private StaticInterface ui;
    public InventoryObject inventory;
    public InventoryObject inventoryOutput;
    public FuelObject fuelObject;
    InventorySlot fuelSlot;
    public float fuel = 0;
    public float timeValue = 5;
    public float time = 5;  
    public int activeRecipieId = -1;
    public bool isWorking = true;
    public GameObject boubles;
    public GameObject arrow;

    public Recipies[] recipieList = new Recipies[0];

    void Start()
    {

        animator = boubles.GetComponentInChildren<Animator>(); //GetComponent<Animator>();
        animator2 = arrow.GetComponentInChildren<Animator>();
        //ui = this.GetComponent<StaticInterface>();
        fuelSlot = inventory.Container.Slots[3];
        //GameEvents.current.onSlotUpdated += onSlotUpdated;
        inventory.GetSlots[0].OnAfterUpdate += OnSlotUpdate;
        inventory.GetSlots[1].OnAfterUpdate += OnSlotUpdate;
        inventory.GetSlots[2].OnAfterUpdate += OnSlotUpdate;
        inventory.GetSlots[3].OnAfterUpdate += OnSlotUpdate;
    }

    public void OnSlotUpdate(InventorySlot _slot) 
    {
        if (fuelSlot.item.Id != -1 || fuel > 0)
        {
            CheckForRecipies();
        }
    }
    private void Update()
    {
        useFuel();
        if (activeRecipieId >= 0)
            ConsumeFuel();
        

    }

    private void CheckForRecipies()
    {
        int s0 = inventory.Container.Slots[0].item.Id;
        int s1 = inventory.Container.Slots[1].item.Id;
        int sfl = inventory.Container.Slots[2].item.Id;
        if(sfl >= 0)
            for (int i = 0; i < recipieList.Length; i++)
            {
                int i0 = -1;
                int i1 = -1;
                int ifl = -1;
                if (recipieList[i].input0 != null) i0 = recipieList[i].input0.Id;
                if (recipieList[i].input1 != null) i1 = recipieList[i].input1.Id;
                if (recipieList[i].flaskSlot != null) ifl = recipieList[i].flaskSlot.Id;
                //Debug.Log(s0 + " " + i0 + " " + s1 + " " + i1);
                if (sfl == ifl)
                    if ((s0 == i0 && s1 == i1) || (s1 == i0 && s0 == i1))
                    {
                        activeRecipieId = i;
                        return;
                    }
                        
                
            }
        
        activeRecipieId = -1;
    }
    private void useFuel() 
    {
        if (fuel > 0)
        {
            fuel -= 1 * Time.deltaTime;
            if (activeRecipieId >= 0)
                animator2.SetBool("isWorking", true);
            else
                animator2.SetBool("isWorking", false);
            if (time > 0 && activeRecipieId!=-1)
                time -= 1 * Time.deltaTime;
            else
            {
                time = timeValue;
                if(activeRecipieId>=0)
                Output(recipieList[activeRecipieId]);
            }
        }
        
        else
        {
            animator.SetBool("IsWorking", false);
            isWorking = false;
            fuel = 0;
        }
        
        
    }

    private void ConsumeFuel() 
    {
        if(fuelSlot.item.Id!=-1)
            fuelObject = (FuelObject)fuelSlot.ItemObject;
        if (fuelSlot.amount > 0 && fuelSlot.CanPlaceInSlot(fuelSlot.ItemObject) && fuel <= 0)
        {
            if(inventory.ConsumeItems(fuelSlot,1))
            { 
            fuel += fuelObject.fuelPower;
            fuelSlot.UpdateSlot(fuelSlot.item, fuelSlot.amount--);
            fuelSlot.UpdateSlot();

            animator.SetBool("IsWorking", true);
            isWorking = true;


                
            }

        }
        

    }

    private void AddOutput(Recipies recipie) 
    {

        if (recipie.output0 != null)
        {
            inventoryOutput.AddItem(recipie.output0.createItem(), recipie.o0amount);
        }
        if (recipie.output1 != null)
        {
            inventoryOutput.AddItem(recipie.output1.createItem(), recipie.o1amount);
        }
        if (recipie.output2 != null)
        {
            inventoryOutput.AddItem(recipie.output2.createItem(), recipie.o2amount);
        }
        if (recipie.output3 != null)
        {
            inventoryOutput.AddItem(recipie.output3.createItem(), recipie.o3amount);
        }
        animator2.SetBool("isWorking", false);

    }
    
    private bool Output(Recipies recipie)
    {
        int freeSlots = 4;
        //int outputSlots = 0;

        List<ItemObject> listOutput = new List<ItemObject>();
        if (recipie.output0 != null) 
        {
            listOutput.Add(recipie.output0);
        }
        if (recipie.output1 != null)
        {
            listOutput.Add(recipie.output1);
        }
        if (recipie.output2 != null)
        {
            listOutput.Add(recipie.output2);
        }
        if (recipie.output3 != null)
        {
            listOutput.Add(recipie.output3);
        }

        List<ItemObject> listInv2 = new List<ItemObject>();
        List<ItemObject> listInv = new List<ItemObject>();
        for (int i = 0; i < 4; i++)
        {
            if (inventoryOutput.GetSlots[i].item.Id != -1) freeSlots--;
            listInv.Add(inventoryOutput.GetSlots[i].ItemObject);
            listInv2.Add(inventory.GetSlots[i].ItemObject);
        }

        if (!listInv2.Contains(recipie.input0)) return false;
        if (!listInv2.Contains(recipie.input1)) return false;
        if (!listInv2.Contains(recipie.flaskSlot)) return false;



        //Debug.Log(freeSlots);

        foreach (var i in listOutput) 
        {
            if (listInv.Contains(i))
            {
                //Debug.Log("id: " + i.Id);
                freeSlots++;
                
            }
        }



        //Debug.Log("free slots= " +freeSlots);

        if (listOutput.Count <= freeSlots)
        {
            if (recipie.input0 != null)
            {
                inventory.ConsumeFromInventory(recipie.input0.createItem(), 1);
                inventory.GetSlots[0].UpdateSlot();
            }
            if (recipie.input1 != null)
            {
                inventory.ConsumeFromInventory(recipie.input1.createItem(), 1);
                inventory.GetSlots[1].UpdateSlot();
            }
            if (recipie.flaskSlot != null)
            {
                inventory.ConsumeFromInventory(recipie.flaskSlot.createItem(), 1);
                inventory.GetSlots[2].UpdateSlot();
            }
            AddOutput(recipie);
            return true;
        }
        return false;
    }

}



[System.Serializable]
public class Recipies 
{
    //input
    public ItemObject input0;
    public ItemObject input1;
    public ItemObject flaskSlot;

    //output

    public ItemObject output0;
    public int o0amount = 1;
    public ItemObject output1;
    public int o1amount = 1;
    public ItemObject output2;
    public int o2amount = 1;
    public ItemObject output3;
    public int o3amount = 1;

    

}