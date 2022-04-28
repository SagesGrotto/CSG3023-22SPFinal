/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Andrew M
 * Last Edited: April 28, 2022
 * 
 * Description: Paddle controler on Horizontal Axis
****/

/*** Using Namespaces ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 5; //speed of paddle
    private float time = 0;
    private float waitTime = 0.05f;
    Vector3 pos = new Vector3();

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > waitTime)
        {
            time = Time.deltaTime;
            if (Input.GetAxis("Horizontal")!=0)
            {
                pos = transform.position;
                pos.x += Input.GetAxis("Horizontal") * speed;
                transform.position = pos;
            }
        }

        
    }//end Update()
}
