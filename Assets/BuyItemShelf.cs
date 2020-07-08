using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class BuyItemShelf : MonoBehaviour
{

    private GameObject moneyManager;
    private MoneyManager money;
    public ItemObject item;
    public int price;
    public GameObject itemGem;
    private GameObject child0;
    private GameObject child1;
    //private GameObject child10;

    private void Start()
    {
        child0 = this.transform.GetChild(0).gameObject;
        //child1 = this.transform.GetChild(1).gameObject;
        //child10 = child1.transform.GetChild(0).gameObject;

        moneyManager = GameObject.Find("MoneyManager");
        money = moneyManager.GetComponent<MoneyManager>();
        child0.GetComponent<SpriteRenderer>().sprite = item.uiDisplay;
        child0.GetComponent<Transform>().transform.localScale = new Vector3(0.8f,0.8f,1);

        //child1.GetComponent<TextMeshProUGUI>().text = price + "";


    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (money.balance - price >= 0)
        {
            spawnItemGem();
            money.balance -= price;
        }

    }

    void spawnItemGem() 
    { 
        Vector3 mpos = this.transform.position;


        GameObject o =  Instantiate(itemGem, mpos, Quaternion.identity);
        o.GetComponent<GroundItem>().item = item;
        o.GetComponent<GroundItem>().amount = 1;
        o.GetComponent<GroundItem>().isPicked = true;
        o.GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
    }
}
