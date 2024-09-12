using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject player;          // Reference to the player's transform
    public float rotationSpeed = 2f;  // Speed at which the object rotates
    public float raycastDistance = 10f; // Distance for the raycast
    public LayerMask obstructionLayer;  // Layer to check for obstructions
    public Animator animator;         // Animator component
    public float fieldOfViewAngle = 45f; // Field of view in degrees


    void Update()
    {
        // Perform raycast if the player is within the field of view
        if (IsPlayerInFieldOfView() && CanSeePlayer())
        {
            // Play animation if the player is visible
            animator.SetTrigger("Taunt");
        }
    }

    bool IsPlayerInFieldOfView()
    {
        // Get the direction to the player
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        // Calculate the dot product between the forward direction and the direction to the player
        float dotProduct = Vector3.Dot(transform.forward, directionToPlayer);

        // Convert the field of view angle from degrees to dot product range
        float maxDotProduct = Mathf.Cos(fieldOfViewAngle * Mathf.Deg2Rad / 2);

        // Check if the dot product is greater than the max allowed dot product
        return dotProduct >= maxDotProduct;
    }

    bool CanSeePlayer()
    {
        // Get direction to player
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        // Perform raycast from the current object to the player
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, raycastDistance))
        {
            // Check if the raycast hit the player
            if (hit.transform == player.transform)
            {
                return true;
            }
        }

        return false;
    }

    // Optional: Debug ray in the editor
    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.transform.position);
        }
    }
}
