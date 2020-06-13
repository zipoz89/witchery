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
    openBackpack inventory;
    void Start()
    {
        isInventoryOpen = false;
        animator = GetComponent<Animator>();
        inventory = GetComponent<openBackpack>();
        if (animator != null)
        {
            isOver = animator.GetBool("IsOver");
            isInventoryOpen = animator.GetBool("IsOpen");
        }
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
            isInventoryOpen = true;
            panel.gameObject.SetActive(true);
        }
        else
        {
            animator.SetBool("IsOpen", false);
            isInventoryOpen = false;
            panel.gameObject.SetActive(false);
        }
    }

}
