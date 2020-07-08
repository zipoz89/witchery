using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int balance = 0;
    public GameObject textMesh;
    private TextMeshProUGUI text;

    void Start()
    {
       text = textMesh.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "$" + balance;
    }
}
