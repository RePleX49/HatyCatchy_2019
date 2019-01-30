using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriplePowerUp : MonoBehaviour
{

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); //fill player Variable with reference to Player
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //check to see if the colliding object had the tag 'Ground'
        if (otherCollider.tag == "Ground")
        {
            //Then destroy the hat, destroy needs to be sent a game object, which we can get from this.gameObject
            Destroy(this.gameObject);

        }
        //check to see if the colliding object had the tag 'Player'
        if (otherCollider.tag == "Player")
        {
            //Tell the scoreManager & player that the player missed a hat
            player.SendMessage("TriplePowerUp");
            //Then destroy the hat, destroy needs to be sent a game object, which we can get from this.gameObject
            Destroy(this.gameObject);
        }
    }
}
