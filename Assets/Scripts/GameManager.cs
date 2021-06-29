using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Canvas mainCanvas;
    private void Update()
    {
        if (mainCanvas.enabled == false)
            if (Input.GetKeyUp("escape"))
            {
                var cells = GameObject.FindGameObjectsWithTag("Cell");
                foreach (var icell in cells)
                {
                    Destroy(icell);
                }

                mainCanvas.enabled = true;
            }
    }
}