using UnityEngine;

public class PlayerAutoMovement : MonoBehaviour
{
    private MovementController movementController;
    private bool active;

    private void Awake()
    {
        movementController = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAutoMovement()
    {

    }

    public void StopAutoMovement()
    {

    }
}
