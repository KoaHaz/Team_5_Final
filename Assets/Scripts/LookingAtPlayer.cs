using System.Collections.Generic;
using UnityEngine;

public class RotateUnlessPlayerHit : MonoBehaviour
{
    public GameObject player;                  // The player GameObject
    public GameObject cannonballPrefab;       // Prefab for the cannonball (Serialized for Unity Inspector)
    public Transform firePoint;               // Point where the cannonball spawns
    public float spinSpeed = 45f;             // Rotation speed in degrees per second
    public float projectileSpeed = 20f;       // Speed of the fired cannonball
    public float fireCooldown = 2f;           // Cooldown time between shots
    public float despawnDelay = 2f;           // Delay before despawning the cannon

    private float lastFireTime = 0f;          // Time when the cannon last fired
    private bool targetLocked = false;

    void FixedUpdate()
    {
        // Perform a raycast from the cannon's forward direction
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // If the ray hits the player
            if (hit.collider.gameObject == player)
            {
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);

                // Check if the cannon is ready to fire
                if (Time.time >= lastFireTime + fireCooldown)
                {
                    FireCannonball();
                    lastFireTime = Time.time; // Update the last fire time
                }
                targetLocked = true;
                return; // Stop rotating if the cannon is aligned with the player
            }
            else if (!targetLocked)
            {
                // Rotate the cannon if not aligned with the player
                transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
            }
        }
    }

    void FireCannonball()
    {
        // Instantiate the cannonball at the fire point
        GameObject newCannonball = Instantiate(cannonballPrefab, firePoint.position, firePoint.rotation);

        // Apply velocity to the cannonball
        Rigidbody rb = newCannonball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * projectileSpeed;
        }

        // Set the firing cannon on the cannonball
        Cannonball cannonballScript = newCannonball.GetComponent<Cannonball>();
        if (cannonballScript != null)
        {
            cannonballScript.SetFiringCannon(this);  // 'this' refers to the current instance of RotateUnlessPlayerHit
        }

        Debug.Log("Cannonball fired!");
    }

    // Add this method to detect collisions with the player's weapon
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerWeapon"))
        {
            Debug.Log("Cannon hit by player weapon. Preparing to despawn...");

            // Optionally add effects here (e.g., explosion or sound)

            // Destroy the cannon after a delay
            Destroy(gameObject, despawnDelay);
        }
    }
}
