using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FuelType
{
    Rowan,
    Default
}

[CreateAssetMenu(fileName = "New Fuel Object", menuName = "Invenotry System/Item/Fuel")]
public class FuelObject : ItemObject
{
    public int fuelPower;
    public FuelType fuelType;
    public void Awake()
    {
        type = ItemType.Fuel;
    }
}