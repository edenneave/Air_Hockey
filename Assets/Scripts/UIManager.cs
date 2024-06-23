using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject CanvasGame;   //define GameObject variable called CanvasGame 
    public GameObject CanvasRestart;    //define GameObject variable called CanvasRestart
    public GameObject WinText;  //define GameObject variable called WinText
    public GameObject LoseText; //define GameObject variable called LoseText
    public ScoreManager scoreManager;   //reference to ScoreManager script
    public PlayerPaddleMovement playerPaddleMovement;   //reference to PlayerPaddleMovement script
    public PuckScript puckScript;   //reference to PuckScript script
    public AIMovement aIMovement;   //reference to AIMovement script

    public void RestartCanvasAppears(bool didAIWin)
    {
        Time.timeScale = 0; //pauses game by setting time to 0

        CanvasGame.SetActive(false);    //CanvasGame not active
        CanvasRestart.SetActive(true);  //CanvasRestart appears and is active

        WinText.SetActive(!didAIWin);   //if didAIWin false then WInText displayed
        LoseText.SetActive(didAIWin);   //if didAIWin true then LoseText displayed
    }

    public void Restart()
    {
        Time.timeScale = 1; //Resumes game by setting time to 1

        CanvasGame.SetActive(true); //CanvasGame appears and is active
        CanvasRestart.SetActive(false); //CanvasRestart disappears and not active

        playerPaddleMovement.ResetPos();    //ResetPos method of playerPaddleMovement script called, resetting position of player paddle
        aIMovement.ResetPos();  //ResetPos method of aIMovement script called, resetting position of AI paddle
        scoreManager.ResetScores(); //ResetScores method of scoreManager script called, resetting the scores to 0
        puckScript.CentrePuck();    //CentrePuck method called of puckScript script, centring the puck
    }
 }