using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronManager : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject child;
    private SpriteRenderer spriteRenderer;
    float timeLeft;
    Color targetColor;
    public Color standard;
    public bool isActive = false;
    public bool isSet = false;
    void Start()
    {
        spriteRenderer = child.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
            changeColor();
        else if (!isSet)
        {
            isSet = true;
            spriteRenderer.material.color = standard;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var groundItem = other.GetComponent<GroundItem>();
        if (groundItem)
        {
            if(groundItem.isPicked == false)
            {
                isActive = true;

                Item _item = new Item(groundItem.item);
                if (inventory.AddItem(_item, groundItem.amount))
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }

    private void changeColor() 
    {
        isSet = false;
        if (timeLeft <= Time.deltaTime)
        {
            // transition complete
            // assign the target color
            spriteRenderer.material.color = targetColor;

            // start a new transition
            targetColor = new Color(Random.value, Random.value, Random.value);
            timeLeft = 1.0f;
        }
        else
        {
            // transition in progress
            // calculate interpolated color
            spriteRenderer.material.color = Color.Lerp(spriteRenderer.material.color, targetColor, Time.deltaTime / timeLeft);

            // update the timer
            timeLeft -= Time.deltaTime;
        }
    }
}
