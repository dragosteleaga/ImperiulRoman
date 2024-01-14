using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct QuestionData
{
    public string question;
    public string[] answers;
    public int correctAnswerIndex;
}
public class NPC_interaction_1 : MonoBehaviour
{   
    [SerializeField] private GameObject coinPrefab; 
    [SerializeField] private QuestionData questionData;
    [SerializeField] private Transform player;
    private GameObject interactionPanel;
    private Text questionText;
    private Button[] answerButtons;
    private float interactionDistance = 2f;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform not assigned in the Inspector.");
            return;
        }

        CreateUIElements();

        questionText.text = questionData.question;
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Text buttonText = answerButtons[i].GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = questionData.answers[i];
            }

            int index = i;
            answerButtons[i].onClick.AddListener(() => AnswerQuestion(index));
        }
    }

    private void CreateUIElements()
    {
        Canvas canvas = FindObjectOfType<Canvas>() ?? CreateCanvas();

        interactionPanel = CreatePanel(canvas.transform);
        questionText = CreateText(interactionPanel.transform, "QuestionText", 100);
        answerButtons = new Button[3];
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i] = CreateButton(interactionPanel.transform, "AnswerButton" + i, -30 - (40 * i));
        }

        interactionPanel.SetActive(false);
    }

    private Canvas CreateCanvas()
    {
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        return canvas;
    }

    private GameObject CreatePanel(Transform parent)
    {
        GameObject panel = new GameObject("InteractionPanel");
        panel.transform.SetParent(parent, false);
        RectTransform rect = panel.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(400, 250);
        rect.anchoredPosition = Vector2.zero;
        panel.AddComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 0.5f);
        return panel;
    }

    private Text CreateText(Transform parent, string name, float posY)
    {
        GameObject textObj = new GameObject(name);
        textObj.transform.SetParent(parent, false);
        RectTransform rect = textObj.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(380, 30);
        rect.anchoredPosition = new Vector2(0, posY);
        Text text = textObj.AddComponent<Text>();
        text.alignment = TextAnchor.MiddleCenter;
        text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.fontSize = 14;
        text.color = Color.black;
        return text;
    }

    private Button CreateButton(Transform parent, string name, float posY)
    {
        GameObject buttonObj = new GameObject(name);
        buttonObj.transform.SetParent(parent, false);
        RectTransform rect = buttonObj.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(360, 30);
        rect.anchoredPosition = new Vector2(0, posY);
        buttonObj.AddComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f);
        Button button = buttonObj.AddComponent<Button>();
        CreateText(buttonObj.transform, "ButtonText", 0);
        return button;
    }

    private void Update()
    {
        HandleInteraction();

        if (interactionPanel.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                AnswerQuestion(0); // Answer with the first button
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                AnswerQuestion(1); // Answer with the second button
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                AnswerQuestion(2); // Answer with the third button
            }
        }
    }

    private void HandleInteraction()
    {
        if (Vector3.Distance(transform.position, player.position) < interactionDistance)
        {
            if (!interactionPanel.activeInHierarchy)
            {
                interactionPanel.SetActive(true);
            }
        }
        else if (interactionPanel.activeInHierarchy)
        {
            interactionPanel.SetActive(false);
        }
    }

    private void AnswerQuestion(int index)
    {
        bool isCorrect = index == questionData.correctAnswerIndex;
        Debug.Log(isCorrect ? "Correct Answer!" : "Incorrect Answer!");
        interactionPanel.SetActive(false);

        if (isCorrect)
        {
            if (coinPrefab != null)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity); // Instantiate a new coin
            }
        }
       
        gameObject.SetActive(false); // Make NPC disappear if the answer is incorrect
        
    }



}
