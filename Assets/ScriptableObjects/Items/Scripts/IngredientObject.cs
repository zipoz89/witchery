using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient Object", menuName = "Invenotry System/Item/Ingredient")]
public class IngredientObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Ingredient;
    }
}
