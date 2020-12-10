using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    public Camera mainCamera;
    public LayoutManager layoutManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Piece"))
                {
                    layoutManager.MoveAttempt(hit.transform);
                }
            }
        }
    }
}
