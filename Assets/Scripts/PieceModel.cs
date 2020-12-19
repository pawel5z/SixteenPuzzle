using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceModel : MonoBehaviour
{
    public float moveTime;
    private PieceModel[] pieceModels;

    private void OnMouseEnter() {
        List<GameObject> movablePieces = LayoutManager.instance.MovablePieces(transform.parent);
        pieceModels = new PieceModel[movablePieces.Count];
        int i = 0;
        foreach (GameObject go in movablePieces)
        {
            pieceModels[i] = go.GetComponentInChildren<PieceModel>();
            pieceModels[i].PullOut();
            i++;
        }
    }

    private void OnMouseExit() {
        foreach (PieceModel pieceModel in pieceModels)
        {
            pieceModel.MoveIn();
        }
    }

    private IEnumerator MoveModel(Vector3 dest) // move model relatively to parent (piece) transform
    {
        float speed = (dest - transform.localPosition).sqrMagnitude / moveTime;
        while ((dest - transform.localPosition).sqrMagnitude > Mathf.Epsilon)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, dest, 
                                                          speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    public void PullOut()
    {
        StopCoroutine("MoveModel");
        StartCoroutine("MoveModel", Vector3.down / 2f);
    }

    private void MoveIn()
    {
        StopCoroutine("MoveModel");
        StartCoroutine("MoveModel", Vector3.zero);
    }
}
