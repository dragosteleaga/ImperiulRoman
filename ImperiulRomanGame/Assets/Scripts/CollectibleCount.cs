using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class CollectibleCount : MonoBehaviour {
    private TMP_Text text;
    private Image image;
    private int count;

    [SerializeField]
    private Button speedButton;

    [SerializeField]
    private Button traceButton;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        GameObject imageObject = GameObject.Find("Image");
        if (imageObject != null)
        {
            image = imageObject.GetComponent<Image>();
        }

        SetAlpha(0);

        speedButton.gameObject.SetActive(false);
        traceButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Collectible.OnCollected += OnCollectibleCollected;
    }

    private void OnDisable()
    {
        Collectible.OnCollected -= OnCollectibleCollected;
    }
    private void OnCollectibleCollected()
    {
        count++;
        UpdateCount();
        StartCoroutine(ShowTemporarily());

        if (count == 3)
        {
            count = 0;
            ShowButtons();
        }
    }

    private IEnumerator ShowTemporarily()
    {
        SetAlpha(1);
        yield return new WaitForSeconds(4);
        SetAlpha(0);
    }

    private void SetAlpha(float alpha)
    {
        Color textColor = text.color;
        textColor.a = alpha;
        text.color = textColor;

        if (image != null)
        {
            Color imageColor = image.color;
            imageColor.a = alpha;
            image.color = imageColor;
        }
    }

    private void UpdateCount()
    {
        text.text = $"{count} / 3";
    }

    private void ShowButtons()
    {
        speedButton.gameObject.SetActive(true);
        traceButton.gameObject.SetActive(true);
        UnlockCursor();
    }

    public void SpeedButtonClicked()
    {
        Debug.Log("Speed button clicked.");
        speedButton.gameObject.SetActive(false);
        traceButton.gameObject.SetActive(false);
        LockCursor();
    }

    public void TraceButtonClicked()
    {
        Debug.Log("Trace button clicked.");
        traceButton.gameObject.SetActive(false);
        speedButton.gameObject.SetActive(false);
        LockCursor();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
