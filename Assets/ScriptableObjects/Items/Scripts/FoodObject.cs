﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Invenotry System/Item/Food")]
public class FoodObject : ItemObject
{
    public int foodRestored;
    public void Awake()
    {
        type = ItemType.Food;
    }
}
