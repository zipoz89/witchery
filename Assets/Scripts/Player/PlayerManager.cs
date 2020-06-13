using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public InventoryObject mainInventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<GroundItem>();
        if (item)
        {
            mainInventory.AddItem(new Item(item.item), 1);
            Destroy(collision.gameObject);
        }
    }
}
