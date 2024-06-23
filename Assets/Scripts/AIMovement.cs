using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public float MaxSpeed;  //declare MaxSpeed variable for AI maximum speed as float 
    private Rigidbody2D rb; //declare Rigidbody2D component called rb
    private Vector2 startPos;   //declare startPos variable which will store position of starting position of AIPaddle

    public Rigidbody2D Puck;    //ddeclare Rigidbody2D component called Puck

    public Transform PlayerBoundary;    //declare Transform variable called PlayerBoundary
    private Boundary playerBoundary;    //declare Boundary variable called playerBoundary

    public Transform PuckBoundary;  //declare Transform variable called PuckBoundary
    private Boundary puckBoundary;  //declare Boundary variable called PlayerBoundary

    private Vector2 targetPosition; //declare Vector2 variable called targetPosition which will store the current target position of the AI

    private bool isFirstTimeInUsersSide = true; //declare bool variable called isFirstTimeInUsersSIde
    private float offsetYFromTarget;    //declare float variable called offsetYFromTarget which will store a random offset based on the target position

    private void Start()    //called once when game starts
    {
        rb = GetComponent<Rigidbody2D>();   //initialise the rb variable with a referenct ot RigidBody2D to same game object as script
        startPos = rb.position; //set initial position of game object to current position of component

        playerBoundary = new Boundary(PlayerBoundary.GetChild(0).position.y,    //new instance of Boundary class and assigns to playerBoundary variable also defines top edge
                                      PlayerBoundary.GetChild(1).position.y,    //define bottom edge
                                      PlayerBoundary.GetChild(2).position.x,    //define left edge
                                      PlayerBoundary.GetChild(3).position.x);   //define right edge

        puckBoundary = new Boundary(PuckBoundary.GetChild(0).position.y,    //new instance of Boundary class and assigns to puckBoundary variable also defines top edge
                                    PuckBoundary.GetChild(1).position.y,    //define bottom edge
                                    PuckBoundary.GetChild(2).position.x,    //define left edge
                                    PuckBoundary.GetChild(3).position.x);   //define right edge
    }

    private void FixedUpdate()  //called at fixed interval to perform physics calculations
    {
        float speed;    //local variable declared called speed

        if (Puck.position.x > puckBoundary.Right)   //if x position of puck is greater than the right edge of the puckBoundary region(on player's side)
        {
            if(isFirstTimeInUsersSide)  //if it the pucks first time in user's/player's side
            {
                isFirstTimeInUsersSide = false; //set to false so that this block of code wont run again in this round
                offsetYFromTarget = Random.Range(1f, -1f);  //offset set to a random range between -1 and 1
            }
            speed = MaxSpeed * Random.Range(0.3f, 0.1f);    //speed variable set to a random value between 0.1 and 0.3 multiplied by the MaxSpeed value
            targetPosition = new Vector2(startPos.x, Mathf.Clamp(Puck.position.y + offsetYFromTarget, playerBoundary.Bottom, playerBoundary.Top));  //set targetPosition to new Vector2 with x equal to starting position of AIPaddle and y equal to previous y offset added to current y pos of puck
        }
        else
        {
            isFirstTimeInUsersSide = true;  //in AI half of board

            speed = Random.Range(MaxSpeed, MaxSpeed * 0.4f);    //speed set to random value between the maxSpeed and 40% of the MaxSpeed value
            targetPosition = new Vector2(Mathf.Clamp(Puck.position.x, playerBoundary.Left, playerBoundary.Right),   
                                         Mathf.Clamp(Puck.position.y, playerBoundary.Bottom, playerBoundary.Top));  //targetPosition set to x value equal to x position of puck and y equal to the y position of puck
        }

        rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition, speed * Time.deltaTime));  //moves AIPaddle towards the targetPosition
    }

    public void ResetPos()
    {
        rb.position = startPos; //reset position of AIPaddle to its inital position
    }
}