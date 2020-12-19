using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;
    public Text endScoreText;
    public Text totalTimeText;
    public ClickDetector clickDetector;
    public GameObject endUI;
    public Score score;
    public Animator boardAnimator;
    public TimerScript timerScript;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void WinGame()
    {
        timerScript.StopAllCoroutines();
        clickDetector.enabled = false;
        endScoreText.text = score.scoreVal.ToString();
        totalTimeText.text = timerScript.TimeElapsed.ToString("N2") + totalTimeText.text;
        endUI.SetActive(true);
        boardAnimator.enabled = true;
        boardAnimator.SetTrigger("WaveAll");
    }
}
