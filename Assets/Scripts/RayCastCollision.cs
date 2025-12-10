using UnityEngine;

public class RayCastCollision : MonoBehaviour
{
    private float maxDistance = 0.5f;
    private bool canMove = false;

    public bool CanMove(Vector3 position, ref Vector3 direction)
    {
        canMove = !Physics.Raycast(position, direction, maxDistance);

        if (!canMove)
        {
            direction = new Vector3(direction.x, 0, 0);
            canMove = !Physics.Raycast(position, direction, maxDistance);
        }

        if (!canMove)
        {
            direction = new Vector3(0, 0, direction.z);
            canMove = !Physics.Raycast(position, direction, maxDistance);
        }

        return canMove;
    }
}
