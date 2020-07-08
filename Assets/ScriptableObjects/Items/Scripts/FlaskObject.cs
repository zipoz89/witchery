using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Flask Object", menuName = "Invenotry System/Item/Flask")]
public class FlaskObject : ItemObject
{
    public bool isEmpty = false;
    public void Awake()
    {
        if (isEmpty)
            type = ItemType.Flask;
        else type = ItemType.EmptyFlask;
    }
}