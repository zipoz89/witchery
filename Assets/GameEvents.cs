using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    void Start()
    {
        current = this;
    }

    public event Action onSlotUpdated;
    public void SlotUpdate() 
    {
        if (onSlotUpdated != null)
        {
            onSlotUpdated();
        }
    }

}
