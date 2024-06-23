using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour   
{
    public float activationInterval;    //declare float variable called activationInterval
    public float activeDuration;    //declare float variable called activeDuration
    private bool isActive = false;  //declare bool variable called isActive and initialise to false
    private float timeUntilNextActivation = 0.0f;   //declare float variable called timeUntilNextActivation and initialise it to 0
    private float activeTime = 0.0f;    //declare float variable called activeTime and initialise it to o

    private void ActivatePortal()   
    {
        isActive = true;    //set portal to active
        timeUntilNextActivation = activationInterval;   //set these 2 variables equal
        activeTime = activeDuration;    //set these variables equal
        transform.position = new Vector2(Random.Range(-7.4f, 7.4f), Random.Range(-3.6f, 3.6f)); //set position of portal to new Vector2 with random x and y values in given range
    }

    private void DeactivatePortal()
    {
        isActive = false;   //portal no longer active
        transform.position = new Vector2(100.0f, 100.0f);   //set portal to position of x = 100 and y = 100 making it not visible 
    }

    private void Update()
    {
        if (isActive)   //if isActive variable true
        {
            activeTime -= Time.deltaTime;   //calculates how much time is left for portal to be active 
            if (activeTime <= 0.0f) //if activeTime is less than or equal to zero
            {
                DeactivatePortal(); //call DeactivatePortal method 
            }
        }
        else  //if isActive false
        {
            timeUntilNextActivation -= Time.deltaTime;  //calculates how much time left until portal can be activated
            if (timeUntilNextActivation <= 0.0f)    //if timUntilNextActivation less than or equal to zero
            {
                ActivatePortal();   //call the ActivatePortal method
            }
        }
    }
}