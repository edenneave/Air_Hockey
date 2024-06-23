using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour
{
    public ScoreManager ScoreManagerInstance;   //declare ScoreManager Instance
    public float MaxSpeed;  //declare float variable MaxSpeed
    private bool playerLastToTouch; //bool variable declared called playerLastToTouch
    private bool didScore;  //bool variable declared called didScore
    private Rigidbody2D rb; //Rigidbody2D variable declared called rb

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   //Puck object set to rb variable
        didScore = false;   //initialise didScore variable to false
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (didScore) return;   //if didScore is true return out doing nothing else

        if (other.CompareTag("AIGoal")) //if tag of object that puck collides with is AIGoal
        {
            ScoreManagerInstance.Increase(ScoreManager.Score.PlayerScore);  //Increase function of ScoreManagerInstance called where player's score increases
            didScore = true;    //didScore set to true so puck doesnt score again before reset
            StartCoroutine(ResetPuck(false));   //start Resetpuck routine 
        }
        else if (other.CompareTag("PlayerGoal"))    //if tag of object that puck collides with is PlayerGoal
        {
            ScoreManagerInstance.Increase(ScoreManager.Score.AIScore);  //Increase function of ScoreManagerInstance called where AI's score increases
            didScore = true;    //didScore set to true so puck doesnt score again before reset
            StartCoroutine(ResetPuck(true));    //start ResetPuck routine
        }
        else if (other.gameObject.CompareTag("Portal")) //if tag of object that puck collides with is Portal
        {
            if (playerLastToTouch == true)  //if player touched puck last
            {
                ScoreManagerInstance.Increase(ScoreManager.Score.PlayerScore);  //player score increases
            }
            if (playerLastToTouch == false) //if AI touched player last
            {
                ScoreManagerInstance.Increase(ScoreManager.Score.AIScore);  //AI score increases
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PlayerPaddle")    //if tag of object that puck collides with is PlayerPaddle
        {
            playerLastToTouch = true;   //playerLastToTouch variable set to true
        }
        if (collision.gameObject.name == "AIPaddle")    //if tag of object that puck collides with is AIPaddle
        {
            playerLastToTouch = false;  //playerLastToTouch variable set to false
        }
    }

    private IEnumerator ResetPuck(bool didAIScore)
    {
        yield return new WaitForSeconds(1f);    //pauses coroutine for 1 second
        didScore = false;   //set didScore to false so puck can be scored again
        rb.velocity = Vector2.zero; //set puck's velocity to zero
        rb.position = didAIScore ? Vector2.right : Vector2.left;    //set puck's position slightly to right side of screen if didAIScore true and to the left if false
    }

    public void CentrePuck()
    {
        rb.position = Vector2.zero; //set puck position to zero
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);    //clamp the magnitude of puck's velocity to MaxSpeed
    }
}