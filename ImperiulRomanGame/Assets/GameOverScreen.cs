using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public Text pointsText;
    public void Update()
    {
        UnlockCursor();
    }
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString()+" POINTS";
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("GameMenu");
        LockCursor();
    }
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
