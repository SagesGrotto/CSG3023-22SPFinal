/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Andrew M
 * Last Edited: April 28, 2022
 * 
 * Description: Controls the ball and sets up the intial game behaviors. 
****/

/*** Using Namespaces ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ball : MonoBehaviour
{
    [Header("General Settings")]
    public int score;
    public Text ballTxt;
    public Text scoreTxt;
    public GameObject paddle;
    public AudioSource audioSource;

    [Header("Ball Settings")]
    public bool isInPlay;
    public int numberOfBalls;
    public Rigidbody rb;
    public float speed;
    public Vector3 initialForce;


    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }//end Awake()


    // Start is called before the first frame update
    void Start()
    {
        SetStartingPos(); //set the starting position

    }//end Start()


    // Update is called once per frame
    void Update()
    {
        if (ballTxt) { ballTxt.text = "Balls: " + numberOfBalls; }
        if (scoreTxt) { scoreTxt.text = "Score: " + score; }

        if(isInPlay == false)
        {
            Vector3 pos = new Vector3();
            pos.x = paddle.transform.position.x;
            pos.y = paddle.transform.position.y + paddle.transform.localScale.y;
            transform.position = pos;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isInPlay == false)
        {
            isInPlay = true;
            Invoke("Move", 0f);
        }
    }//end Update()


    private void LateUpdate()
    {
        if (isInPlay == true)
        {
            Vector3 velocity = rb.velocity;
            velocity = speed*velocity.normalized;
            rb.velocity = velocity;
        }

    }//end LateUpdate()

    void Move()
    {
        Vector3 addForce = rb.velocity;
        addForce = initialForce;
        rb.velocity = addForce;
    }

    void SetStartingPos()
    {
        isInPlay = false;//ball is not in play
        rb.velocity = Vector3.zero;//set velocity to keep ball stationary

        Vector3 pos = new Vector3();
        pos.x = paddle.transform.position.x; //x position of paddel
        pos.y = paddle.transform.position.y + paddle.transform.localScale.y; //Y position of paddle plus it's height
        transform.position = pos;//set starting position of the ball 
    }//end SetStartingPos()
    
    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play(0);
        GameObject otherGO = collision.gameObject;
        if (otherGO.tag == "Brick")
        {
            score += 100;
            Destroy(otherGO);
        }
    }


    private void OnTriggerEnter(Collider other)
        {
        GameObject otherGO = other.gameObject;
            if(other.tag=="OutBounds"){
                numberOfBalls -= 1;
            }
            if (numberOfBalls > 0)
            {
                Invoke("SetStartingPos", 2.0f);
            }
        }
}
