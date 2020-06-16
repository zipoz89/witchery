    using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public ItemObject item;

    private bool isPicked = false;

    public GameObject targetObj;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    public int amount = 1;


    private void Start()
    {
        targetObj = GameObject.Find("Backpack");
    }

    private void LateUpdate()
    {
        if (isPicked)
        {
            SmothFollow();
        }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "ItemCollector")
        if (!isPicked) {
            isPicked = true;
        }
    }

    private void SmothFollow()
    {
        Vector3 targetPosition = targetObj.transform.TransformPoint(new Vector3(0, 0, -10));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public void OnBeforeSerialize()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
    }

    public void OnAfterDeserialize()
    {
    }

    //public void SpawnItem(Vector3 pos, ItemObject itemSpawn) 
    //{
    //    item = itemSpawn;
    //    pos.z = 0;
    //    Instantiate(this, pos, Quaternion.identity);
    //}

    //public void SpawnItem(Vector3 pos, InventorySlot itemSpawn)
    //{
    //    item = itemSpawn.item.itemObj;
    //    pos.z = 0;
    //    Instantiate(this, pos, Quaternion.identity);
    //}
}
