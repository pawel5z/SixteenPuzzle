using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Concurrent;

public class PieceModel : MonoBehaviour
{
    public Animator[] animators;

    private void OnMouseEnter() {
        List<GameObject> movablePieces = LayoutManager.instance.MovablePieces(transform.parent);
        animators = new Animator[movablePieces.Count];
        int i = 0;
        foreach (GameObject go in movablePieces)
        {
            animators[i] = go.GetComponent<Animator>();
            animators[i].SetTrigger("PullOut");
            i++;
        }
    }

    private void OnMouseExit() {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("MoveIn");
        }
    }
}
