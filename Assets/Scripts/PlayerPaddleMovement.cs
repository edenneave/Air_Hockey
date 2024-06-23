using UnityEngine;

public class PlayerPaddleMovement : MonoBehaviour
{
    bool canMove;   //declare bool variable called canMove

    Rigidbody2D rb; //declare Rigidbody2D called rb
    Vector2 startPos;   //declare Vector2 variable called startPos

    public Transform BoundaryHolder;    //declare Transform variable called BoundaryHolder
    Boundary playerBoundary;    //declare Boundary variable called BoundaryHolder
    Collider2D playerCollider;  //declare Collider2D variable called playerCollider

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   //assign Rigidbody2D component attached to PlayerPadle object as script to rb variable 
        startPos = rb.position; //set startPos variable to initial position of rb
        playerCollider = GetComponent<Collider2D>();    //assign Collider2D component attached to PlayerPaddle object as script to playerCollider variable
        playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,    //new instance of Boundary class and assigns to playerBoundary variable also defines top edge
                                      BoundaryHolder.GetChild(1).position.y,    //define bottom edge
                                      BoundaryHolder.GetChild(2).position.x,    //define left edge
                                      BoundaryHolder.GetChild(3).position.x);   //define right edge

    }

   

    void OnMouseDown()  //when mouse button pressed
    {
        if (playerCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))   //if the point in world space of the current mouse position overlaps with the collider of PlayerPaddle object
        {
            canMove = true; //object can move
        }
        else  //if not clicked over object
        {
            canMove = false;    //object cant move
        }
    }

    void OnMouseDrag()  //when mouse is dragged
    {
        if (canMove)    //if canMove is true
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //get position of cursor in world co ordinates(screen co ordinates converted to world co ordinates)
            Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, playerBoundary.Left, playerBoundary.Right),
                                                  Mathf.Clamp(mousePos.y, playerBoundary.Bottom, playerBoundary.Top));  //clamps x and y position of cursor within boundaries set by playerBoundary
            rb.MovePosition(clampedMousePos);   //allows the PlayerPaddle object to move with cursor
        }
    }

    void OnMouseUp()    //when player releases mouse
    {
        canMove = false;    //object cant move
    }

    public void ResetPos()
    {
        rb.position = startPos; //sets startPos variable to initial position
    }
}