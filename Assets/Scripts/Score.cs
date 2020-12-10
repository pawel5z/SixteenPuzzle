using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public string baseText = "Moves: ";
    public TextMeshPro scoreTextMesh;
    public int scoreVal = 0;

    private void Start() {
        UpdateScoreText();
    }

    public void Inc()
    {
        scoreVal++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreTextMesh.text = baseText + scoreVal.ToString();
    }
}
