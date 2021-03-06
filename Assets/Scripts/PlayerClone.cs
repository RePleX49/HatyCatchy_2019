﻿//HATY CATCHY by m@ boch - NYU GAMECENTER - Oct 2016
//modifications by Bennett Foddy - Jan 2019

using UnityEngine;
using System.Collections;

//This script is a simple player controller
public class PlayerClone : MonoBehaviour
{
    //public variables like this one are accessible to other scripts, and often set in the inspector
    //they're great for tunable variables because we can change them while the game is running
    public float moveSpeed; //we're not setting its value here, because we want to remember to set it in the inspector.
    public float PowerUpTime = 6.0f;
    public float flashRate = 0.75f;

    public Sprite sadPlayerSprite;
    public Sprite happyPlayerSprite;

    private GameObject player;

    //private variables have to be set in code, like Phaser's global variables
    private SpriteRenderer CloneSR;

    private float initTime;
    private bool isFlashing = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        //fill the SpriteRenderer variable with a reference to the SpriteRender on this GameObject
        CloneSR = GetComponent<SpriteRenderer>();
        initTime = Time.timeSinceLevelLoad;
        Destroy(this.gameObject, PowerUpTime);
    }

    // Update is called once per frame
    void Update()
    {
        //int for speed multiplier
        int speedMultiplier;

        float timeSinceInit = Time.timeSinceLevelLoad - initTime;
        Debug.Log(timeSinceInit);
        if(timeSinceInit >= (PowerUpTime-2.5) && !isFlashing)
        {
            InvokeRepeating("StartFlashing", 0.0f, flashRate);
            isFlashing = true;
        }

        //Input.GetAxis lets you use arrows, WASD, joysticks, etc.
        //These are also mapped in the Input Manager (Edit->Project Settings->Input)

        //Here we use the GetAxis input to move the player's position.
        //Note: the player has a physics Rigidbody2D component, but we aren't using it for physics, just collision detection, so we set it to 'Kinematic' mode.
        //As a rule, you don't want to move a Collider2D unless it has a Rigidbody2D on it.
        if (Input.GetAxis("Horizontal") > 0)
        {
            //Move the player a little to the right (remember a Vector3 has an X (horizontal), Y (vertical) and Z (depth) part)
            //we use Time.deltaTime so that the player moves at the same speed even if the frame rate changes.
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            //Move the player a little to the left (remember a Vector3 has an X (horizontal), Y (vertical) and Z (depth) part)
            //we use Time.deltaTime so that the hat moves at the same speed even if the frame rate changes.
            transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0);
        }

        //let's stop the player from leaving the screen! 
        //We can use a reference to the main camera to get the x position of the edges of the screen...
        //and if the player goes too far, we can move them back to the edge.

        //we could get these values from the Camera component on the main camera, which would make it work no matter what resolution we were
        //rendering the game at.

        //But that's too complicated for this project. For now let's just declare some values
        float rightEdge = 5f; //the right edge of the screen in world coordinates
        float leftEdge = -5f;
        if (transform.position.x > rightEdge)
        {
            //remember we sadly can't set the x coordinate of a transform in C# directly... we have to give it a whole new Vector3
            transform.position = new Vector3(rightEdge, transform.position.y, 0);
        }
        if (transform.position.x < leftEdge)
        {
            //remember we sadly can't set the x coordinate of a transform in C# directly... we have to give it a whole new Vector3
            transform.position = new Vector3(leftEdge, transform.position.y, 0);
        }
    }

    //To receive the message 'HatCaught' we need to have a public function with that name
    //This is called by the Hat script on each Hat, using SendMessage("HatCaught")
    public void HatCaught()
    {
        CloneSR.sprite = happyPlayerSprite; //changes the sprite on the SpriteRenderer component
    }

    //To receive the message 'HatMissed' we need to have a public function with that name
    //This is called by the Hat script on each Hat, using SendMessage("HatMissed")
    public void HatMissed()
    {
        CloneSR.sprite = sadPlayerSprite;//changes the sprite on the SpriteRenderer component
    }

    public void OnDestroy()
    {
        player.SendMessage("ClearPowerUp");
    }

    public void StartFlashing()
    {
        if(CloneSR.enabled)
        {
            CloneSR.enabled = false;
        }
        else
        {
            CloneSR.enabled = true;
        }
    }
}
