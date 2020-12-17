using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;
    public Text endScoreText;
    public ClickDetector clickDetector;
    public GameObject endUI;
    public Score score;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void WinGame()
    {
        clickDetector.enabled = false;
        endScoreText.text = score.scoreVal.ToString();
        endUI.SetActive(true);
        SoundManager.instance.PlayWinGameSound();
    }
}
