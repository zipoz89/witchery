using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackScript : MonoBehaviour
{


    public GameObject cam;
    public InventoryObject inventory;
    public InventoryObject distellery;

    private void Start()
    {
        inventory.Container.Clear();
        distellery.Container.Clear();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var groundItem = other.GetComponent<GroundItem>();
        if (groundItem)
        {
            Item _item = new Item(groundItem.item);
            if (inventory.AddItem(_item, groundItem.amount))
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void Update()
    {
        this.transform.position = new Vector3(cam.transform.position.x, -1f, transform.position.y);
        if (Input.GetKeyDown(KeyCode.F11))
        {
            inventory.Save();
            distellery.Save();
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            inventory.Load();
            distellery.Load();
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Slots = new InventorySlot[35];
        distellery.Container.Clear();

    }
}
