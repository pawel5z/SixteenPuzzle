﻿using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class LayoutManager : MonoBehaviour
{
    public static LayoutManager instance = null;
    public Shuffler shuffler;
    public Transform[] puzzle1D;
    public Transform[,] puzzle2D = new Transform[4,4];
    public Vector3 gapPos;
    public Score score;
    public int xMin = 0;
    public int xMax = 3;
    public int zMin = 0;
    public int zMax = 3;
    public BoardSounds boardSounds;

    private int puzzleSize = 4;
    /* 0 - no row ever completed
     * 1 - completed once till the 1st row
     * 2 - completed once till the 2nd row
     * 3 - completed once till the 3rd row
     */
    private int completion = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        Init2Dpuzzle();
    }

    private void Init2Dpuzzle()
    {
        foreach (Transform piece in puzzle1D)
        {
            puzzle2D[(int)piece.localPosition.x, (int)piece.localPosition.z] = piece;
        }
    }

    private int piecesInPlace()
    {
        int inPlaceCount = 0;
        for (int z = zMin; z <= zMax; z++)
        {
            for (int x = xMin; x <= xMax && (puzzleSize*z + x + 1) <= 15; x++)
            {
                switch (inPlaceCount)
                {
                    case 4: // fall through
                    case 8: // fall through
                    case 12:
                        if (completion >= inPlaceCount / 4)
                            break;
                        for (int i = 0; i < inPlaceCount; i++)
                            puzzle1D[i].GetComponentInChildren<Animator>().SetTrigger("Spin");
                        completion++;
                        boardSounds.PlayCompletedFragmentClip();
                        break;
                }
                if (puzzle2D[x, z] != null)
                {
                    if (puzzle2D[x, z].GetComponentInChildren<TextMeshPro>().text != (puzzleSize*z + x + 1).ToString())
                        return inPlaceCount;
                }
                else
                    return inPlaceCount;
                inPlaceCount++;
            }
        }
        return inPlaceCount;
    }

    public bool Solved()
    {
        return piecesInPlace() == 15;
    }

    public void MoveAttempt(Transform piece)
    {
        if (piece.localPosition.x == gapPos.x || piece.localPosition.z == gapPos.z) // gap exists in the same column/row
        {
            Move(piece, tweening:true);
            boardSounds.PlayMovedSound();
            score.Inc();

            if (Solved())
                GameController.instance.WinGame();
        }
    }

    public void Move(Transform piece, bool tweening)
    {
        Vector3 pieceOrigPos = piece.localPosition;
        Vector3 gap2PieceNorm = (piece.localPosition - gapPos).normalized; // normalized vector from gap to piece
        Vector3 piece2GapNorm = -gap2PieceNorm;                            // normalized vector from piece to gap
        // update coords and puzzle2D representation of pieces between gap and clicked piece
        for (Vector3 to = gapPos; to != pieceOrigPos; to += gap2PieceNorm)
        {
            Vector3 from = to + gap2PieceNorm;
            if (tweening) // move piece smoothly
                puzzle2D[(int)from.x, (int)from.z].gameObject.GetComponent<PieceScript>().MoveTween(to);
            puzzle2D[(int)to.x, (int)to.z] = puzzle2D[(int)from.x, (int)from.z]; // update board array representation
            puzzle2D[(int)from.x, (int)from.z] = null;                           // temporary gap
            if (!tweening) // move piece immediately
                puzzle2D[(int)to.x, (int)to.z].localPosition += piece2GapNorm;
        }
        gapPos = pieceOrigPos;
        puzzle2D[(int)gapPos.x, (int)gapPos.z] = null;
    }

    /* Returns array of movable pieces in given piece's row/column. */
    public List<GameObject> MovablePieces(Transform piece)
    {
        List<GameObject> movablePieces = new List<GameObject>();
        if (piece.localPosition.x != gapPos.x && piece.localPosition.z != gapPos.z)
            return movablePieces; // given piece is not movable

        Vector3 gapDir = (gapPos - piece.localPosition).normalized;
        for (Vector3 i = piece.localPosition; i != gapPos; i += gapDir)
            if (puzzle2D[(int)i.x, (int)i.z] != null)
                movablePieces.Add(puzzle2D[(int)i.x, (int)i.z].gameObject);
        return movablePieces;
    }

    public void SetPiecesUnclickable()
    {
        foreach (Transform piece in puzzle1D)
            piece.GetComponentInChildren<Transform>().gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    public void SetPiecesClickable()
    {
        foreach (Transform piece in puzzle1D)
            piece.GetComponentInChildren<Transform>().gameObject.layer = LayerMask.NameToLayer("Default");
    }
}
