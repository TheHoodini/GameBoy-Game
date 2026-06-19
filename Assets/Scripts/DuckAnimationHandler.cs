using UnityEngine;

public class DuckAnimationHandler : MonoBehaviour
{
    private DuckController duckController;

    private void Awake()
    {
        duckController = FindFirstObjectByType<DuckController>();
    }

    public void OnTrickEnd()
    {
        //duckController.IsTricking = false;
    }
}
