//HATY CATCHY by m@ boch - NYU GAMECENTER - Oct 2016
//modifications by Bennett Foddy - Jan 2019

using UnityEngine;
using System.Collections;

public class Hat : MonoBehaviour
{

    GameObject scoreManager;
    GameObject player;
    private float fallSpeed=5.0f;

    // Start() is called at the beginning of the game
    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager");//fill the scoreManager variable with a reference to the Score Manager
        player = GameObject.Find("Player"); //fill player Variable with reference to Player
    }

    // Update() is called every frame
    void Update()
    {
        //We aren't using the physics engine here, so we have to be responsible for moving the hat toward the ground
        //we use Time.deltaTime so that the hat moves at the same speed even if the frame rate changes.
        transform.position += new Vector3(0,-fallSpeed*Time.deltaTime,0);
    }

    //OnTriggerEnter2D() is called by the unity engine under the following conditions
    //1. The object that the script is on has a Collider2D set to 'Trigger' mode(could be a box, circle, etc)
    //2. The object that the script is on is touching another object with a Collider2D and a Rigidbody2D on it.
    //The function receives a reference to the incoming Collider2D component as a parameter

    //Note: we aren't using any of the other functions of the Rigidbody2D here, so we set it to 'Kinematic' mode.
    //As a rule, you don't want to move a Collider2D unless it has a Rigidbody2D on it.
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //check to see if the colliding object had the tag 'Ground'
        if (otherCollider.tag == "Ground")
        {
            //And tell the scoreManager & player that the player missed a hat
            scoreManager.SendMessage("HatMissed");
            player.SendMessage("HatMissed");
            //Then destroy the hat, destroy needs to be sent a game object, which we can get from this.gameObject
            Destroy(this.gameObject);

        }
        //check to see if the colliding object had the tag 'Player'
        if (otherCollider is BoxCollider2D && otherCollider.tag == "Player")
        {
            //Tell the scoreManager & player that the player missed a hat
            scoreManager.SendMessage("HatCaught");
            player.SendMessage("HatCaught");
            //Then destroy the hat, destroy needs to be sent a game object, which we can get from this.gameObject
            Destroy(this.gameObject);
        }
    }
}