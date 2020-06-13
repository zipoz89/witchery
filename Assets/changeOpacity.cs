using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class changeOpacity : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private CanvasGroup canvGroup;
    void Start()
    {
        canvGroup = GetComponent<CanvasGroup>();
        this.gameObject.SetActive(false);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        canvGroup.alpha = 1f;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canvGroup.alpha = 0.7f;
    }
}
