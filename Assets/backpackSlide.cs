using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class backpackSlide : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    Animator animator;
    public bool isOver;
    public bool isInventoryOpen;
    public GameObject panel;
    private CanvasGroup cg;

    void Start()
    {

        isInventoryOpen = true;
        animator = GetComponent<Animator>();
        
        cg = panel.GetComponent<CanvasGroup>();
        if (animator != null)
        {
            isOver = animator.GetBool("IsOver");
            isInventoryOpen = animator.GetBool("IsOpen");
        }

        panel.GetComponent<RectTransform>().localPosition = new Vector3(100, 160, 0);
        animator.SetBool("IsOpen", false);
        cg.alpha = 0;
        isInventoryOpen = false;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
        animator.SetBool("IsOver",true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
        if(!isInventoryOpen)
        animator.SetBool("IsOver", false);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isInventoryOpen)
        {
            animator.SetBool("IsOpen", true);
            panel.GetComponent<RectTransform>().localPosition = new Vector3(100, -9, 0);
            cg.alpha = 1;
            isInventoryOpen = true;
        }
        else
        {
            panel.GetComponent<RectTransform>().localPosition = new Vector3(100, 160, 0);
            animator.SetBool("IsOpen", false);
            cg.alpha = 0;
            isInventoryOpen = false;
        }
    }

}
