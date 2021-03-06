﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffler : MonoBehaviour
{
    public LayoutManager layoutManager;
    public int swapCount;

    private void Start() {
        Shuffle(swapCount);
    }

    public void Shuffle(int n)
    {
        do
        {
            for (int i = 0; i < n; i++)
            {
                if (Random.Range(0, 2) == 1) // move piece horizontally
                {
                    int x;
                    while ((x = Random.Range(layoutManager.xMin, layoutManager.xMax+1)) == layoutManager.gapPos.x);
                    layoutManager.Move(layoutManager.puzzle2D[x, (int)layoutManager.gapPos.z], tweening:false);
                }
                else // move piece vertically
                {
                    int z;
                    while ((z = Random.Range(layoutManager.zMin, layoutManager.zMax+1)) == layoutManager.gapPos.z);
                    layoutManager.Move(layoutManager.puzzle2D[(int)layoutManager.gapPos.x, z], tweening:false);
                }
            }
        /* Theoretically there is non-zero probability of obtaining already solved puzzle after shuffling (but 
         * extremely small though). So make sure it doesn't happen! */
        } while (layoutManager.Solved());
    }
}
