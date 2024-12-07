using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lastRing : MonoBehaviour
{
    [SerializeField] private GameObject currentRing; // The current ring to deactivate
    [SerializeField] private float bobHeight = 0.1f; // Range of bobbing on the Y-axis
    [SerializeField] private float bobSpeed = 2.0f; // Speed of the bobbing animation

    private Vector3 currentRingInitialPosition;

    private bool isBobbingCurrentRing = false;

    void Start()
    {
        // Store the initial positions of the rings
        if (currentRing != null)
        {
            currentRingInitialPosition = currentRing.transform.position;
            isBobbingCurrentRing = true;
        }
    }

    void Update()
    {
        // Bobbing logic for the current ring
        if (isBobbingCurrentRing && currentRing != null)
        {
            float newY = currentRingInitialPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
            currentRing.transform.position = new Vector3(
                currentRingInitialPosition.x,
                newY,
                currentRingInitialPosition.z
            );
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger zone
        if (other.CompareTag("Player"))
        {
            if (currentRing != null)
            {
                isBobbingCurrentRing = false; // Stop bobbing the current ring
                currentRing.SetActive(false); // Deactivate the current ring
            }
        }

    }
}
