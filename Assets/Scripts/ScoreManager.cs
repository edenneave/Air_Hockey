using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public enum Score   //declare enum called Score used to represent two types of scores in game
    {
        AIScore, PlayerScore
    }

    public Text AiScoreText, PlayerScoreText, CountdownText;    //declare Text variables used as UI elements

    public UIManager uIManager; //reference to UIManager script

    public int MaxScore;    //declare int variable called MaxScore
    private int aiScore, playerScore;   //declare int variables called aiScore and playerScore
    public GameObject AIPaddle; //declare GameObject called AIPaddle
    public GameObject PlayerPaddle; //declare GameObject called PlayerPaddle

    private void Awake()
    {
        UpdateScoreText();  //UpdateScoreText method called
        StartCoroutine(StartCountdown());   //start countdown
    }

    public IEnumerator StartCountdown()
    {
        PlayerPaddle.SetActive(false);  //PlayerPaddle not active cant be used
        AIPaddle.SetActive(false);  //AIPaddle not active cant be used

        CountdownText.text = "3";   //display 3
        yield return new WaitForSeconds(1.0f);  //delay 1 second
        CountdownText.text = "2";   //display 2
        yield return new WaitForSeconds(1.0f);  //delay 1 second
        CountdownText.text = "1";   //display 1
        yield return new WaitForSeconds(1.0f);  //delay 1 second
        CountdownText.text = "";

        PlayerPaddle.SetActive(true);   //player paddle active can be used
        AIPaddle.SetActive(true);   //ai paddle active can be used

    }

    public void Increase(Score scoreType)
    {
        if (scoreType == Score.AIScore) //scoreType is AIScore
        {
            aiScore++;  //AI score increases by 1
            if (aiScore >= MaxScore)    //if AI score is more than maxScore
            {
                uIManager.RestartCanvasAppears(true);   //RestartCanvasAppears saying you lose
            }

        }
        else  //if scoreType is PlayerScore
        {
            playerScore++;  //player score increases by 1
            if (playerScore >= MaxScore)    //If player score more than the MaxScore
            {
                uIManager.RestartCanvasAppears(false);  //RestartCanvasAppears saying you Win
            }
        }
        UpdateScoreText();  //UpdateScoreText method called
    }
    public void ResetScores()
    {
        aiScore = playerScore = 0;  //reset scores to 0
        UpdateScoreText();  //UpdateScoreText method called
    }

    private void UpdateScoreText()
    {
        if (AiScoreText != null && PlayerScoreText != null) //if both AiScoreText and PlayerScoreText are not null (they have been assigned values)
        {
            AiScoreText.text = aiScore.ToString();  //set text property of AiScoreText to string representation of aiScore
            PlayerScoreText.text = playerScore.ToString();  //set text property of PlayerScoreText to string representation of playerScore
        }
    }
}