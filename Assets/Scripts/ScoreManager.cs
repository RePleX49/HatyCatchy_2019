//HATY CATCHY by m@ boch - NYU GAMECENTER - Oct 2016
//modifications by Bennett Foddy - Jan 2019

using UnityEngine;
using System.Collections;

//This Score manager script keeps and displays the score
//It receives messages from the Hats to increment the score
//It also makes sounds based on the Hat messages
public class ScoreManager : MonoBehaviour {

    //public variables like this one are accessible to other scripts, and often set in the inspector
    //they're great for tunable variables because we can change them while the game is running
    //sometimes it's just easier to set a reference to another object by dragging it into a public variable in the inspector
    public TextMesh scoreText; //reference to the 3D score text object, set in the inspector
    public AudioSource audioSource; //reference to audio source on the score object
    public AudioClip caughtSound; //sound we'll use for catched hats
    public AudioClip missedSound; //sould we'll use for missed hats

    //private variables have to be set in code
    private int score;

    //Start is called at game start, sets the score to 0
    void Start () {
        score = 0;
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = score.ToString(); //in C# you can't just put a number in a text object - you need to convert it.
                                           //it would also be legal to write:
                                           //scoreText.text = "Score: " + score;
                                           //but not legal to write:
                                           //scoreText.text = score
    }

    //To receive the message 'HatCaught' we need to have a public function with that name
    //This is called by the Hat script on each Hat, using SendMessage("HatCaught")
    public void HatCaught()
    {
        Debug.Log("player caught a hat"); //Print a useful message in the debug console
        score += 1;
        audioSource.PlayOneShot(caughtSound); //play the caught sound
    }

    //To receive the message 'HatMissed' we need to have a public function with that name
    //This is called by the Hat script on each Hat, using SendMessage("HatMissed")
    public void HatMissed()
    {
        Debug.Log("player missed a hat"); //Print a useful message in the debug console
        score -= 1;
        audioSource.PlayOneShot(missedSound); //play the missed sound
    }
}
