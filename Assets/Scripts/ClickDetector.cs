using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    public Camera mainCamera;
    public LayoutManager layoutManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // LMB clicked
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.CompareTag("Piece"))
                {
                    /* Move whole piece object. Not only model relatively to parent. */
                    layoutManager.MoveAttempt(hitInfo.transform.parent);
                }
            }
        }
    }
}
