using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public Text questionText;
    public Button[] answerButtons;
    public GameObject interactionPanel;

    private string currentQuestion;
    private string[] currentAnswers;

    private void Start()
    {
        // Initialize the interaction panel (make sure it's inactive)
        interactionPanel.SetActive(false);

        // Call a function to set a new question and answers
        SetQuestionAndAnswers("What is the capital of Romania?", new string[] { "Belgrade", "Cuba", "Bucharest" });
    }

    public void SetQuestionAndAnswers(string question, string[] answers)
    {
        // Set the current question and answers
        currentQuestion = question;
        currentAnswers = answers;

        // Update the question text
        questionText.text = currentQuestion;

        // Assign answers to buttons (assuming you have three buttons)
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < currentAnswers.Length)
            {
                answerButtons[i].gameObject.SetActive(true);
                answerButtons[i].GetComponentInChildren<Text>().text = currentAnswers[i];
            }
            else
            {
                // Disable extra buttons if there are fewer than three answers
                answerButtons[i].gameObject.SetActive(false);
            }
        }

        // Activate the interaction panel
        interactionPanel.SetActive(true);
    }

    // You can attach functions to each button's OnClick event in the Unity Inspector
    public void OnAnswerButton1Click()
    {
        CheckAnswer(0);
    }

    public void OnAnswerButton2Click()
    {
        CheckAnswer(1);
    }

    public void OnAnswerButton3Click()
    {
        CheckAnswer(2);
    }

    private void CheckAnswer(int selectedAnswerIndex)
    {
        if (selectedAnswerIndex >= 0 && selectedAnswerIndex < currentAnswers.Length)
        {
            string selectedAnswer = currentAnswers[selectedAnswerIndex];
            if (selectedAnswer == "Bucharest")
            {
                Debug.Log("Correct Answer!");
            }
            else
            {
                Debug.Log("Incorrect Answer!");
            }
        }

        // Hide the interaction panel after answering
        interactionPanel.SetActive(false);
    }
}
