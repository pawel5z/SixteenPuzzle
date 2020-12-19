using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    public float moveTime;
    private Vector3 actualDest;

    public void MoveTween(Vector3 dest)
    {
        actualDest = dest;
        StartCoroutine("MoveTweenCoro");
    }

    private IEnumerator MoveTweenCoro()
    {
        Vector3 beg = transform.localPosition;
        float speed = (actualDest - transform.localPosition).sqrMagnitude / moveTime;
        /* User musn't attempt moving pieces while one is being moved. Otherwise our layout will blow up! */
        // gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        LayoutManager.instance.SetPiecesUnclickable();
        while ((actualDest - transform.localPosition).sqrMagnitude > Mathf.Epsilon)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, actualDest, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        LayoutManager.instance.SetPiecesClickable();
        // gameObject.layer = LayerMask.NameToLayer("Default");
        yield return null;
    }
}
