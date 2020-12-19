using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;
    public Text endScoreText;
    public ClickDetector clickDetector;
    public GameObject endUI;
    public Score score;
    public Animator boardAnimator;

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
        boardAnimator.enabled = true;
        boardAnimator.SetTrigger("WaveAll");
    }
}
