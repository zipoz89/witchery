using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectItem : MonoBehaviour
{



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            var pos = Input.mousePosition;
            pos.z = 45;
            pos = Camera.main.ScreenToWorldPoint(pos);
            transform.position = pos;
        }
    }


}
