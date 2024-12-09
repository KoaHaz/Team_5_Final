using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private RotateUnlessPlayerHit firingCannon;  // The cannon that fired this cannonball
    public float lifespan = 5f;   // Destroy the cannonball after this time

    void Start()
    {
        // Automatically destroy the cannonball after its lifespan
        Destroy(gameObject, lifespan);
    }

    public void SetFiringCannon(RotateUnlessPlayerHit cannon)
    {
        firingCannon = cannon;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player slashes the cannonball
        if (other.CompareTag("PlayerWeapon"))
        {
            if (firingCannon != null)
            {
                // Teleport the player to the firing cannon
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    player.transform.position = firingCannon.transform.position;  // Teleport to cannon
                }
            }

            // Destroy the cannonball
            Destroy(gameObject);
        }
    }
}
