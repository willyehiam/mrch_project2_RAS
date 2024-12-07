using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    [SerializeField] private GameObject currentRing; // The current ring to deactivate
    [SerializeField] private GameObject nextRing; // The next ring to activate
    [SerializeField] private float bobHeight = 0.1f; // Range of bobbing on the Y-axis
    [SerializeField] private float bobSpeed = 2.0f; // Speed of the bobbing animation

    private Vector3 currentRingInitialPosition;
    private Vector3 nextRingInitialPosition;

    private bool isBobbingCurrentRing = false;
    private bool isBobbingNextRing = false;

    void Start()
    {
        // Store the initial positions of the rings
        if (currentRing != null)
        {
            currentRingInitialPosition = currentRing.transform.position;
            isBobbingCurrentRing = true;
        }

        if (nextRing != null)
        {
            nextRingInitialPosition = nextRing.transform.position;
            isBobbingNextRing = false; // Initially next ring is inactive
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

        // Bobbing logic for the next ring
        if (isBobbingNextRing && nextRing != null)
        {
            float newY = nextRingInitialPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
            nextRing.transform.position = new Vector3(
                nextRingInitialPosition.x,
                newY,
                nextRingInitialPosition.z
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

            if (nextRing != null)
            {
                isBobbingNextRing = true; // Start bobbing the next ring
                nextRing.SetActive(true); // Activate the next ring
            }

            // Optionally, deactivate this trigger box to prevent repeated triggering
            //gameObject.SetActive(false);
        }
    }
}
