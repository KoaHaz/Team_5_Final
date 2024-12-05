using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUnlessPlayerHit : MonoBehaviour
{
    public GameObject player;  // The target GameObject to detect
    public float spinSpeed = 45f;  // Speed of rotation in degrees per second

    void FixedUpdate()
    {
        // Perform a raycast (linetrace) from the object's forward direction
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // If the ray hits the player, do not rotate
            if (hit.collider.gameObject == player)
            {
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);

                //FIRE THE CANNONBALL

                return; // Stop rotation
            }
        }

        // Otherwise, rotate the object around its Y-axis
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);

    }
}
