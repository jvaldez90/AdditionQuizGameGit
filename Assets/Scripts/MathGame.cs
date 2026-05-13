// This code was made with the help of Microsoft's Copilot AI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MathGame : MonoBehaviour
{
    [Header("TMPro Input Texts")]
    public TMP_InputField totalQuestionsInput;
    public TMP_InputField answerInput;
    public TMP_Text questionText;
    public TMP_Text scoreText;
    public TMP_Text endText;

    private int totalQuestions;
    private int currentQuestion;
    private int score;
    private int questionNo;

    private int num1, num2, correctAnswer;

    // Version 2 Update
    [Header("Slider Variables")]
    public Slider num1MinSlider;
    public Slider num1MaxSlider;
    public Slider num2MinSlider;
    public Slider num2MaxSlider;

    [Header("TMPro Number Range Text")]
    public TMP_Text num1MinText;
    public TMP_Text num1MaxText;
    public TMP_Text num2MinText;
    public TMP_Text num2MaxText;

    private int num1Min, num1Max, num2Min, num2Max, temp;

    [Header("Audio Sources")]
    public AudioSource successSFX;
    public AudioSource buzzerSFX;

    [Header("Buttons")]
    public Button startButton;
    public Button submitButton;

    void Start()
    {
        startButton.interactable = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if(submitButton.interactable)
                SubmitAnswer();
        }
    }

    public void StartGame()
    {
        score = 0;
        currentQuestion = 0;
        questionNo = 1;

        num1Min = (int)num1MinSlider.value;
        num1Max = (int)num2MaxSlider.value;
        num2Min = (int)num2MinSlider.value;
        num2Max = (int)num2MaxSlider.value;

        if(num1Min > num1Max)
        {
            temp = num1Max;
            num1Max = num1Min;
            num1Min = temp;
        }
        if(num2Min > num2Max)
        {
            temp = num2Max;
            num2Max = num2Min;
            num2Min = temp;
        }

        //totalQuestions = int.Parse(totalQuestionsInput.text);
        if (!int.TryParse(totalQuestionsInput.text, out totalQuestions) || totalQuestions <= 0)
        {
            Debug.LogWarning("Invalid number of questions");
            return;
        }
        scoreText.text = $"Score: 0 / {totalQuestions}";

        GenerateQuestion();
    }

    public void SubmitAnswer()
    {
        // int playerAnswer = int.Parse(answerInput.text);
        int playerAnswer;

        if(!int.TryParse(answerInput.text, out playerAnswer))
        {
            // Invalid input - show warning, play error sound, and stop
            buzzerSFX.Play();
            answerInput.text = "";
            return;
        }

        if (playerAnswer == correctAnswer)
        {
            successSFX.Play();
            score++;
        }
        else
            buzzerSFX.Play();

        currentQuestion++;

        scoreText.text = $"SCORE: {score} / {totalQuestions}";
        answerInput.text = "";

        if (currentQuestion < totalQuestions)
            GenerateQuestion();
        else
            EndGame();
    }
    private void GenerateQuestion()
    {
        num1 = Random.Range(num1Min, num1Max + 1);
        num2 = Random.Range(num2Min, num2Max + 1);
        correctAnswer = num1 + num2;

        questionText.text = $"({questionNo})  {num1} + {num2} = ";
        questionNo++;

        // Auto-focus the input field
        answerInput.text = "";
        answerInput.Select();
        answerInput.ActivateInputField();
    }
    private void EndGame()
    {
        ScoreKeeper.score = score;
        ScoreKeeper.totalQuestions = totalQuestions;

        // Start coroutine to wait before loading next scene
        StartCoroutine(LoadEndSceneAfterDelay());

    }
    private IEnumerator LoadEndSceneAfterDelay()
    {
        // Wait for the audio clip to finish
        yield return new WaitForSeconds(2f);

        // Now Load the next Scene
        SceneManager.LoadScene("EndScene");
    }
    public void UpdateNum1MinText()
    {
        num1MinText.text = num1MinSlider.value.ToString();
    }
    public void UpdateNum1MaxText()
    {
        num1MaxText.text = num1MaxSlider.value.ToString();
    }
    public void UpdateNum2MinText()
    {
        num2MinText.text = num2MinSlider.value.ToString();
    }
    public void UpdateNum2MaxText()
    {
        num2MaxText.text = num2MaxSlider.value.ToString();
    }
    public void ValidateSettings()
    {
        bool valid = true;

        // Check total questions
        if (string.IsNullOrEmpty(totalQuestionsInput.text))
            valid = false;

        // Check sliders
        if (num1MinSlider.value == 0 && num1MaxSlider.value == 0)
            valid = false;
        if (num2MinSlider.value == 0 && num2MaxSlider.value == 0)
            valid = false;

        // Enable or disable the Start button
        startButton.interactable = valid;
    }
}
