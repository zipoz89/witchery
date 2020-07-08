using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DistilleryScript : MonoBehaviour
{
    public GameObject panel;
    public bool isInventoryOpen ;
    private CanvasGroup cg;
    void Start()
    {
        cg = panel.GetComponent<CanvasGroup>();

        panel.GetComponent<RectTransform>().localPosition = new Vector3(-86, 160, 0);
        isInventoryOpen = false;
        cg.alpha = 0;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!isInventoryOpen)
        {
            panel.GetComponent<RectTransform>().localPosition = new Vector3(-86,-10,0);
            isInventoryOpen = true;
            cg.alpha = 1;
        }
        else
        {
            panel.GetComponent<RectTransform>().localPosition = new Vector3(-86, 160, 0);
            isInventoryOpen = false;
            cg.alpha = 0;
        }

    }

}
