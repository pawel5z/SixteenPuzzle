using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LayoutManager : MonoBehaviour
{
    public Shuffler shuffler;
    public Transform[] puzzle1D;
    public Transform[,] puzzle2D = new Transform[4,4];
    public Vector3 gapPos;
    public int shiftsNumber = 1000;
    public Score score;
    public Text endScoreText;
    public ClickDetector clickDetector;
    public GameObject endUI;

    private int puzzleSize = 4;
    public int xMin = 0;
    public int xMax = 3;
    public int zMin = 0;
    public int zMax = 3;


    void Awake()
    {
        Init2Dpuzzle();
    }

    private void Init2Dpuzzle()
    {
        foreach (Transform piece in puzzle1D)
        {
            puzzle2D[(int)piece.localPosition.x, (int)piece.localPosition.z] = piece;
        }
        puzzle1D = null;
    }

    public bool Solved()
    {
        for (int z = zMin; z <= zMax; z++)
        {
            for (int x = xMin; x <= xMax && (puzzleSize*z + x + 1) <= 15; x++)
            {
                if (puzzle2D[x, z] != null)
                {
                    if (puzzle2D[x, z].GetComponentInChildren<TextMeshPro>().text != (puzzleSize*z + x + 1).ToString())
                        return false;
                }
                else
                    return false;
            }
        }
        return true;
    }

    public void MoveAttempt(Transform piece)
    {
        if (piece.localPosition.x == gapPos.x || piece.localPosition.z == gapPos.z) // gap exists in the same column/row
        {
            Move(piece);
            score.Inc();

            if (Solved())
            {
                clickDetector.enabled = false;
                endScoreText.text = score.scoreVal.ToString();
                endUI.SetActive(true);
            }
        }
    }

    public void Move(Transform piece)
    {
        Vector3 pieceOrigPos = piece.localPosition;
        Vector3 gap2PieceNorm = (piece.localPosition - gapPos).normalized;
        Vector3 piece2GapNorm = -gap2PieceNorm;
        // update coords and puzzle2D representation
        for (Vector3 to = gapPos; to != pieceOrigPos; to += gap2PieceNorm)
        {
            Vector3 from = to + gap2PieceNorm;
            puzzle2D[(int)to.x, (int)to.z] = puzzle2D[(int)from.x, (int)from.z];
            puzzle2D[(int)to.x, (int)to.z].localPosition += piece2GapNorm;
        }
        gapPos = pieceOrigPos;
        puzzle2D[(int)gapPos.x, (int)gapPos.z] = null;
    }
}
