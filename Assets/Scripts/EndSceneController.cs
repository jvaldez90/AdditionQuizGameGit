// This code was made with the help of Microsoft's Copilot AI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndSceneController : MonoBehaviour
{
    public TMP_Text finalScoreText;

    void Start()
    {
        finalScoreText.text = $"Your total score is {ScoreKeeper.score} out of {ScoreKeeper.totalQuestions}";
    }

    public void PlayAgain()
    {
        StartCoroutine(LoadSceneDelay());
        SceneManager.LoadScene("Gameplay");
    }
    public void ReturnToMenu()
    {
        StartCoroutine(LoadSceneDelay());
        SceneManager.LoadScene("MainMenu");
    }
    private IEnumerator LoadSceneDelay()
    {
        yield return new WaitForSeconds(3f);
    }
}
