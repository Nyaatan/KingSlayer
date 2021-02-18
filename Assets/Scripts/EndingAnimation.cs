using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAnimation : MonoBehaviour {

    float acceleration = 300;
    bool moveSword = false, rotateAxe = false;
    //int x;


	public void MoveSwords()
    {
        moveSword = true;
    }

    public void RotateAxe()
    {
        rotateAxe = true;
    }

    public void Update()
    {
        float xHelper = gameObject.transform.position.x;
        if (moveSword)
        {
            gameObject.transform.Translate(new Vector2(acceleration * (float)Math.Pow(-1,(xHelper+Math.Abs(xHelper))/(2*xHelper))*Time.deltaTime,acceleration*Time.deltaTime));
        }
        if (gameObject.transform.position.y <= 3*Screen.height/5) moveSword = false;

        
    }
    
}
