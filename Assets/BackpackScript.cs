using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackScript : MonoBehaviour
{
    public GameObject camera;
    public InventoryObject inventory;   


    private void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            inventory.AddItem(new Item(item.item), item.amount);
            Destroy(other.gameObject);
            
        }
    }

    private void Update()
    {
        this.transform.position = new Vector3(camera.transform.position.x, -1f, transform.position.y);
        if (Input.GetKeyDown(KeyCode.F11))
        {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            inventory.Load();
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[35];
    }
}
