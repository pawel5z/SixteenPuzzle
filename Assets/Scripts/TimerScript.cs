using UnityEngine;
using TMPro;
using System.Collections;

public class TimerScript : MonoBehaviour
{
    public TextMeshPro timeText;
    public float updatePeriod;
    public string baseText = "Time: ";

    private float timeElapsed = 0f;

    public float TimeElapsed { get => timeElapsed; }

    void Start()
    {
        StartCoroutine("UpdateTimeText");
    }

    private void Update() {
        timeElapsed += Time.deltaTime;
    }

    private IEnumerator UpdateTimeText()
    {
        while (true)
        {
            timeText.text = baseText + timeElapsed.ToString("N2");
            yield return new WaitForSecondsRealtime(updatePeriod);
        }
    }
}
