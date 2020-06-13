using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCameraObject : MonoBehaviour
{
    public float ScrollSpeed = 1;
    public int cpos = 0;
    public int leftmax = -1;
    public int rightmax = 1;
    //public GameObject camera;
    public double xtrn;
    private bool wasMoving = false;

    //followCameraObject cameraObject;

    private void Start()
    {
        //cameraObject = camera.GetComponent<followCameraObject>();
    }

    void Update()
    {
        

        if (this.transform.position.x % 3.2 != 0 && !(Input.mousePosition.x >= Screen.width * 0.99)  && !(Input.mousePosition.x <= Screen.width * 0.01)&&wasMoving==true)
        {
            wasMoving = false;
            xtrn = this.transform.position.x / 3.2;
            cpos = (int)Math.Round(xtrn);
           // cpos = FindPos();
        }

        if (!(Input.mousePosition.x >= Screen.width * 0.99) && !(Input.mousePosition.x <= Screen.width * 0.01) && (this.transform.position.x > rightmax*3.2 || this.transform.position.x < leftmax*3.2))
        {
            if (this.transform.position.x > rightmax * 3.2)
            {
                cpos = rightmax;
            }
            else
            {
                cpos = leftmax;
            }
            MoveTo();
        }

        if (Input.mousePosition.x >= Screen.width * 0.99 && transform.position.x <= 3.2)
        {
            wasMoving = true;
            transform.Translate(Vector3.right * Time.deltaTime * ScrollSpeed, Space.World);
            
        }
        if (Input.mousePosition.x <= Screen.width * 0.01 && transform.position.x >=-3.2)
        {
            wasMoving = true;
            transform.Translate(Vector3.left * Time.deltaTime * ScrollSpeed, Space.World);
          
        }
        if (Input.GetKeyDown(KeyCode.A) && cpos!=leftmax)
        {
            cpos--;
            MoveTo();
        }
        if (Input.GetKeyDown(KeyCode.D) && cpos != rightmax)
        {
            cpos++;
            MoveTo();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            cpos=0;
            MoveTo();
        }
    }

    private int FindPos()
    {
        int a = 0;
        double x = this.transform.position.x;
        double ax = Math.Abs(x);
        for (int i = 0; i < 10; i++)
        {
            if (i * 3.2 < ax && (i + 1) * 3.2 > ax) {
                a = i;
                break;
            }
        }

        if (ax - a * 3.2 < a * 3.2 - ax)
        {
            if (x > 0)
            return a;
                else return -a;
        }
        else
        {
            if (x > 0)  return a + 1;
                else return -(a + 1);
         } 

    }

    void MoveTo()
    {
        double npos = cpos * 3.2;
        Vector3 temp = new Vector3((float)npos, 0, 0);
        this.transform.position = temp;
    }
}
