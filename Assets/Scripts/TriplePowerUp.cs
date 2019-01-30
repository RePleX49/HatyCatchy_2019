using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriplePowerUp : MonoBehaviour
{

    GameObject player;
    private float fallSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); //fill player Variable with reference to Player
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -fallSpeed * Time.deltaTime, 0);
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
            Player playerScript = player.GetComponent<Player>();
            //Tell the scoreManager & player that the player missed a hat
            if (playerScript.bHasTriplePowerUp)
            {
                Destroy(this.gameObject);
            }
            else
            {
                player.SendMessage("TriplePowerUp");
                //Then destroy the hat, destroy needs to be sent a game object, which we can get from this.gameObject
                Destroy(this.gameObject);
            }
            
        }
    }
}
