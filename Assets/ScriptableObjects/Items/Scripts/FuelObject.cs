using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Fuel Object", menuName = "Invenotry System/Item/Fuel")]
public class FuelObject : ItemObject
{
    public int fuelPower;
    public void Awake()
    {
        type = ItemType.Fuel;
    }
}