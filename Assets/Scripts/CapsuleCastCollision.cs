using UnityEngine;

public class CapsuleCastCollision : MonoBehaviour
{
    private float maxDistance = 0.5f;
    private float height = 1.8f;
    private bool canMove = false;

    public bool CanMove(Vector2 input, Vector3 position, Vector3 forward, ref Vector3 direction)
    {
        canMove = !Physics.CapsuleCast(position, position + Vector3.up * height, maxDistance / 2, direction, maxDistance);

        if (!canMove)
        {
            direction = new Vector3(input.x, 0, 0);
            canMove = !Physics.CapsuleCast(position, position + Vector3.up * height, maxDistance / 2, direction, maxDistance);
        }

        if (!canMove)
        {
            direction = new Vector3(0, 0, input.y);
            canMove = !Physics.CapsuleCast(position, position + Vector3.up * height, maxDistance / 2, direction, maxDistance);
        }


        return canMove && direction.magnitude > 0.001f;
    }

    public void OnDrawGizmos()
    {
        float gizmoLength = 0.5f;  // Length of our Raycast (maxDistance)
        float gizmoHeight = 0.5f;    // Height offset above the pivot
                                     // Change the Gizmos colour
        Gizmos.color = Color.green;
        // Calculate start point (elevated above pivot)
        Vector3 startPoint = transform.position + Vector3.up * gizmoHeight;
        // Calculate end point (start + forward direction scaled by length)
        Vector3 endPoint = startPoint + transform.forward * gizmoLength;
        // Draw the main line
        Gizmos.DrawLine(startPoint, endPoint);
        // Draw a small sphere at the end for an "arrowhead" effect
        Gizmos.DrawSphere(endPoint, 0.05f);  // Tiny sphere as a tip

        float radius = 0.25f;
        float height = 1.8f;
        Gizmos.color = Color.yellow;
        float cylinderTopHeight = (height - (radius * 2) / 2f);
        float cylinderBotton = radius;

        // Calculate the positions of the two sphere centers
        Vector3 topSphereCenter = transform.position + transform.up * cylinderTopHeight + transform.forward * radius;
        Vector3 bottomSphereCenter = transform.position + transform.up * cylinderBotton + transform.forward * radius;

        // Draw the two wire spheres at the ends of the capsule
        Gizmos.DrawWireSphere(topSphereCenter, radius);
        Gizmos.DrawWireSphere(bottomSphereCenter, radius);

        // Draw the connecting lines for the cylindrical part
        // These lines connect the edges of the spheres to form the cylinder outline
        Vector3 rightOffset = transform.right * radius;
        Vector3 forwardOffset = transform.forward * radius;

        // Draw the four main connecting lines
        Gizmos.DrawLine(topSphereCenter + rightOffset, bottomSphereCenter + rightOffset);
        Gizmos.DrawLine(topSphereCenter - rightOffset, bottomSphereCenter - rightOffset);
        Gizmos.DrawLine(topSphereCenter + forwardOffset, bottomSphereCenter + forwardOffset);
        Gizmos.DrawLine(topSphereCenter - forwardOffset, bottomSphereCenter - forwardOffset);
    }
}
